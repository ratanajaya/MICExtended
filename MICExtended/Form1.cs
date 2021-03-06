using MICExtended.Common;
using MICExtended.Model;
using MICExtended.Service;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Linq;

namespace MICExtended
{
#pragma warning disable CS8618
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    public partial class Form1 : Form
    {
        AppSettingJson _appSetting;
        AppLogic _al;
        Form1ViewModel _viewModel;
        Progress<ProgressReport> _progress;

        #region Initialization
        public Form1(AppLogic al, AppSettingJson appSetting) {
            InitializeComponent();
            _al = al;
            _appSetting = appSetting;
        }

        private async void Form1_Load(object sender, EventArgs e) {
            var persistentState = await _al.LoadState();
            _viewModel = new Form1ViewModel {
                Selection = persistentState.Selection,
                Compression = persistentState.Compression,
            };

            _progress = new Progress<ProgressReport>();
            _progress.ProgressChanged += async (sender, report) => {
                _viewModel.ProgressReport = report;
                await UpdateProgressBar();
            };
            this.FormClosing += new FormClosingEventHandler(async (sender, report) => {
                await _al.SaveState(_viewModel);
            });

            clFileType.Items.AddRange(_appSetting.AllowedExtensions);
            clFileType.CheckOnClick = true;

            dtModifiedFrom.Format = dtModifiedTo.Format = DateTimePickerFormat.Custom;
            dtModifiedFrom.CustomFormat = dtModifiedTo.CustomFormat = " yyyy/MM/dd";

            tmModifiedFrom.Format = tmModifiedTo.Format = DateTimePickerFormat.Custom;
            tmModifiedFrom.CustomFormat = tmModifiedTo.CustomFormat = " HH:mm:ss";

            UpdateDirectoryDisplays();
            UpdateClFileType();
            UpdateFileSelectionChk();
            UpdateFileSelectionValue();

            UpdateCompressionParameter();
            await UpdateProgressBar();

            this.AllowDrop = true;
        }
        #endregion

        #region UI Updater
        private void UpdateDirectoryDisplays() {
            txtScrDir.Text = _viewModel.SrcDir;
            txtDstDir.Text = _viewModel.DstDir;
            chkReplaceOriginalFile.Checked = _viewModel.Compression.ReplaceOriginal;

            btnOpenDst.Enabled = !_viewModel.Compression.ReplaceOriginal && !string.IsNullOrEmpty(_viewModel.SrcDir);
            txtDstDir.Enabled = !_viewModel.Compression.ReplaceOriginal;
        }

        private ListViewItem[] GetListViewItems(List<FileModel> files, string rootPath) {
            string rootPathWithSlash = $"{rootPath}{Path.DirectorySeparatorChar}";

            return files.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(rootPathWithSlash, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                lv.SubItems.Add(a.DimensionDisplay);
                lv.SubItems.Add(a.BytesPer100PixelDisplay);
                lv.SubItems.Add(a.ModifiedDate.ToString("yyyy/MM/dd  HH:mm:ss"));
                return lv;
            }).ToArray();
        }

        private void UpdateSrcList() {
            listViewSrc.Items.Clear();
            listViewSrc.Items.AddRange(GetListViewItems(_viewModel.SrcFiles, _viewModel.SrcDir));
        }

        private void UpdateDstList() {
            listViewDst.Items.Clear();
            listViewDst.Items.AddRange(GetListViewItems(_viewModel.DstFiles, _viewModel.DstDir));
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

        private void UpdateFileSelectionChk() {
            chkMinSize.Checked = _viewModel.Selection.UseMinSize;
            numMinSize.Enabled = _viewModel.Selection.UseMinSize;

            chkMinB100.Checked = _viewModel.Selection.UseMinB100;
            numMinB100.Enabled = _viewModel.Selection.UseMinB100;

            chkSkipCompressed.Checked = _viewModel.Selection.SkipCompressed;

            chkModifiedFrom.Checked = _viewModel.Selection.UseModifiedDateFrom;
            dtModifiedFrom.Enabled = _viewModel.Selection.UseModifiedDateFrom;
            tmModifiedFrom.Enabled = _viewModel.Selection.UseModifiedDateFrom;

            chkModifiedTo.Checked = _viewModel.Selection.UseModifiedDateTo;
            dtModifiedTo.Enabled = _viewModel.Selection.UseModifiedDateTo;
            tmModifiedTo.Enabled = _viewModel.Selection.UseModifiedDateTo;
        }

        private void UpdateFileSelectionValue() {
            numMinSize.Value = _viewModel.Selection.MinSize;
            numMinB100.Value = _viewModel.Selection.MinB100;

            var dtFrom = _viewModel.Selection.ModifiedDateFrom.Clamp(Constant.MIN_DATE, Constant.MAX_DATE);
            var dtTo = _viewModel.Selection.ModifiedDateTo.Clamp(Constant.MIN_DATE, Constant.MAX_DATE);
            dtModifiedFrom.Value = dtFrom.Date;
            dtModifiedTo.Value = dtTo.Date;
            tmModifiedFrom.Value = dtFrom;
            tmModifiedTo.Value = dtTo;
        }

        private async Task UpdateProgressBar() {
            lblProgress.Text = _viewModel.ProgressReport.CurrentTask;
            barProgress.Value = _viewModel.ProgressReport.StepPct;
            barProgress.Update();

            if(_viewModel.ProgressReport.TaskEnd) {
                if(_viewModel.ProgressReport.ShowPopup) {
                    var confirmResult = MessageBox.Show(_viewModel.ProgressReport.TaskEndMessage, "Success", MessageBoxButtons.OK);
                    if(confirmResult == DialogResult.OK) { }
                }
                else
                    await Task.Delay(1000);

                lblProgress.Text = _viewModel.ProgressReport.TaskEndMessage;
                barProgress.Value = 0;
                barProgress.Update();

                //_viewModel.ProgressReport = new ProgressReport {
                //    CurrentTask = _viewModel.ProgressReport.TaskEndMessage
                //};

                //await UpdateProgressBar();
            }
        }
        #endregion

        #region UI Blocker
        private void Block() { SetControlInteractability(false); }
        private void Unblock() { SetControlInteractability(true); }

        private void SetControlInteractability(bool enabled) {
            grpSrcPath.Enabled = enabled;
            grpDstPath.Enabled = enabled;
            grpCompression.Enabled = enabled;
            grpSelection.Enabled = enabled;
            btnCompress.Enabled = enabled;
        }
        #endregion

        #region Event Handlers
        private async void btnOpenSrc_Click(object sender, EventArgs e) {
            var path = OpenDirectorySelector();
            if(string.IsNullOrEmpty(path)) return;
            
            Block();

            await SetSrcDir(path);

            Unblock();
        }

        private void btnOpenDst_Click(object sender, EventArgs e) {
            var path = OpenDirectorySelector();
            if(string.IsNullOrEmpty(path)) return;

            SetDstDir(path);
        }

        private void chkReplaceOriginalFile_CheckedChanged(object sender, EventArgs e) {
            _viewModel.Compression.ReplaceOriginal = chkReplaceOriginalFile.Checked;
            _viewModel.DstDir = GetDefaultDstPath(_viewModel.SrcDir, _viewModel.SrcDir, _viewModel.DstDir, _viewModel.Compression.ReplaceOriginal);

            UpdateDirectoryDisplays();
            ReloadDstFiles();
        }

        private async void btnCompress_Click(object sender, EventArgs e) {
            Block();

            await Task.Run(() => _al.CompressFiles(_viewModel.SrcFiles, _viewModel.DstFiles, _viewModel.Compression, _progress));

            _viewModel.DstFiles = _al.LoadFileDetail(_viewModel.DstFiles).ToList();
            UpdateDstList();

            Unblock();
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

        private async void clFileType_SelectedIndexChanged(object sender, EventArgs e) {
            _viewModel.Selection.FileTypes = clFileType.CheckedItems.Cast<object>().Select(a => clFileType.GetItemText(a)).ToList();

            _viewModel.Selection.CheckAllFile = _viewModel.Selection.FileTypes.Count == clFileType.Items.Count;

            UpdateChkFileTypeAll();
            await ReloadFiles();
        }

        private async void chkFileTypeAll_Click(object sender, EventArgs e) {
            _viewModel.Selection.CheckAllFile = chkFileTypeAll.Checked;
            _viewModel.Selection.FileTypes = _viewModel.Selection.CheckAllFile
                ? clFileType.Items.Cast<object>().Select(a => clFileType.GetItemText(a)).ToList()
                : new List<string>();

            UpdateClFileType();
            await ReloadFiles();
        }

        private async void selectionChk_Click(object sender, EventArgs e) {
            _viewModel.Selection.UseMinSize = chkMinSize.Checked;
            _viewModel.Selection.UseMinB100 = chkMinB100.Checked;

            _viewModel.Selection.UseModifiedDateFrom = chkModifiedFrom.Checked;
            _viewModel.Selection.UseModifiedDateTo = chkModifiedTo.Checked;

            UpdateFileSelectionChk();
            await ReloadFiles();
        }

        private async void numMinSize_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.MinSize = (int)numMinSize.Value;
            await ReloadFiles();
        }

        private async void numMinB100_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.MinB100 = (int)numMinB100.Value;
            await ReloadFiles();
        }

        private async void chkSkipCompressed_CheckedChanged(object sender, EventArgs e) {
            _viewModel.Selection.SkipCompressed = chkSkipCompressed.Checked;
            await ReloadFiles();
        }

        private async void dtModifiedFrom_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.ModifiedDateFrom = dtModifiedFrom.Value.Date + tmModifiedFrom.Value.TimeOfDay;
            await ReloadFiles();
        }

        private async void dtModifiedTo_ValueChanged(object sender, EventArgs e) {
            _viewModel.Selection.ModifiedDateTo = dtModifiedTo.Value.Date + tmModifiedTo.Value.TimeOfDay;
            await ReloadFiles();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if(e.Data.GetDataPresent(DataFormats.FileDrop) && Directory.Exists(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
                e.Effect = DragDropEffects.Copy;
        }

        async void Form1_DragDrop(object sender, DragEventArgs e) {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if(!Directory.Exists(path)) return;

            Block();

            await SetSrcDir(path);

            Unblock();
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

        private async Task ReloadFiles() {
            await ReloadSrcFiles();
            ReloadDstFiles();
        }

        private async Task ReloadSrcFiles() {
            if(string.IsNullOrEmpty(_viewModel.SrcDir)) return;

            _viewModel.SrcFiles = await _al.GetFileViewModels(_viewModel.SrcDir, _viewModel.Selection, _progress);
            UpdateSrcList();
        }

        private void ReloadDstFiles() {
            if(string.IsNullOrEmpty(_viewModel.DstDir)) return;

            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression);
            UpdateDstList();
        }

        private string GetDefaultDstPath(string srcDir, string oldSrcDir, string dstDir, bool replaceOriginal) {
            if(string.IsNullOrEmpty(srcDir)) return string.Empty;
            if(replaceOriginal) return srcDir;
            if(!dstDir.Contains(oldSrcDir) && !string.IsNullOrEmpty(dstDir)) return dstDir;
            return Path.Combine(srcDir, Constant.Pathing.COMPRESSED);
        }

        async Task SetSrcDir(string path) {
            var oldSrcDir = _viewModel.SrcDir;
            _viewModel.SrcDir = path;

            _viewModel.DstDir = GetDefaultDstPath(_viewModel.SrcDir, oldSrcDir, _viewModel.DstDir, _viewModel.Compression.ReplaceOriginal);

            _al.ClearCache();

            UpdateDirectoryDisplays();
            await ReloadFiles();
        }

        void SetDstDir(string path) {
            _viewModel.DstDir = path;

            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.DstDir, _viewModel.SrcFiles, _viewModel.Compression);
            UpdateDirectoryDisplays();
            ReloadDstFiles();
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