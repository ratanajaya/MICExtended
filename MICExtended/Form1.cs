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

        }

        private void UpdateDisplay() {
            txtScrDir.Text = _viewModel.SrcDir;
            txtDstDir.Text = _viewModel.DstDir;

            var srcViewItems = _viewModel.SrcFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.SrcDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                return lv;
            }).ToArray();
            listViewSrc.Items.Clear();
            listViewSrc.Items.AddRange(srcViewItems);

            var dstViewItems = _viewModel.DstFiles.Select(a => {
                var lv = new ListViewItem(a.FilePath.Replace(_viewModel.SrcDir, String.Empty));
                lv.SubItems.Add(a.SizeDisplay);
                return lv;
            }).ToArray();
            listViewDst.Items.Clear();
            listViewDst.Items.AddRange(dstViewItems);
        }

        private string OpenDirectorySelector() {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() {
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                return dialog.FileName;
            }

            return string.Empty;
        }

        #region Event Handlers
        private void btnOpenSrc_Click(object sender, EventArgs e) {
            _viewModel.SrcDir = OpenDirectorySelector();
            _viewModel.SrcFiles = _al.GetFileViewModels(_viewModel.SrcDir).ToList();
            _viewModel.DstDir = Path.Combine(_viewModel.SrcDir, Constant.Pathing.COMPRESSED);
            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.SelectionCondition).ToList();

            UpdateDisplay();
        }

        private void btnOpenDst_Click(object sender, EventArgs e) {
            _viewModel.DstDir = OpenDirectorySelector();
            _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, _viewModel.DstDir, _viewModel.SrcFiles, _viewModel.SelectionCondition).ToList();

            UpdateDisplay();

        }

        private void btnCompress_Click(object sender, EventArgs e) {
            _al.CompressFiles(_viewModel.SrcFiles, _viewModel.DstFiles);
        }
        #endregion
    }
}