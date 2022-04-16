using MICExtended.Common;
using MICExtended.Model;
using MICExtended.Service;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Linq;

namespace MICExtended
{
    public partial class Form1 : Form
    {
        AppLogic _al;
        MainFormViewModel _viewModel = new MainFormViewModel();
        //ConfigurationModel _config = new ConfigurationModel();

        #region Initialization
        public Form1(AppLogic al) {
            InitializeComponent();
            _al = al;
        }

        private async void Form1_Load(object sender, EventArgs e) {
            await Initialize();

            UpdateCompressionParameter();
            UpdateProgressBar();

            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private async Task Initialize() {
            var config = await _al.LoadConfiguration();

            _viewModel.Selection = config.Selection;
            _viewModel.Compression = config.Compression;

            clFileType.Items.AddRange(Constant.Extension.ALLOWED.ToArray());
            clFileType.CheckOnClick = true;

            UpdateClFileType();
            UpdateFileSelectionMinParameter();
            UpdateFileSelectionMinParameterValue();
        }
        #endregion

        #region UI Updater
        private void UpdateDirectoryTxt() {
            txtScrDir.Text = _viewModel.SrcDir;
            txtDstDir.Text = _viewModel.DstDir;
            btnOpenDst.Enabled = !string.IsNullOrEmpty(_viewModel.SrcDir);
        }

        private void UpdateSrcList() {
            var srcViewItems = _viewModel.SrcFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.SrcDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                lv.SubItems.Add(a.DimensionDisplay);
                lv.SubItems.Add(a.BytesPer100PixelDisplay);
                return lv;
            }).ToArray();
            listViewSrc.Items.Clear();
            listViewSrc.Items.AddRange(srcViewItems);
        }

        private void UpdateDstList() {
            var dstViewItems = _viewModel.DstFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.DstDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                lv.SubItems.Add(a.DimensionDisplay);
                lv.SubItems.Add(a.BytesPer100PixelDisplay);
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

        private void UpdateChkFileTypeAll() {
            chkFileTypeAll.Checked = _viewModel.Selection.CheckAllFile;
        }

        private void UpdateClFileType() {
            for(int i = 0; i < clFileType.Items.Count; i++) {
                clFileType.SetItemChecked(i, _viewModel.Selection.FileTypes.Contains(clFileType.Items[i]));
            }
        }

        private void UpdateFileSelectionMinParameter() {
            chkMinSize.Checked = _viewModel.Selection.UseMinSize;
            chkMinB100.Checked = _viewModel.Selection.UseMinB100;
            numMinSize.Enabled = _viewModel.Selection.UseMinSize;
            numMinB100.Enabled = _viewModel.Selection.UseMinB100;
        }

        private void UpdateFileSelectionMinParameterValue() {
            numMinSize.Value = _viewModel.Selection.MinSize;
            numMinB100.Value = _viewModel.Selection.MinB100;
        }

        private void UpdateProgressBar() {
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

        private void ProgressChanged(object? sender, ProgressReport report) {
            _viewModel.ProgressReport = report;

            UpdateProgressBar();
        }
        #endregion

        #region Event Handlers
        private void btnOpenSrc_Click(object sender, EventArgs e) {
            _viewModel.SrcDir = OpenDirectorySelector();
            if(string.IsNullOrEmpty(_viewModel.SrcDir)) return;

            _viewModel.DstDir = Path.Combine(_viewModel.SrcDir, Constant.Pathing.COMPRESSED);

            _al.ClearCache();

            UpdateDirectoryTxt();
            ReloadFiles();
        }

        private void btnOpenDst_Click(object sender, EventArgs e) {
            _viewModel.DstDir = OpenDirectorySelector();
            if(string.IsNullOrEmpty(_viewModel.DstDir)) return;

            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression).ToList();
            UpdateDirectoryTxt();
            ReloadDstFiles();
        }

        private async void btnCompress_Click(object sender, EventArgs e) {
            var progress = new Progress<ProgressReport>();
            progress.ProgressChanged += ProgressChanged;

            await _al.CompressFiles(_viewModel.SrcFiles, _viewModel.DstFiles, _viewModel.Compression, progress);

            _viewModel.DstFiles = _al.LoadFileDetail(_viewModel.DstFiles).ToList();
            UpdateDstList();
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
        }

        private void clFileType_SelectedIndexChanged(object sender, EventArgs e) {
            _viewModel.Selection.FileTypes = clFileType.CheckedItems.Cast<object>().Select(a => clFileType.GetItemText(a)).ToList();

            _viewModel.Selection.CheckAllFile = _viewModel.Selection.FileTypes.Count == clFileType.Items.Count;

            UpdateChkFileTypeAll();
            ReloadFiles();
        }

        private void chkFileTypeAll_Click(object sender, EventArgs e) {
            _viewModel.Selection.CheckAllFile = chkFileTypeAll.Checked;
            _viewModel.Selection.FileTypes = _viewModel.Selection.CheckAllFile
                ? clFileType.Items.Cast<object>().Select(a => clFileType.GetItemText(a)).ToList()
                : new List<string>();

            UpdateClFileType();
            ReloadFiles();
        }

        private void minParameter_CheckedChanged(object sender, EventArgs e) {
            _viewModel.Selection.UseMinSize = chkMinSize.Checked;
            _viewModel.Selection.UseMinB100 = chkMinB100.Checked;

            UpdateFileSelectionMinParameter();
            ReloadFiles();
        }

        private void numMinSize_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.MinSize = (int)numMinSize.Value;
            ReloadFiles();
        }

        private void numMinB100_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.MinB100 = (int)numMinB100.Value;
            ReloadFiles();
        }

        private async void Form1_FormClosing(object? sender, FormClosingEventArgs e) {
            await _al.SaveConfiguration(_viewModel);
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

        private void ReloadFiles() {
            ReloadSrcFiles();
            ReloadDstFiles();
        }

        private void ReloadSrcFiles() {
            if(string.IsNullOrEmpty(_viewModel.SrcDir)) return;

            _viewModel.SrcFiles = _al.GetFileViewModels(_viewModel.SrcDir, _viewModel.Selection).ToList();
            UpdateSrcList();
        }

        private void ReloadDstFiles() {
            if(string.IsNullOrEmpty(_viewModel.DstDir)) return;

            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression).ToList();
            UpdateDstList();
        }
        #endregion

        #region Unused
        private void progressBar1_Click(object sender, EventArgs e) {

        }

        private void listViewSrc_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void listViewSrc_ColumnClick(object sender, ColumnClickEventArgs e) {
            //TODO column sorting
        }
        #endregion
    }
}