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
            this.grpSrcPath = new System.Windows.Forms.GroupBox();
            this.btnOpenSrc = new System.Windows.Forms.Button();
            this.txtScrDir = new System.Windows.Forms.TextBox();
            this.grpDstPath = new System.Windows.Forms.GroupBox();
            this.chkReplaceOriginalFile = new System.Windows.Forms.CheckBox();
            this.btnOpenDst = new System.Windows.Forms.Button();
            this.txtDstDir = new System.Windows.Forms.TextBox();
            this.listViewSrc = new System.Windows.Forms.ListView();
            this.srcColFile = new System.Windows.Forms.ColumnHeader();
            this.srcColSize = new System.Windows.Forms.ColumnHeader();
            this.srcColDimension = new System.Windows.Forms.ColumnHeader();
            this.srcColBytesPerPixel = new System.Windows.Forms.ColumnHeader();
            this.btnCompress = new System.Windows.Forms.Button();
            this.grpSelection = new System.Windows.Forms.GroupBox();
            this.numMinB100 = new System.Windows.Forms.NumericUpDown();
            this.numMinSize = new System.Windows.Forms.NumericUpDown();
            this.chkMinB100 = new System.Windows.Forms.CheckBox();
            this.chkMinSize = new System.Windows.Forms.CheckBox();
            this.clFileType = new System.Windows.Forms.CheckedListBox();
            this.chkFileTypeAll = new System.Windows.Forms.CheckBox();
            this.grpCompression = new System.Windows.Forms.GroupBox();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewDst = new System.Windows.Forms.ListView();
            this.dstColFile = new System.Windows.Forms.ColumnHeader();
            this.dstColSize = new System.Windows.Forms.ColumnHeader();
            this.dstColDimension = new System.Windows.Forms.ColumnHeader();
            this.dstColBytesPerPixel = new System.Windows.Forms.ColumnHeader();
            this.grpSrcPath.SuspendLayout();
            this.grpDstPath.SuspendLayout();
            this.grpSelection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinB100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).BeginInit();
            this.grpCompression.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkNewDimensionPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewDimensionPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedWidth)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSrcPath
            // 
            this.grpSrcPath.Controls.Add(this.btnOpenSrc);
            this.grpSrcPath.Controls.Add(this.txtScrDir);
            this.grpSrcPath.Location = new System.Drawing.Point(10, 10);
            this.grpSrcPath.Name = "grpSrcPath";
            this.grpSrcPath.Size = new System.Drawing.Size(400, 53);
            this.grpSrcPath.TabIndex = 0;
            this.grpSrcPath.TabStop = false;
            this.grpSrcPath.Text = "Open Directory Containing Images to Compress";
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
            // grpDstPath
            // 
            this.grpDstPath.Controls.Add(this.chkReplaceOriginalFile);
            this.grpDstPath.Controls.Add(this.btnOpenDst);
            this.grpDstPath.Controls.Add(this.txtDstDir);
            this.grpDstPath.Location = new System.Drawing.Point(420, 10);
            this.grpDstPath.Name = "grpDstPath";
            this.grpDstPath.Size = new System.Drawing.Size(400, 53);
            this.grpDstPath.TabIndex = 2;
            this.grpDstPath.TabStop = false;
            this.grpDstPath.Text = "Save Compressed Image To";
            // 
            // chkReplaceOriginalFile
            // 
            this.chkReplaceOriginalFile.AutoSize = true;
            this.chkReplaceOriginalFile.Location = new System.Drawing.Point(256, 0);
            this.chkReplaceOriginalFile.Name = "chkReplaceOriginalFile";
            this.chkReplaceOriginalFile.Size = new System.Drawing.Size(138, 19);
            this.chkReplaceOriginalFile.TabIndex = 16;
            this.chkReplaceOriginalFile.Text = "Replace Original Files";
            this.chkReplaceOriginalFile.UseVisualStyleBackColor = true;
            this.chkReplaceOriginalFile.CheckedChanged += new System.EventHandler(this.chkReplaceOriginalFile_CheckedChanged);
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
            this.srcColFile,
            this.srcColSize,
            this.srcColDimension,
            this.srcColBytesPerPixel});
            this.listViewSrc.Location = new System.Drawing.Point(0, 2);
            this.listViewSrc.Name = "listViewSrc";
            this.listViewSrc.Size = new System.Drawing.Size(800, 192);
            this.listViewSrc.TabIndex = 3;
            this.listViewSrc.UseCompatibleStateImageBehavior = false;
            this.listViewSrc.View = System.Windows.Forms.View.Details;
            this.listViewSrc.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewSrc_ColumnClick);
            this.listViewSrc.SelectedIndexChanged += new System.EventHandler(this.listViewSrc_SelectedIndexChanged);
            // 
            // srcColFile
            // 
            this.srcColFile.Text = "File Name";
            this.srcColFile.Width = 539;
            // 
            // srcColSize
            // 
            this.srcColSize.Text = "Size";
            this.srcColSize.Width = 80;
            // 
            // srcColDimension
            // 
            this.srcColDimension.Text = "Dimensions";
            this.srcColDimension.Width = 80;
            // 
            // srcColBytesPerPixel
            // 
            this.srcColBytesPerPixel.Text = "Bytes/100px";
            this.srcColBytesPerPixel.Width = 80;
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
            // grpSelection
            // 
            this.grpSelection.Controls.Add(this.numMinB100);
            this.grpSelection.Controls.Add(this.numMinSize);
            this.grpSelection.Controls.Add(this.chkMinB100);
            this.grpSelection.Controls.Add(this.chkMinSize);
            this.grpSelection.Controls.Add(this.clFileType);
            this.grpSelection.Controls.Add(this.chkFileTypeAll);
            this.grpSelection.Location = new System.Drawing.Point(10, 69);
            this.grpSelection.Name = "grpSelection";
            this.grpSelection.Size = new System.Drawing.Size(400, 144);
            this.grpSelection.TabIndex = 2;
            this.grpSelection.TabStop = false;
            this.grpSelection.Text = "File Selection";
            // 
            // numMinB100
            // 
            this.numMinB100.Location = new System.Drawing.Point(242, 48);
            this.numMinB100.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMinB100.Name = "numMinB100";
            this.numMinB100.Size = new System.Drawing.Size(88, 23);
            this.numMinB100.TabIndex = 15;
            this.numMinB100.ValueChanged += new System.EventHandler(this.numMinB100_ValueChanged);
            // 
            // numMinSize
            // 
            this.numMinSize.Location = new System.Drawing.Point(242, 18);
            this.numMinSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numMinSize.Name = "numMinSize";
            this.numMinSize.Size = new System.Drawing.Size(88, 23);
            this.numMinSize.TabIndex = 10;
            this.numMinSize.ValueChanged += new System.EventHandler(this.numMinSize_ValueChanged);
            // 
            // chkMinB100
            // 
            this.chkMinB100.AutoSize = true;
            this.chkMinB100.Location = new System.Drawing.Point(143, 49);
            this.chkMinB100.Name = "chkMinB100";
            this.chkMinB100.Size = new System.Drawing.Size(93, 19);
            this.chkMinB100.TabIndex = 14;
            this.chkMinB100.Text = "Min B/100px";
            this.chkMinB100.UseVisualStyleBackColor = true;
            this.chkMinB100.MouseClick += new System.Windows.Forms.MouseEventHandler(this.minParameter_CheckedChanged);
            // 
            // chkMinSize
            // 
            this.chkMinSize.AutoSize = true;
            this.chkMinSize.Location = new System.Drawing.Point(143, 19);
            this.chkMinSize.Name = "chkMinSize";
            this.chkMinSize.Size = new System.Drawing.Size(95, 19);
            this.chkMinSize.TabIndex = 13;
            this.chkMinSize.Text = "Min Size (Kb)";
            this.chkMinSize.UseVisualStyleBackColor = true;
            this.chkMinSize.MouseClick += new System.Windows.Forms.MouseEventHandler(this.minParameter_CheckedChanged);
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
            // grpCompression
            // 
            this.grpCompression.Controls.Add(this.groupBox6);
            this.grpCompression.Controls.Add(this.label3);
            this.grpCompression.Controls.Add(this.trkNewDimensionPct);
            this.grpCompression.Controls.Add(this.numNewDimensionPct);
            this.grpCompression.Controls.Add(this.numFixedWidth);
            this.grpCompression.Controls.Add(this.groupBox5);
            this.grpCompression.Controls.Add(this.label2);
            this.grpCompression.Controls.Add(this.numQuality);
            this.grpCompression.Controls.Add(this.label1);
            this.grpCompression.Controls.Add(this.trkQuality);
            this.grpCompression.Location = new System.Drawing.Point(420, 69);
            this.grpCompression.Name = "grpCompression";
            this.grpCompression.Size = new System.Drawing.Size(400, 144);
            this.grpCompression.TabIndex = 3;
            this.grpCompression.TabStop = false;
            this.grpCompression.Text = "Compression Parameter";
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(10, 218);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(810, 224);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewSrc);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(802, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Files to Compress";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewDst);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(802, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compressed Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewDst
            // 
            this.listViewDst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dstColFile,
            this.dstColSize,
            this.dstColDimension,
            this.dstColBytesPerPixel});
            this.listViewDst.Location = new System.Drawing.Point(0, 2);
            this.listViewDst.Name = "listViewDst";
            this.listViewDst.Size = new System.Drawing.Size(800, 192);
            this.listViewDst.TabIndex = 0;
            this.listViewDst.UseCompatibleStateImageBehavior = false;
            this.listViewDst.View = System.Windows.Forms.View.Details;
            // 
            // dstColFile
            // 
            this.dstColFile.Text = "File Name";
            this.dstColFile.Width = 539;
            // 
            // dstColSize
            // 
            this.dstColSize.Text = "Size";
            this.dstColSize.Width = 80;
            // 
            // dstColDimension
            // 
            this.dstColDimension.Text = "Dimensions";
            this.dstColDimension.Width = 80;
            // 
            // dstColBytesPerPixel
            // 
            this.dstColBytesPerPixel.Text = "Bytes/100px";
            this.dstColBytesPerPixel.Width = 80;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 501);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.grpCompression);
            this.Controls.Add(this.grpSelection);
            this.Controls.Add(this.btnCompress);
            this.Controls.Add(this.grpDstPath);
            this.Controls.Add(this.grpSrcPath);
            this.Name = "Form1";
            this.Text = "Mass Image Compressor Extended";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.grpSrcPath.ResumeLayout(false);
            this.grpSrcPath.PerformLayout();
            this.grpDstPath.ResumeLayout(false);
            this.grpDstPath.PerformLayout();
            this.grpSelection.ResumeLayout(false);
            this.grpSelection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinB100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).EndInit();
            this.grpCompression.ResumeLayout(false);
            this.grpCompression.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkNewDimensionPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNewDimensionPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFixedWidth)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox grpSrcPath;
        private Button btnOpenSrc;
        private TextBox txtScrDir;
        private GroupBox grpDstPath;
        private Button btnOpenDst;
        private TextBox txtDstDir;
        private ListView listViewSrc;
        private ColumnHeader srcColFile;
        private ColumnHeader srcColSize;
        private Button btnCompress;
        private GroupBox grpSelection;
        private GroupBox grpCompression;
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
        private ColumnHeader srcColDimension;
        private ColumnHeader srcColBytesPerPixel;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ListView listViewDst;
        private ColumnHeader dstColFile;
        private ColumnHeader dstColSize;
        private ColumnHeader dstColDimension;
        private ColumnHeader dstColBytesPerPixel;
        private CheckBox chkMinB100;
        private CheckBox chkMinSize;
        private NumericUpDown numMinB100;
        private NumericUpDown numMinSize;
        private CheckBox chkReplaceOriginalFile;
    }
}