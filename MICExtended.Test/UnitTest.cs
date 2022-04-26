using Microsoft.VisualStudio.TestTools.UnitTesting;
using MICExtended.Common;
using MICExtended.Model;
using MICExtended.Service;
using Moq;
using Serilog;

namespace MICExtended.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void FooBar() {
            int x = 1 + 1;

            Assert.AreEqual(x, 2);
        }
    }
}
