using MICExtended;
using MICExtended.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;

namespace MICExtended.Test
{
    [TestClass]
    public class IntegrationTest
    {
        AppLogic _al;

        [TestInitialize]
        public void TestInitialize() {
            var loggerMock = (new Mock<ILogger>()).Object;

            _al = new AppLogic(new IoWrapper(), new ImageCompressor(loggerMock), loggerMock);
        }

        [TestMethod]
        public void FooBar() {
            int x = 1 + 1;

            Assert.AreEqual(x, 2);
        }
    }
}