using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpDiff.Parsers.GitDiff
{
  public partial class Diff
  {

    internal void DetermineFileNames()
    {
      if (this.files == null) {
        DetermineFileDefNamesHelper helper = new DetermineFileDefNamesHelper(this.diffHeader.RawFileDefs);

        // the diff's files:

        // if there is a ChunksHeader, it contains the files
        if (this.chunksHeader != null) {
          this.files = new IFile[] { chunksHeader.OriginalFile, chunksHeader.NewFile };
        }
        // if there are copy/rename headers, they contain the file names
        else if (GetCopyRenameHeaders(out CopyRenameHeader from, out CopyRenameHeader to)) {
          this.files = new IFile[] { new File('a', from.FileName), new File('b', to.FileName) };
        }
        // if there is a BinaryFiles, we can utilize its RawFileDefs
        else if (this.binaryFiles != null) {
          this.files = helper.GetDiffFilesFromBinaryFiles(this.binaryFiles.RawFileDefs);
        }
        // else a completely empty file was added or deleted. in this case the file names in the header are identical
        else {
          this.files = helper.GetIdenticalHeaderFiles();
          if (this.files == null) {
            System.Diagnostics.Trace.TraceWarning("Unable to determine diff file names. Defaulting to NullFiles.");
            this.files = new IFile[] { new NullFile(), new NullFile() };
          }
        }

        helper.diffFiles = this.files;

        // the diff header's files ('a/<file name> b/<file name>')
        helper.GetHeaderFiles();
      }
    }

    public class DetermineFileDefNamesHelper
    {
      /* ### Add handling of escaped paths
       * http://www.kernel.org/pub/software/scm/git/docs/git-diff.html:
       *
       * TAB, LF, double quote and backslash characters in pathnames are
       * represented as \t, \n, \" and \\, respectively. If there is need for
       * such substitution then the whole pathname is put in double quotes.
       */

      List<KeyValuePair<string, string>> headerSplitCandidates;
      internal IList<IFile> diffFiles;

      public DetermineFileDefNamesHelper(string headerRawFileDefs)
      {
        this.SplitHeader(headerRawFileDefs);
      }

      public DetermineFileDefNamesHelper(DiffHeader header) : this(header.RawFileDefs) { }

      public void SplitHeader(string headerRawFileDefs)
      {
        // gather all candidate splittings for the diff header file names
        this.headerSplitCandidates = new List<KeyValuePair<string, string>>(1);
        for (int index = 0; ; index++) {
          index = headerRawFileDefs.IndexOf(' ', index);
          if (index == -1) break;
          string headerFilename1 = headerRawFileDefs.Substring(0, index);
          string headerFilename2 = headerRawFileDefs.Substring(index + 1 /* " ".Length */);
          if (AreValidFileDefs(headerFilename1, headerFilename2, false)) {
            headerSplitCandidates.Add(new KeyValuePair<string, string>(headerFilename1, headerFilename2));
          }
        }
      }

      public IList<IFile> GetHeaderFiles()
      {
        List<IFile> files = new List<IFile>(2);

        // if there is no space in the file names, it's easy to parse
        if (headerSplitCandidates.Count == 1) {
          files.Add(FileFromFileDef(headerSplitCandidates[0].Key));
          files.Add(FileFromFileDef(headerSplitCandidates[0].Value));
        }
        // if there are spaces in the file name, we must get help from the diff files
        //   (which were determined using other header fields)
        else if (headerSplitCandidates.Count > 1) {
          if (this.diffFiles == null) {
            System.Diagnostics.Trace.TraceError("Missing diff files to determine diff header file names.");
          } else {
            IFile diffFile1 = this.diffFiles[0];
            IFile diffFile2 = this.diffFiles[1];
            if (diffFile1 is NullFile) {
              if (diffFile2 is NullFile) {
                System.Diagnostics.Trace.TraceError("Only one of the diff files may be a NullFile.");
              } else {
                files.Add(new File('a', diffFile2.FileName));
                files.Add(diffFile2);
              }
            } else {
              if (diffFile2 is NullFile) {
                files.Add(diffFile1);
                files.Add(new File('b', diffFile1.FileName));
              } else {
                files.Add(diffFile1);
                files.Add(diffFile2);
              }
            }
          }
        } else { // no candidate
          System.Diagnostics.Trace.TraceError("Unable to determine diff header file names.");
        }

        // check if we found anything, and choose a default if not
        if (files.Count < 2) {
          System.Diagnostics.Trace.TraceWarning("Defaulting to NullFiles.");
          files.Add(new NullFile());
          files.Add(new NullFile());
        }
        return files;
      }

      public IList<IFile> GetDiffFilesFromBinaryFiles(string binaryFilesRawFileDefs)
      {
        List<IFile> files = new List<IFile>(2);

        string rawFileDefs = binaryFilesRawFileDefs;

        // rawFileDefs has the following format:
        //   '<file def> and <file def> differ'
        // See IsValidFileDef for valid values of <file def>.

        // 1. remove ' differ' from the end
        int index = rawFileDefs.IndexOf(" differ", StringComparison.InvariantCulture);
        if (index != -1) {
          rawFileDefs = rawFileDefs.Substring(0, index);
        } else {
          System.Diagnostics.Trace.TraceWarning("Missing string ' differ' at end of BinaryFiles raw line: '{0}'", binaryFilesRawFileDefs);
        }

        // 2. repeatedly split at ' and ' positions, and find the ones where we have valid file names
        var candidates = new List<KeyValuePair<string, string>>(1);
        index = 0;
        while (true) {
          index = rawFileDefs.IndexOf(" and ", index, StringComparison.InvariantCulture);
          if (index == -1) break;
          string filename1 = rawFileDefs.Substring(0, index);
          string filename2 = rawFileDefs.Substring(index + 5 /* " and ".Length */);
          if (AreValidFileDefs(filename1, filename2)) {
            candidates.Add(new KeyValuePair<string, string>(filename1, filename2));
          }
        }

        // 3. find the right candidate
        if (candidates.Count == 0) {
          System.Diagnostics.Trace.TraceWarning("Missing file names in BinaryFiles raw line '{0}'", binaryFilesRawFileDefs);
        } else if (candidates.Count == 1) {
          files[0] = FileFromFileDef(candidates[0].Key);
          files[1] = FileFromFileDef(candidates[0].Value);
        } else {
          // We got at least one file name with ' and b/' or ' and /dev/null' in its path ...
          // We can consult the diff header's raw string, which looks like this:
          //   '<file def> <file def>'
          // Try to find a matching pair of candidates.
          foreach (var candidate in candidates) {
            if (this.headerSplitCandidates.Any(headerCandidate => CandidatesMatch(candidate.Key, headerCandidate.Key, candidate.Value, headerCandidate.Value))) {
              files[0] = FileFromFileDef(candidate.Key);
              files[1] = FileFromFileDef(candidate.Value);
              break;
            }
          }
          if (files.Count < 2) {
            System.Diagnostics.Trace.TraceWarning("Could not find any pair of file names in BinaryFiles header that match the diff header file names.");
          }
        }

        // 4. check if we found anything, and choose a default if not
        if (files.Count < 2) {
          if (candidates.Count > 0) {
            System.Diagnostics.Trace.TraceWarning("Defaulting to first candidate.");
            files[0] = FileFromFileDef(candidates[0].Key);
            files[1] = FileFromFileDef(candidates[0].Value);
          } else {
            System.Diagnostics.Trace.TraceWarning("Defaulting to NullFiles.");
            files[0] = new NullFile();
            files[1] = new NullFile();
          }
        }
        return files;
      }

      // this happens when a completely empty file was added or deleted
      // the complete diff may look like this (note that the files names are identical)
      //   diff --git a/foo.txt b/foo.txt
      //   new file mode 100644
      //   index 0000000..33ab9f1
      public IList<IFile> GetIdenticalHeaderFiles()
      {
        foreach (var candidate in headerSplitCandidates) {
          if (candidate.Key.Substring(2) == candidate.Value.Substring(2)) {
            return new IFile[] { FileFromFileDef(candidate.Key), FileFromFileDef(candidate.Value) };
          }
        }
        return null;
      }

      // A valid file def must be one of:
      //   - NullFile.NAME if mayBeNull
      //   - 'a/<file name>' for the first candidate
      //   - 'b/<file name>' for the second candidate
      // A <file name> may contain spaces, must not be empty and must not end with '/'.
      // At most one file def may be NullFile.NAME.
      public static bool AreValidFileDefs(string candidate1, string candidate2, bool mayBeNull = true)
      {
        return IsValidFileDef(candidate1, true, mayBeNull, out bool candidate1IsNull) &&
           IsValidFileDef(candidate2, false, mayBeNull, out bool candidate2IsNull) &&
           !(candidate1IsNull && candidate2IsNull);
      }

      public static bool IsValidFileDef(string candidate, bool isFirst, bool mayBeNull, out bool isNull)
      {
        isNull = false;
        if (mayBeNull && candidate == NullFile.NAME) {
          isNull = true;
          return true;
        } else if ((isFirst && candidate.StartsWith("a/", StringComparison.InvariantCulture))
                 || (!isFirst && candidate.StartsWith("b/", StringComparison.InvariantCulture))) {
          if (candidate.Length > 2 && !candidate.EndsWith("/", StringComparison.InvariantCulture)) {
            return true;
          }
        }
        return false;
      }

      public static bool CandidatesMatch(string filename1, string headerFilename1, string filename2, string headerFilename2)
      {
        // If the values are equal, everything is fine.
        if (filename1 == headerFilename1 && filename2 == headerFilename2) return true;

        // If filenameX is a NullFile, the other filename must match the other headerFilename
        // and both headerFilenames must be equal (without the prefix).
        if (filename1 == NullFile.NAME) {
          return filename2 == headerFilename2 &&
              headerFilename1.Substring(2) == headerFilename2.Substring(2);
        } else if (filename2 == NullFile.NAME) {
          return filename1 == headerFilename1 &&
              headerFilename1.Substring(2) == headerFilename2.Substring(2);
        }

        // obviously there is no match here
        return false;
      }

      public static IFile FileFromFileDef(string fileDef)
      {
        if (fileDef == null) {
          return null;
        } else if (fileDef == NullFile.NAME) {
          return new NullFile();
        } else {
          // '<letter>/<filename>'
          return new File(fileDef[0], fileDef.Substring(2));
        }
      }
    }
  }
}
