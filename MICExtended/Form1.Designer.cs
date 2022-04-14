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
            this.listViewDst = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btnCompress = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.clFileType = new System.Windows.Forms.CheckedListBox();
            this.chkFileTypeAll = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbCtOriginal = new System.Windows.Forms.RadioButton();
            this.rbCtPng = new System.Windows.Forms.RadioButton();
            this.rbCtJpeg = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.trkNewDimensionPct = new System.Windows.Forms.TrackBar();
            this.numNewDimensionPct = new System.Windows.Forms.NumericUpDown();
            this.numFixedWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbNewDimensionPct = new System.Windows.Forms.RadioButton();
            this.rbFixedWidth = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.numQuality = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.trkQuality = new System.Windows.Forms.TrackBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkNewDimensionPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewDimensionPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedWidth)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).BeginInit();
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
            this.btnOpenDst.Click += new System.EventHandler(this.btnOpenDst_Click);
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
            // listViewDst
            // 
            this.listViewDst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewDst.Location = new System.Drawing.Point(420, 219);
            this.listViewDst.Name = "listViewDst";
            this.listViewDst.Size = new System.Drawing.Size(400, 219);
            this.listViewDst.TabIndex = 4;
            this.listViewDst.UseCompatibleStateImageBehavior = false;
            this.listViewDst.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File";
            this.columnHeader1.Width = 320;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 80;
            // 
            // btnCompress
            // 
            this.btnCompress.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCompress.Location = new System.Drawing.Point(720, 449);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(100, 40);
            this.btnCompress.TabIndex = 5;
            this.btnCompress.Text = "COMPRESS";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.clFileType);
            this.groupBox3.Controls.Add(this.chkFileTypeAll);
            this.groupBox3.Location = new System.Drawing.Point(10, 69);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 144);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File Selection";
            // 
            // clFileType
            // 
            this.clFileType.FormattingEnabled = true;
            this.clFileType.Location = new System.Drawing.Point(6, 40);
            this.clFileType.Name = "clFileType";
            this.clFileType.Size = new System.Drawing.Size(111, 94);
            this.clFileType.TabIndex = 11;
            this.clFileType.SelectedIndexChanged += new System.EventHandler(this.clFileType_SelectedIndexChanged);
            // 
            // chkFileTypeAll
            // 
            this.chkFileTypeAll.AutoSize = true;
            this.chkFileTypeAll.Location = new System.Drawing.Point(9, 19);
            this.chkFileTypeAll.Name = "chkFileTypeAll";
            this.chkFileTypeAll.Size = new System.Drawing.Size(114, 19);
            this.chkFileTypeAll.TabIndex = 12;
            this.chkFileTypeAll.Text = "Select All / None";
            this.chkFileTypeAll.UseVisualStyleBackColor = true;
            this.chkFileTypeAll.Click += new System.EventHandler(this.chkFileTypeAll_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.trkNewDimensionPct);
            this.groupBox4.Controls.Add(this.numNewDimensionPct);
            this.groupBox4.Controls.Add(this.numFixedWidth);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numQuality);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.trkQuality);
            this.groupBox4.Location = new System.Drawing.Point(420, 69);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(400, 144);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Compression Parameter";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbCtOriginal);
            this.groupBox6.Controls.Add(this.rbCtPng);
            this.groupBox6.Controls.Add(this.rbCtJpeg);
            this.groupBox6.Location = new System.Drawing.Point(77, 103);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(271, 27);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            // 
            // rbCtOriginal
            // 
            this.rbCtOriginal.AutoSize = true;
            this.rbCtOriginal.Location = new System.Drawing.Point(154, 8);
            this.rbCtOriginal.Name = "rbCtOriginal";
            this.rbCtOriginal.Size = new System.Drawing.Size(67, 19);
            this.rbCtOriginal.TabIndex = 2;
            this.rbCtOriginal.TabStop = true;
            this.rbCtOriginal.Text = "Original";
            this.rbCtOriginal.UseVisualStyleBackColor = true;
            this.rbCtOriginal.CheckedChanged += new System.EventHandler(this.rbCt_CheckedChanged);
            // 
            // rbCtPng
            // 
            this.rbCtPng.AutoSize = true;
            this.rbCtPng.Location = new System.Drawing.Point(80, 8);
            this.rbCtPng.Name = "rbCtPng";
            this.rbCtPng.Size = new System.Drawing.Size(49, 19);
            this.rbCtPng.TabIndex = 1;
            this.rbCtPng.TabStop = true;
            this.rbCtPng.Text = "PNG";
            this.rbCtPng.UseVisualStyleBackColor = true;
            this.rbCtPng.CheckedChanged += new System.EventHandler(this.rbCt_CheckedChanged);
            // 
            // rbCtJpeg
            // 
            this.rbCtJpeg.AutoSize = true;
            this.rbCtJpeg.Location = new System.Drawing.Point(6, 8);
            this.rbCtJpeg.Name = "rbCtJpeg";
            this.rbCtJpeg.Size = new System.Drawing.Size(50, 19);
            this.rbCtJpeg.TabIndex = 0;
            this.rbCtJpeg.TabStop = true;
            this.rbCtJpeg.Text = "JPEG";
            this.rbCtJpeg.UseVisualStyleBackColor = true;
            this.rbCtJpeg.CheckedChanged += new System.EventHandler(this.rbCt_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Convert To";
            // 
            // trkNewDimensionPct
            // 
            this.trkNewDimensionPct.Location = new System.Drawing.Point(192, 73);
            this.trkNewDimensionPct.Maximum = 100;
            this.trkNewDimensionPct.Name = "trkNewDimensionPct";
            this.trkNewDimensionPct.Size = new System.Drawing.Size(158, 45);
            this.trkNewDimensionPct.TabIndex = 8;
            this.trkNewDimensionPct.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkNewDimensionPct.Scroll += new System.EventHandler(this.trkNewDimensionPct_Scroll);
            // 
            // numNewDimensionPct
            // 
            this.numNewDimensionPct.Location = new System.Drawing.Point(354, 73);
            this.numNewDimensionPct.Name = "numNewDimensionPct";
            this.numNewDimensionPct.Size = new System.Drawing.Size(40, 23);
            this.numNewDimensionPct.TabIndex = 7;
            this.numNewDimensionPct.ValueChanged += new System.EventHandler(this.numNewDimensionPct_ValueChanged);
            // 
            // numFixedWidth
            // 
            this.numFixedWidth.Location = new System.Drawing.Point(83, 73);
            this.numFixedWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numFixedWidth.Name = "numFixedWidth";
            this.numFixedWidth.Size = new System.Drawing.Size(88, 23);
            this.numFixedWidth.TabIndex = 6;
            this.numFixedWidth.ValueChanged += new System.EventHandler(this.numFixedWidth_ValueChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbNewDimensionPct);
            this.groupBox5.Controls.Add(this.rbFixedWidth);
            this.groupBox5.Location = new System.Drawing.Point(77, 40);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(271, 27);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            // 
            // rbNewDimensionPct
            // 
            this.rbNewDimensionPct.AutoSize = true;
            this.rbNewDimensionPct.Location = new System.Drawing.Point(115, 8);
            this.rbNewDimensionPct.Name = "rbNewDimensionPct";
            this.rbNewDimensionPct.Size = new System.Drawing.Size(135, 19);
            this.rbNewDimensionPct.TabIndex = 1;
            this.rbNewDimensionPct.TabStop = true;
            this.rbNewDimensionPct.Text = "New Dimension in %";
            this.rbNewDimensionPct.UseVisualStyleBackColor = true;
            this.rbNewDimensionPct.CheckedChanged += new System.EventHandler(this.rbDimension_CheckedChanged);
            // 
            // rbFixedWidth
            // 
            this.rbFixedWidth.AutoSize = true;
            this.rbFixedWidth.Location = new System.Drawing.Point(6, 8);
            this.rbFixedWidth.Name = "rbFixedWidth";
            this.rbFixedWidth.Size = new System.Drawing.Size(88, 19);
            this.rbFixedWidth.TabIndex = 0;
            this.rbFixedWidth.TabStop = true;
            this.rbFixedWidth.Text = "Fixed Width";
            this.rbFixedWidth.UseVisualStyleBackColor = true;
            this.rbFixedWidth.CheckedChanged += new System.EventHandler(this.rbDimension_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dimension";
            // 
            // numQuality
            // 
            this.numQuality.Location = new System.Drawing.Point(354, 18);
            this.numQuality.Name = "numQuality";
            this.numQuality.Size = new System.Drawing.Size(40, 23);
            this.numQuality.TabIndex = 3;
            this.numQuality.ValueChanged += new System.EventHandler(this.numQuality_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quality";
            // 
            // trkQuality
            // 
            this.trkQuality.Location = new System.Drawing.Point(77, 20);
            this.trkQuality.Maximum = 100;
            this.trkQuality.Name = "trkQuality";
            this.trkQuality.Size = new System.Drawing.Size(273, 45);
            this.trkQuality.TabIndex = 0;
            this.trkQuality.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkQuality.Scroll += new System.EventHandler(this.trkQuality_Scroll);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(10, 445);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(61, 15);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "TaskName";
            // 
            // barProgress
            // 
            this.barProgress.Location = new System.Drawing.Point(10, 466);
            this.barProgress.Maximum = 1000;
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(700, 23);
            this.barProgress.Step = 1;
            this.barProgress.TabIndex = 7;
            this.barProgress.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 501);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.listViewDst);
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
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkNewDimensionPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewDimensionPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedWidth)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ListView listViewDst;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button btnCompress;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label1;
        private TrackBar trkQuality;
        private NumericUpDown numQuality;
        private GroupBox groupBox5;
        private RadioButton rbNewDimensionPct;
        private RadioButton rbFixedWidth;
        private Label label2;
        private TrackBar trkNewDimensionPct;
        private NumericUpDown numNewDimensionPct;
        private NumericUpDown numFixedWidth;
        private Label label3;
        private GroupBox groupBox6;
        private RadioButton rbCtOriginal;
        private RadioButton rbCtPng;
        private RadioButton rbCtJpeg;
        private Label lblProgress;
        private ProgressBar barProgress;
        private CheckedListBox clFileType;
        private CheckBox chkFileTypeAll;
    }
}