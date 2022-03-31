using MICExtended.Helpers;
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

        private void btnOpenSrc_Click(object sender, EventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() {
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                _viewModel.SrcDir = dialog.FileName;
                _viewModel.SrcFiles = _al.GetFileViewModels(dialog.FileName).ToList();
                var defaultDstPath = Path.Combine(dialog.FileName, "compressed");
                _viewModel.DstDir = defaultDstPath;
                _viewModel.DstFiles = _al.GetCompressedFilePreview(_viewModel.SrcDir, defaultDstPath, _viewModel.SrcFiles, _viewModel.SelectionCondition).ToList();

                UpdateDisplay();
            }
        }
    }
}