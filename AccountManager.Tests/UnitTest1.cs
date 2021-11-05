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
    }
}