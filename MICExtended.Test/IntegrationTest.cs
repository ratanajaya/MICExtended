using MICExtended;
using MICExtended.Common;
using MICExtended.Model;
using MICExtended.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Serilog;

#pragma warning disable CS8618
namespace MICExtended.Test
{
    [TestClass]
    public class IntegrationTest
    {
        AppLogic _al;
        string _libPath;

        [TestInitialize]
        public void Initialize() {
            _libPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "_TestImages");

            var loggerMock = (new Mock<ILogger>()).Object;
            _al = new AppLogic(new IoWrapper(), new ImageCompressor(loggerMock), loggerMock);
        }

        [TestCleanup]
        public void Cleanup() {
            Directory.Delete(_libPath, true);
        }

        [TestMethod]
        public async Task OpeningNewSrcFolderWithFilter() {
            var progressMock = new Mock<IProgress<ProgressReport>>().Object;

            var selection = new SelectionCondition {
                FileTypes = new List<string> { Constant.Extension.JPG, Constant.Extension.PNG },
                UseMinSize = true,
                MinSize = 900
            };

            var files = await _al.GetFileViewModels(_libPath, selection, progressMock);
            var extensions = files.Select(a => a.Extension).ToList();

            Assert.IsTrue(extensions.Contains(Constant.Extension.JPG));
            Assert.IsTrue(extensions.Contains(Constant.Extension.PNG));
            Assert.IsTrue(!extensions.Contains(Constant.Extension.JPEG));
        }

    }
}