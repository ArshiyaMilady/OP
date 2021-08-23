namespace OrdersProgress
{
    partial class J2200_Users_Levels
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(J2200_Users_Levels));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radDisabledLevel = new System.Windows.Forms.RadioButton();
            this.radEnabledLevel = new System.Windows.Forms.RadioButton();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_Description = new System.Windows.Forms.TextBox();
            this.cmbST_Description = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkCanEdit = new System.Windows.Forms.CheckBox();
            this.chkShowUpdateMessage = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnImportDataFromExcel = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUL_Features = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUL_Features_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteAllFeatures = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUL_See_ULs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUL_See_OL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetOL_UL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUL_Request_Categories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tsmiUL_Confirm_UL_Request = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnImportDataFromExcel);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 289);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Controls.Add(this.radDisabledLevel);
            this.groupBox1.Controls.Add(this.radEnabledLevel);
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_Description);
            this.groupBox1.Controls.Add(this.cmbST_Description);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(557, 74);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 173);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // radAll
            // 
            this.radAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAll.AutoSize = true;
            this.radAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAll.Location = new System.Drawing.Point(132, 77);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(99, 19);
            this.radAll.TabIndex = 59;
            this.radAll.Text = "نمایش تمام سطوح";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.RadAll_CheckedChanged);
            // 
            // radDisabledLevel
            // 
            this.radDisabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDisabledLevel.AutoSize = true;
            this.radDisabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDisabledLevel.Location = new System.Drawing.Point(110, 51);
            this.radDisabledLevel.Name = "radDisabledLevel";
            this.radDisabledLevel.Size = new System.Drawing.Size(121, 19);
            this.radDisabledLevel.TabIndex = 58;
            this.radDisabledLevel.Text = "نمایش سطوح غیر فعال";
            this.radDisabledLevel.UseVisualStyleBackColor = true;
            this.radDisabledLevel.CheckedChanged += new System.EventHandler(this.RadAll_CheckedChanged);
            // 
            // radEnabledLevel
            // 
            this.radEnabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEnabledLevel.AutoSize = true;
            this.radEnabledLevel.Checked = true;
            this.radEnabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnabledLevel.Location = new System.Drawing.Point(130, 25);
            this.radEnabledLevel.Name = "radEnabledLevel";
            this.radEnabledLevel.Size = new System.Drawing.Size(102, 19);
            this.radEnabledLevel.TabIndex = 57;
            this.radEnabledLevel.TabStop = true;
            this.radEnabledLevel.Text = "نمایش سطوح فعال";
            this.radEnabledLevel.UseVisualStyleBackColor = true;
            this.radEnabledLevel.CheckedChanged += new System.EventHandler(this.RadAll_CheckedChanged);
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(154, 141);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(79, 26);
            this.btnShowAll.TabIndex = 56;
            this.btnShowAll.Text = "نمایش همه";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(6, 141);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 50;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtST_Description
            // 
            this.txtST_Description.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Description.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Description.Location = new System.Drawing.Point(5, 117);
            this.txtST_Description.Name = "txtST_Description";
            this.txtST_Description.Size = new System.Drawing.Size(109, 22);
            this.txtST_Description.TabIndex = 45;
            this.txtST_Description.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbST_Description
            // 
            this.cmbST_Description.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_Description.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_Description.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_Description.FormattingEnabled = true;
            this.cmbST_Description.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_Description.Location = new System.Drawing.Point(116, 115);
            this.cmbST_Description.Name = "cmbST_Description";
            this.cmbST_Description.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Description.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(207, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "شرح :";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(114, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(437, 18);
            this.label4.TabIndex = 75;
            this.label4.Text = "* برای مشاهده موارد دیگر، بر روی ردیف مورد نظر کلیک راست نمایید";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.chkCanEdit);
            this.panel2.Controls.Add(this.chkShowUpdateMessage);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Location = new System.Drawing.Point(557, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 62);
            this.panel2.TabIndex = 1;
            // 
            // chkCanEdit
            // 
            this.chkCanEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCanEdit.AutoSize = true;
            this.chkCanEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCanEdit.Location = new System.Drawing.Point(12, 3);
            this.chkCanEdit.Name = "chkCanEdit";
            this.chkCanEdit.Size = new System.Drawing.Size(229, 21);
            this.chkCanEdit.TabIndex = 4;
            this.chkCanEdit.Text = "فعال کردن حالت «امکان تغییر» در جدول";
            this.chkCanEdit.UseVisualStyleBackColor = true;
            this.chkCanEdit.CheckedChanged += new System.EventHandler(this.ChkCanEdit_CheckedChanged);
            // 
            // chkShowUpdateMessage
            // 
            this.chkShowUpdateMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowUpdateMessage.AutoSize = true;
            this.chkShowUpdateMessage.Checked = true;
            this.chkShowUpdateMessage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUpdateMessage.Enabled = false;
            this.chkShowUpdateMessage.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowUpdateMessage.Location = new System.Drawing.Point(59, 29);
            this.chkShowUpdateMessage.Name = "chkShowUpdateMessage";
            this.chkShowUpdateMessage.Size = new System.Drawing.Size(183, 19);
            this.chkShowUpdateMessage.TabIndex = 10;
            this.chkShowUpdateMessage.Text = "نمایش پیغام اخطار قبل از ثبت تغییرات";
            this.chkShowUpdateMessage.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "نمایش موارد غیرفعال",
            "نمایش همه موارد",
            "نمایش موارد فعال"});
            this.comboBox1.Location = new System.Drawing.Point(10, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(229, 25);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.Visible = false;
            // 
            // btnImportDataFromExcel
            // 
            this.btnImportDataFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportDataFromExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImportDataFromExcel.BackgroundImage")));
            this.btnImportDataFromExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportDataFromExcel.Location = new System.Drawing.Point(371, 254);
            this.btnImportDataFromExcel.Name = "btnImportDataFromExcel";
            this.btnImportDataFromExcel.Size = new System.Drawing.Size(34, 34);
            this.btnImportDataFromExcel.TabIndex = 80;
            this.btnImportDataFromExcel.UseVisualStyleBackColor = true;
            this.btnImportDataFromExcel.Visible = false;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 255);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 80;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAll.Location = new System.Drawing.Point(83, 255);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAll.TabIndex = 75;
            this.btnDeleteAll.Text = "حذف همه";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Visible = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(411, 255);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(141, 32);
            this.btnAddNew.TabIndex = 70;
            this.btnAddNew.Text = "تعریف سطح جدید";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(1, 28);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(551, 221);
            this.dgvData.TabIndex = 90;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvData_CellBeginEdit);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUL_Features,
            this.tsmiUL_See_ULs,
            this.tsmiUL_See_OL,
            this.tsmiSetOL_UL,
            this.tsmiUL_Request_Categories,
            this.tsmiUL_Confirm_UL_Request,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(288, 180);
            // 
            // tsmiUL_Features
            // 
            this.tsmiUL_Features.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUL_Features_Edit,
            this.tsmiDeleteAllFeatures});
            this.tsmiUL_Features.Name = "tsmiUL_Features";
            this.tsmiUL_Features.Size = new System.Drawing.Size(287, 22);
            this.tsmiUL_Features.Text = "امکانات";
            // 
            // tsmiUL_Features_Edit
            // 
            this.tsmiUL_Features_Edit.Name = "tsmiUL_Features_Edit";
            this.tsmiUL_Features_Edit.Size = new System.Drawing.Size(183, 22);
            this.tsmiUL_Features_Edit.Text = "مشاهده و ویراش امکانات";
            this.tsmiUL_Features_Edit.Click += new System.EventHandler(this.TsmiUL_Features_Edit_Click);
            // 
            // tsmiDeleteAllFeatures
            // 
            this.tsmiDeleteAllFeatures.Name = "tsmiDeleteAllFeatures";
            this.tsmiDeleteAllFeatures.Size = new System.Drawing.Size(183, 22);
            this.tsmiDeleteAllFeatures.Text = "حذف تمام امکانات";
            this.tsmiDeleteAllFeatures.Click += new System.EventHandler(this.TsmiDeleteAllFeatures_Click);
            // 
            // tsmiUL_See_ULs
            // 
            this.tsmiUL_See_ULs.Name = "tsmiUL_See_ULs";
            this.tsmiUL_See_ULs.Size = new System.Drawing.Size(287, 22);
            this.tsmiUL_See_ULs.Text = "مشاهده سطوح کاربری دیگر";
            this.tsmiUL_See_ULs.Click += new System.EventHandler(this.tsmiUL_See_ULs_Click);
            // 
            // tsmiUL_See_OL
            // 
            this.tsmiUL_See_OL.Name = "tsmiUL_See_OL";
            this.tsmiUL_See_OL.Size = new System.Drawing.Size(287, 22);
            this.tsmiUL_See_OL.Text = "مشاهده مراحل سفارش";
            this.tsmiUL_See_OL.Click += new System.EventHandler(this.TsmiUL_See_OL_Click);
            // 
            // tsmiSetOL_UL
            // 
            this.tsmiSetOL_UL.Name = "tsmiSetOL_UL";
            this.tsmiSetOL_UL.Size = new System.Drawing.Size(287, 22);
            this.tsmiSetOL_UL.Text = "تعیین مراحل سفارش قابل تأیید";
            this.tsmiSetOL_UL.Click += new System.EventHandler(this.TsmiSetOL_UL_Click);
            // 
            // tsmiUL_Request_Categories
            // 
            this.tsmiUL_Request_Categories.Name = "tsmiUL_Request_Categories";
            this.tsmiUL_Request_Categories.Size = new System.Drawing.Size(287, 22);
            this.tsmiUL_Request_Categories.Text = "تنظیمات درخواست کالا از انبار";
            this.tsmiUL_Request_Categories.Click += new System.EventHandler(this.TsmiUL_Request_Categories_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(287, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(378, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 93;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // tsmiUL_Confirm_UL_Request
            // 
            this.tsmiUL_Confirm_UL_Request.Name = "tsmiUL_Confirm_UL_Request";
            this.tsmiUL_Confirm_UL_Request.Size = new System.Drawing.Size(287, 22);
            this.tsmiUL_Confirm_UL_Request.Text = "سطح کاربری تأییدکننده درخواست کالا از انبار";
            this.tsmiUL_Confirm_UL_Request.Visible = false;
            this.tsmiUL_Confirm_UL_Request.Click += new System.EventHandler(this.TsmiUL_Confirm_UL_Request_Click);
            // 
            // J2200_Users_Levels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(804, 289);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "J2200_Users_Levels";
            this.Text = "   سطوح کاربری";
            this.Shown += new System.EventHandler(this.J2200_Users_Levels_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtST_Description;
        private System.Windows.Forms.ComboBox cmbST_Description;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.CheckBox chkShowUpdateMessage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnImportDataFromExcel;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radDisabledLevel;
        private System.Windows.Forms.RadioButton radEnabledLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_Features;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteAllFeatures;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_Features_Edit;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_See_ULs;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_See_OL;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetOL_UL;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_Request_Categories;
        private System.Windows.Forms.ToolStripMenuItem tsmiUL_Confirm_UL_Request;
    }
}