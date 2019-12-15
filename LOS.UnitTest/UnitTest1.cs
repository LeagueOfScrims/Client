using NUnit.Framework;

namespace LOS.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var test = "Test";
            Assert.AreEqual(test, "Test");
        }
    }
}