using MICExtended.Common;
using MICExtended.Models;
using MICExtended.Services;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MICExtended
{
    public partial class Form1 : Form
    {
        AppLogic _al;

        private MainFormViewModel _viewModel = new MainFormViewModel();

        public Form1(AppLogic appLogic) {
            InitializeComponent();
            _al = appLogic;
        }

        private void Form1_Load(object sender, EventArgs e) {
            UpdateCompressionParameter();
            UpdateProgressBar();
        }

        #region UI Updater
        private void UpdateDisplay() {
            UpdateDirectoryTxt();
            UpdateSrcList();
            UpdateDstList();
            UpdateCompressionParameter();
            UpdateProgressBar();
        }

        private void UpdateDirectoryTxt() {
            txtScrDir.Text = _viewModel.SrcDir;
            txtDstDir.Text = _viewModel.DstDir;
            btnOpenDst.Enabled = !string.IsNullOrEmpty(_viewModel.SrcDir);
        }

        private void UpdateSrcList() {
            var srcViewItems = _viewModel.SrcFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.SrcDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                return lv;
            }).ToArray();
            listViewSrc.Items.Clear();
            listViewSrc.Items.AddRange(srcViewItems);
        }

        private void UpdateDstList() {
            var dstViewItems = _viewModel.DstFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.SrcDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                return lv;
            }).ToArray();
            listViewDst.Items.Clear();
            listViewDst.Items.AddRange(dstViewItems);
        }

        private void UpdateCompressionParameter() {
            numQuality.Value 
                = trkQuality.Value 
                = _viewModel.Compression.Quality;

            rbFixedWidth.Checked
                = numFixedWidth.Enabled
                = _viewModel.Compression.Dimension == Dimension.FixedWidth;

            rbNewDimensionPct.Checked
                = numNewDimensionPct.Enabled
                = trkNewDimensionPct.Enabled
                = _viewModel.Compression.Dimension == Dimension.NewDimensionInPct;

            numFixedWidth.Value = _viewModel.Compression.DimensionFixedWidth;
            numNewDimensionPct.Value 
                = trkNewDimensionPct.Value 
                = _viewModel.Compression.DimensionInPct;

            rbCtJpeg.Checked = _viewModel.Compression.ConvertTo == SupportedMimeType.JPEG;
            rbCtPng.Checked = _viewModel.Compression.ConvertTo == SupportedMimeType.PNG;
            rbCtOriginal.Checked = _viewModel.Compression.ConvertTo == SupportedMimeType.ORIGINAL;
        }

        public void UpdateProgressBar() {
            lblProgress.Text = _viewModel.ProgressReport.CurrentTask;
            barProgress.Value = _viewModel.ProgressReport.StepPct;
            barProgress.Update();

            if(_viewModel.ProgressReport.TaskEnd) {
                var confirmResult = MessageBox.Show(_viewModel.ProgressReport.TaskEndMessage, "Success", MessageBoxButtons.OK);
                if(confirmResult == DialogResult.OK) {
                    _viewModel.ProgressReport = new ProgressReport();
                    UpdateProgressBar();
                }
            }
        }
        #endregion

        private void ProgressChanged(object? sender, ProgressReport report) {
            _viewModel.ProgressReport = report;

            UpdateProgressBar();
        }

        #region Event Handlers
        private void btnOpenSrc_Click(object sender, EventArgs e) {
            _viewModel.SrcDir = OpenDirectorySelector();
            if(string.IsNullOrEmpty(_viewModel.SrcDir)) return;

            _viewModel.SrcFiles = _al.GetFileViewModels(_viewModel.SrcDir).ToList();
            _viewModel.DstDir = Path.Combine(_viewModel.SrcDir, Constant.Pathing.COMPRESSED);
            ReloadDstFiles();

            UpdateDisplay();
        }

        private void btnOpenDst_Click(object sender, EventArgs e) {
            _viewModel.DstDir = OpenDirectorySelector();
            if(string.IsNullOrEmpty(_viewModel.DstDir)) return;

            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression).ToList();
            ReloadDstFiles();

            UpdateDisplay();
        }

        private async void btnCompress_Click(object sender, EventArgs e) {
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += ProgressChanged;

            await _al.CompressFiles(_viewModel.SrcFiles, _viewModel.DstFiles, _viewModel.Compression, progress);
        }

        private void trkQuality_Scroll(object sender, EventArgs e) {
            _viewModel.Compression.Quality = trkQuality.Value;

            UpdateCompressionParameter();
        }

        private void numQuality_ValueChanged(object sender, EventArgs e) {
            _viewModel.Compression.Quality = (int)numQuality.Value;

            UpdateCompressionParameter();
        }

        private void rbDimension_CheckedChanged(object sender, EventArgs e) {
            _viewModel.Compression.Dimension = rbFixedWidth.Checked
                ? Dimension.FixedWidth
                : Dimension.NewDimensionInPct;

            UpdateCompressionParameter();
        }

        private void numFixedWidth_ValueChanged(object sender, EventArgs e) {
            _viewModel.Compression.DimensionFixedWidth = (int)numFixedWidth.Value;

            UpdateCompressionParameter();
        }

        private void numNewDimensionPct_ValueChanged(object sender, EventArgs e) {
            _viewModel.Compression.DimensionInPct = (int)numNewDimensionPct.Value;

            UpdateCompressionParameter();
        }

        private void trkNewDimensionPct_Scroll(object sender, EventArgs e) {
            _viewModel.Compression.DimensionInPct = trkNewDimensionPct.Value;

            UpdateCompressionParameter();
        }

        private void rbCt_CheckedChanged(object sender, EventArgs e) {
            _viewModel.Compression.ConvertTo = rbCtJpeg.Checked ? SupportedMimeType.JPEG
                : rbCtPng.Checked ? SupportedMimeType.PNG
                : SupportedMimeType.ORIGINAL;
            ReloadDstFiles();

            UpdateCompressionParameter();
            UpdateDstList();
        }
        #endregion

        #region Shared
        private string OpenDirectorySelector() {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() {
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                return dialog.FileName;
            }

            return string.Empty;
        }

        private void ReloadDstFiles() {
            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression).ToList();
        }
        #endregion

        #region Unused
        private void progressBar1_Click(object sender, EventArgs e) {

        }
        #endregion
    }
}