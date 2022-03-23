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

            var lvi = _viewModel.SrcFileInfos.Select(a => {
                var lv = new ListViewItem(a.FullName);
                lv.SubItems.Add(a.Length.ToString());
                return lv;
            }).ToArray();
            listViewSrc.Items.Clear();
            listViewSrc.Items.AddRange(lvi);
        }

        private void btnOpenSrc_Click(object sender, EventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() {
                IsFolderPicker = true
            };
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                _viewModel.SrcDir = dialog.FileName;
                _viewModel.SrcFileInfos = _al.GetFileInfos(dialog.FileName);

                UpdateDisplay();
            }
        }
    }
}