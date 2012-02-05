using NUnit.Framework;
using SharpDiff.Parsers.GitDiff;

namespace SharpDiff.Tests
{
    [TestFixture]
    public class FormatTests : AbstractParserTestFixture
    {
        [Test]
        public void FormatParsed()
        {
            var result = Parse<DiffFormatType>("--git", x => x.DiffFormatType);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("git"));
        }
    }
}