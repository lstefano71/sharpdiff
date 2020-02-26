using System.Collections.Generic;
using System.Linq;
using SharpDiff.Parsers.GitDiff;
using OMetaSharp;

namespace SharpDiff.Parsers.GitDiff
{
    public class GitDiffParser : Parser
    {
        public virtual bool Diffs(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> diffs = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(
                              (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                                modifiedStream4 = inputStream4;
                                if (!MetaRules.Or(modifiedStream4, out result4, out modifiedStream4,
                                  (OMetaStream<char> inputStream5, out OMetaList<HostExpression> result5, out OMetaStream<char> modifiedStream5) => {
                                    modifiedStream5 = inputStream5;
                                    if (!MetaRules.Apply(NewLine, modifiedStream5, out result5, out modifiedStream5)) {
                                      return MetaRules.Fail(out result5, out modifiedStream5);
                                    }
                                    return MetaRules.Success();
                                  }
                                  , (OMetaStream<char> inputStream5, out OMetaList<HostExpression> result5, out OMetaStream<char> modifiedStream5) => {
                                    modifiedStream5 = inputStream5;
                                    if (!MetaRules.Apply(Empty, modifiedStream5, out result5, out modifiedStream5)) {
                                      return MetaRules.Fail(out result5, out modifiedStream5);
                                    }
                                    return MetaRules.Success();
                                  }
                                  )) {
                                  return MetaRules.Fail(out result4, out modifiedStream4);
                                }
                                if (!MetaRules.Apply(Diff, modifiedStream4, out result4, out modifiedStream4)) {
                                  return MetaRules.Fail(out result4, out modifiedStream4);
                                }
                                return MetaRules.Success();
                              }, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  diffs = result2;
                  result2 = (diffs).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Diff(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> diffHeader = null;
            OMetaList<HostExpression> headers = null;
            OMetaList<HostExpression> chunksHeader = null;
            OMetaList<HostExpression> chunks = null;
            OMetaList<HostExpression> binaryFiles = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(DiffHeader, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    diffHeader = result3;
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Headers, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    headers = result3;
                    if (!MetaRules.Apply(ChunksHeader, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    chunksHeader = result3;
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Chunks, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    chunks = result3;
                    result3 = (new Diff(diffHeader.As<DiffHeader>(), headers.ToIEnumerable<IHeader>(), chunksHeader.As<ChunksHeader>(), chunks.ToIEnumerable<Chunk>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(DiffHeader, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    diffHeader = result3;
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Headers, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    headers = result3;
                    if (!MetaRules.Apply(BinaryFiles, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    binaryFiles = result3;
                    result3 = (new Diff(diffHeader.As<DiffHeader>(), headers.ToIEnumerable<IHeader>(), binaryFiles.As<BinaryFiles>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool DiffHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> format = null;
            OMetaList<HostExpression> rawFileDefs = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("diff").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(DiffFormatType, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    format = result3;
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Text, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    rawFileDefs = result3;
                    result3 = (new DiffHeader(format.As<DiffFormatType>(), rawFileDefs.As<string>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("diff").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(DiffFormatType, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    format = result3;
                    result3 = (new DiffHeader(format.As<DiffFormatType>(), (string)null)).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool DiffFormatType(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> format = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("--").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(LetterOrDigit, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  format = result2;
                  result2 = (new DiffFormatType(format.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Headers(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> headers = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Many(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(Header_, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  headers = result2;
                  result2 = (headers).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Header_(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> header = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Apply(Header, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  header = result2;
                  if (!MetaRules.Apply(NewLine, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  result2 = (header).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Header(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(ModeHeader, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(CopyRenameHeader, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(SimilarityHeader, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(IndexHeader, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ModeHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> kind = null;
            OMetaList<HostExpression> mode = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("old file").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("new file").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("deleted file").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("old").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("new").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("deleted").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  kind = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("mode").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Number, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  mode = result2;
                  result2 = (new ModeHeader(kind.As<string>(), mode.As<int>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool CopyRenameHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> transaction = null;
            OMetaList<HostExpression> direction = null;
            OMetaList<HostExpression> filename = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("copy").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("rename").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  transaction = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("from").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("to").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  direction = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Text, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  filename = result2;
                  result2 = (new CopyRenameHeader(transaction.As<string>(), direction.As<string>(), filename.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool SimilarityHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> kind = null;
            OMetaList<HostExpression> index = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("similarity").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("dissimilarity").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  kind = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("index").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Number, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  index = result2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("%").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  result2 = (new SimilarityHeader(kind.As<string>(), index.As<int>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool IndexHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> range = null;
            OMetaList<HostExpression> mode = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("index").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(HashRange, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    range = result3;
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Number, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    mode = result3;
                    result3 = (new IndexHeader(range.As<HashRange>(), mode.As<int>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("index").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(HashRange, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    range = result3;
                    result3 = (new IndexHeader(range.As<HashRange>(), null)).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool BinaryFiles(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> raw = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("Binary files").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Text, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  raw = result2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(Empty, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  result2 = (new BinaryFiles(raw.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ChunksHeader(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> originalFile = null;
            OMetaList<HostExpression> newFile = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("---").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(FileDef, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  originalFile = result2;
                  if (!MetaRules.Apply(NewLine, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("+++").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(FileDef, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  newFile = result2;
                  result2 = (new ChunksHeader(originalFile.As<IFile>(), newFile.As<IFile>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Chunks(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> chunks = null;
            modifiedStream = inputStream;
            if(!MetaRules.Many1(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Apply(Chunk, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  return MetaRules.Success();
                }
            , modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            chunks = result;
            return MetaRules.Success();
        }

        public virtual bool Chunk(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> range = null;
            OMetaList<HostExpression> snippets = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(ChunkRange, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    range = result3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(Snippet, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    snippets = result3;
                    if (!MetaRules.Or(modifiedStream3, out result3, out modifiedStream3,
                      (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                        modifiedStream4 = inputStream4;
                        if (!MetaRules.Apply(NewLine, modifiedStream4, out result4, out modifiedStream4)) {
                          return MetaRules.Fail(out result4, out modifiedStream4);
                        }
                        return MetaRules.Success();
                      }
                      , (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                        modifiedStream4 = inputStream4;
                        if (!MetaRules.Apply(Empty, modifiedStream4, out result4, out modifiedStream4)) {
                          return MetaRules.Fail(out result4, out modifiedStream4);
                        }
                        return MetaRules.Success();
                      }
                      )) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    result3 = (new Chunk(range.As<ChunkRange>(), snippets.ToIEnumerable<ISnippet>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(ChunkRange, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    range = result3;
                    if (!MetaRules.Or(modifiedStream3, out result3, out modifiedStream3,
                    (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                      modifiedStream4 = inputStream4;
                      if (!MetaRules.Apply(NewLine, modifiedStream4, out result4, out modifiedStream4)) {
                        return MetaRules.Fail(out result4, out modifiedStream4);
                      }
                      return MetaRules.Success();
                    }
                    , (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                      modifiedStream4 = inputStream4;
                      if (!MetaRules.Apply(Empty, modifiedStream4, out result4, out modifiedStream4)) {
                        return MetaRules.Fail(out result4, out modifiedStream4);
                      }
                      return MetaRules.Success();
                    }
                    )) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    result3 = (new Chunk(range.As<ChunkRange>(), new List<ISnippet>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ChunkRange(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> originalRange = null;
            OMetaList<HostExpression> newRange = null;
            OMetaList<HostExpression> value = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("@@").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(ChangeRange, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  originalRange = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(ChangeRange, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  newRange = result2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("@@").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(Text, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(Empty, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            result4 = ("").AsHostExpressionList();
                            return MetaRules.Success();
                          }, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  value = result2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(Empty, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  result2 = (new ChunkRange(originalRange.As<ChangeRange>(), newRange.As<ChangeRange>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ChangeRange(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> line = null;
            OMetaList<HostExpression> affected = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Or(modifiedStream3, out result3, out modifiedStream3,
                      (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                        modifiedStream4 = inputStream4;
                        if (!MetaRules.ApplyWithArgs(Token, modifiedStream4, out result4, out modifiedStream4, ("+").AsHostExpressionList())) {
                          return MetaRules.Fail(out result4, out modifiedStream4);
                        }
                        return MetaRules.Success();
                      }
                      , (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                        modifiedStream4 = inputStream4;
                        if (!MetaRules.ApplyWithArgs(Token, modifiedStream4, out result4, out modifiedStream4, ("-").AsHostExpressionList())) {
                          return MetaRules.Fail(out result4, out modifiedStream4);
                        }
                        return MetaRules.Success();
                      }
                      )) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Number, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    line = result3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, (",").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Number, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    affected = result3;
                    result3 = (new ChangeRange(line.As<int>(), affected.As<int>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Or(modifiedStream3, out result3, out modifiedStream3,
                    (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                      modifiedStream4 = inputStream4;
                      if (!MetaRules.ApplyWithArgs(Token, modifiedStream4, out result4, out modifiedStream4, ("+").AsHostExpressionList())) {
                        return MetaRules.Fail(out result4, out modifiedStream4);
                      }
                      return MetaRules.Success();
                    }
                    , (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                      modifiedStream4 = inputStream4;
                      if (!MetaRules.ApplyWithArgs(Token, modifiedStream4, out result4, out modifiedStream4, ("-").AsHostExpressionList())) {
                        return MetaRules.Fail(out result4, out modifiedStream4);
                      }
                      return MetaRules.Success();
                    }
                    )) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Number, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    line = result3;
                    result3 = (new ChangeRange(line.As<int>(), 1)).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Snippet(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(ContextSnippet, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(ModificationSnippet, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(AdditionSnippet, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(SubtractionSnippet, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ContextSnippet(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> lines = null;
            OMetaList<HostExpression> eof = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(ContextLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof = result3;
                    result3 = (new ContextSnippet(lines.ToIEnumerable<ILine>().Concat(new ILine[] { eof.As<ILine>() }))).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(ContextLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    result3 = (new ContextSnippet(lines.ToIEnumerable<ILine>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool AdditionSnippet(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> lines = null;
            OMetaList<HostExpression> eof = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof = result3;
                    result3 = (new AdditionSnippet(lines.ToIEnumerable<ILine>().Concat(new ILine[] { eof.As<ILine>() }))).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    result3 = (new AdditionSnippet(lines.ToIEnumerable<ILine>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool SubtractionSnippet(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> lines = null;
            OMetaList<HostExpression> eof = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof = result3;
                    result3 = (new SubtractionSnippet(lines.ToIEnumerable<ILine>().Concat(new ILine[] { eof.As<ILine>() }))).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    lines = result3;
                    result3 = (new SubtractionSnippet(lines.ToIEnumerable<ILine>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ModificationSnippet(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> subtractions = null;
            OMetaList<HostExpression> eof1 = null;
            OMetaList<HostExpression> additions = null;
            OMetaList<HostExpression> eof2 = null;
            OMetaList<HostExpression> eof = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    subtractions = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof1 = result3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    additions = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof2 = result3;
                    result3 = (new ModificationSnippet(subtractions.ToIEnumerable<ILine>().Concat(new ILine[] { eof1.As<ILine>() }), additions.ToIEnumerable<ILine>().Concat(new ILine[] { eof2.As<ILine>() }))).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    subtractions = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof = result3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    additions = result3;
                    result3 = (new ModificationSnippet(subtractions.ToIEnumerable<ILine>().Concat(new ILine[] { eof.As<ILine>() }), additions.ToIEnumerable<ILine>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    subtractions = result3;
                    if (!MetaRules.Many1(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            return MetaRules.Success();
                          }
                      , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    additions = result3;
                    if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    eof = result3;
                    result3 = (new ModificationSnippet(subtractions.ToIEnumerable<ILine>(), additions.ToIEnumerable<ILine>().Concat(new ILine[] { eof.As<ILine>() }))).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(SubtractionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    subtractions = result3;
                    if (!MetaRules.Many1(
                        (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                          modifiedStream4 = inputStream4;
                          if (!MetaRules.Apply(AdditionLine, modifiedStream4, out result4, out modifiedStream4)) {
                            return MetaRules.Fail(out result4, out modifiedStream4);
                          }
                          return MetaRules.Success();
                        }
                    , modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    additions = result3;
                    result3 = (new ModificationSnippet(subtractions.ToIEnumerable<ILine>(), additions.ToIEnumerable<ILine>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool DiffLines(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> lines = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(DiffLine, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  lines = result2;
                  result2 = (lines).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool DiffLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(ContextLine, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(AdditionLine, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(SubtractionLine, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(NoNewLineAtEOFLine, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool ContextLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> value = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Apply(Space, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Line, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  value = result2;
                  result2 = (new ContextLine(value.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool AdditionLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> value = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("+").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Line, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  value = result2;
                  result2 = (new AdditionLine(value.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool SubtractionLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> value = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("-").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Apply(Line, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  value = result2;
                  result2 = (new SubtractionLine(value.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool NoNewLineAtEOFLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Exactly, modifiedStream3, out result3, out modifiedStream3, ("\\").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("No newline at end of file").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(NewLine, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    result3 = (new NoNewLineAtEOFLine()).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Exactly, modifiedStream3, out result3, out modifiedStream3, ("\\").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Space, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("No newline at end of file").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    result3 = (new NoNewLineAtEOFLine()).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Line(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> value = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Or(modifiedStream2, out result2, out modifiedStream2,
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(Text, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  , (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(
                          (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                            modifiedStream4 = inputStream4;
                            if (!MetaRules.Apply(Empty, modifiedStream4, out result4, out modifiedStream4)) {
                              return MetaRules.Fail(out result4, out modifiedStream4);
                            }
                            result4 = ("").AsHostExpressionList();
                            return MetaRules.Success();
                          }, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }
                  )) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  value = result2;
                  if (!MetaRules.Apply(NewLine, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  result2 = (value.As<string>()).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool FileDef(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> letter = null;
            OMetaList<HostExpression> filename = null;
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.Apply(Letter, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    letter = result3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("/").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.Apply(Text, modifiedStream3, out result3, out modifiedStream3)) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    filename = result3;
                    result3 = (new File(letter.As<char>(), filename.As<string>())).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Token, modifiedStream3, out result3, out modifiedStream3, ("/dev/null").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    result3 = (new NullFile()).AsHostExpressionList();
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool HashRange(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            OMetaList<HostExpression> first = null;
            OMetaList<HostExpression> second = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(LetterOrDigit, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  first = result2;
                  if (!MetaRules.ApplyWithArgs(Token, modifiedStream2, out result2, out modifiedStream2, ("..").AsHostExpressionList())) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(LetterOrDigit, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  second = result2;
                  result2 = (new HashRange(first.As<string>(), second.As<string>())).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool Text(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Many1(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Apply(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Not(
                              (OMetaStream<char> inputStream4, out OMetaList<HostExpression> result4, out OMetaStream<char> modifiedStream4) => {
                                modifiedStream4 = inputStream4;
                                if (!MetaRules.Apply(NewLine, modifiedStream4, out result4, out modifiedStream4)) {
                                  return MetaRules.Fail(out result4, out modifiedStream4);
                                }
                                return MetaRules.Success();
                              }
                          , modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        if (!MetaRules.Apply(Character, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }, modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  return MetaRules.Success();
                }
            , modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public override bool Number(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            Rule<char> __baseRule__ = base.Number;
            OMetaList<HostExpression> ds = null;
            modifiedStream = inputStream;
            if(!MetaRules.Apply(
                (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
                  modifiedStream2 = inputStream2;
                  if (!MetaRules.Many1(
                      (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                        modifiedStream3 = inputStream3;
                        if (!MetaRules.Apply(Digit, modifiedStream3, out result3, out modifiedStream3)) {
                          return MetaRules.Fail(out result3, out modifiedStream3);
                        }
                        return MetaRules.Success();
                      }
                  , modifiedStream2, out result2, out modifiedStream2)) {
                    return MetaRules.Fail(out result2, out modifiedStream2);
                  }
                  ds = result2;
                  result2 = (int.Parse(ds.As<string>(), System.Globalization.CultureInfo.InvariantCulture)).AsHostExpressionList();
                  return MetaRules.Success();
                }, modifiedStream, out result, out modifiedStream))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }

        public virtual bool NewLine(OMetaStream<char> inputStream, out OMetaList<HostExpression> result, out OMetaStream <char> modifiedStream)
        {
            modifiedStream = inputStream;
            if(!MetaRules.Or(modifiedStream, out result, out modifiedStream,
            (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.Apply(
                  (OMetaStream<char> inputStream3, out OMetaList<HostExpression> result3, out OMetaStream<char> modifiedStream3) => {
                    modifiedStream3 = inputStream3;
                    if (!MetaRules.ApplyWithArgs(Exactly, modifiedStream3, out result3, out modifiedStream3, ("\r").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    if (!MetaRules.ApplyWithArgs(Exactly, modifiedStream3, out result3, out modifiedStream3, ("\n").AsHostExpressionList())) {
                      return MetaRules.Fail(out result3, out modifiedStream3);
                    }
                    return MetaRules.Success();
                  }, modifiedStream2, out result2, out modifiedStream2)) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            , (OMetaStream<char> inputStream2, out OMetaList<HostExpression> result2, out OMetaStream<char> modifiedStream2) => {
              modifiedStream2 = inputStream2;
              if (!MetaRules.ApplyWithArgs(Exactly, modifiedStream2, out result2, out modifiedStream2, ("\n").AsHostExpressionList())) {
                return MetaRules.Fail(out result2, out modifiedStream2);
              }
              return MetaRules.Success();
            }
            ))
            {
                return MetaRules.Fail(out result, out modifiedStream);
            }
            return MetaRules.Success();
        }
    }
}
