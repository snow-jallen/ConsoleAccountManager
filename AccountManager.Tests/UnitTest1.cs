using NUnit.Framework;

namespace AccountManager.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShortenTo7Chars()
        {
            var actual = Program.ShortenString("0123456789", 7);
            var expected = "0123...";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShortenTo1Chars()
        {
            var actual = Program.ShortenString("0123456789", 1);
            var expected = "0";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShortenToNegativeChars()
        {
            var actual = Program.ShortenString("0123456789", -1);
            var expected = "";
            Assert.AreEqual(expected, actual);
        }
    }
}