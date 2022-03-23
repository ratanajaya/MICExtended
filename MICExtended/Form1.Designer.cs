namespace MICExtended
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenSrc = new System.Windows.Forms.Button();
            this.txtScrDir = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpenDst = new System.Windows.Forms.Button();
            this.txtDstDir = new System.Windows.Forms.TextBox();
            this.listViewSrc = new System.Windows.Forms.ListView();
            this.colFile = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenSrc);
            this.groupBox1.Controls.Add(this.txtScrDir);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Open Directory Containing Images to Compress";
            // 
            // btnOpenSrc
            // 
            this.btnOpenSrc.Location = new System.Drawing.Point(364, 21);
            this.btnOpenSrc.Name = "btnOpenSrc";
            this.btnOpenSrc.Size = new System.Drawing.Size(30, 23);
            this.btnOpenSrc.TabIndex = 1;
            this.btnOpenSrc.Text = "...";
            this.btnOpenSrc.UseVisualStyleBackColor = true;
            this.btnOpenSrc.Click += new System.EventHandler(this.btnOpenSrc_Click);
            // 
            // txtScrDir
            // 
            this.txtScrDir.Location = new System.Drawing.Point(6, 22);
            this.txtScrDir.Name = "txtScrDir";
            this.txtScrDir.Size = new System.Drawing.Size(350, 23);
            this.txtScrDir.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenDst);
            this.groupBox2.Controls.Add(this.txtDstDir);
            this.groupBox2.Location = new System.Drawing.Point(420, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 53);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Save Compressed Image To";
            // 
            // btnOpenDst
            // 
            this.btnOpenDst.Location = new System.Drawing.Point(364, 22);
            this.btnOpenDst.Name = "btnOpenDst";
            this.btnOpenDst.Size = new System.Drawing.Size(30, 23);
            this.btnOpenDst.TabIndex = 1;
            this.btnOpenDst.Text = "...";
            this.btnOpenDst.UseVisualStyleBackColor = true;
            // 
            // txtDstDir
            // 
            this.txtDstDir.Location = new System.Drawing.Point(6, 22);
            this.txtDstDir.Name = "txtDstDir";
            this.txtDstDir.Size = new System.Drawing.Size(350, 23);
            this.txtDstDir.TabIndex = 0;
            // 
            // listViewSrc
            // 
            this.listViewSrc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFile,
            this.colSize});
            this.listViewSrc.Location = new System.Drawing.Point(10, 219);
            this.listViewSrc.Name = "listViewSrc";
            this.listViewSrc.Size = new System.Drawing.Size(400, 219);
            this.listViewSrc.TabIndex = 3;
            this.listViewSrc.UseCompatibleStateImageBehavior = false;
            this.listViewSrc.View = System.Windows.Forms.View.Details;
            // 
            // colFile
            // 
            this.colFile.Text = "File";
            this.colFile.Width = 320;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 80;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 450);
            this.Controls.Add(this.listViewSrc);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Mass Image Compressor Extended";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private Button btnOpenSrc;
        private TextBox txtScrDir;
        private GroupBox groupBox2;
        private Button btnOpenDst;
        private TextBox txtDstDir;
        private ListView listViewSrc;
        private ColumnHeader colFile;
        private ColumnHeader colSize;
    }
}