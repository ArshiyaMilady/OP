namespace OrdersProgress
{
    partial class L1110_Order_Levels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(L1110_Order_Levels));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.radEnabledLevel = new System.Windows.Forms.RadioButton();
            this.radDisabledLevel = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.tsmiOL_Prerequisites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVerifyingUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteAllOL_Prerequisites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.panel1.Controls.Add(this.panel3);
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
            this.panel1.Size = new System.Drawing.Size(805, 271);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radAll);
            this.panel3.Controls.Add(this.radEnabledLevel);
            this.panel3.Controls.Add(this.radDisabledLevel);
            this.panel3.Location = new System.Drawing.Point(557, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 83);
            this.panel3.TabIndex = 91;
            this.panel3.Visible = false;
            // 
            // radAll
            // 
            this.radAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAll.AutoSize = true;
            this.radAll.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAll.Location = new System.Drawing.Point(142, 55);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(99, 19);
            this.radAll.TabIndex = 59;
            this.radAll.Text = "نمایش تمام سطوح";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.RadEnabledLevel_CheckedChanged);
            // 
            // radEnabledLevel
            // 
            this.radEnabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radEnabledLevel.AutoSize = true;
            this.radEnabledLevel.Checked = true;
            this.radEnabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radEnabledLevel.Location = new System.Drawing.Point(137, 5);
            this.radEnabledLevel.Name = "radEnabledLevel";
            this.radEnabledLevel.Size = new System.Drawing.Size(102, 19);
            this.radEnabledLevel.TabIndex = 57;
            this.radEnabledLevel.TabStop = true;
            this.radEnabledLevel.Text = "نمایش سطوح فعال";
            this.radEnabledLevel.UseVisualStyleBackColor = true;
            this.radEnabledLevel.CheckedChanged += new System.EventHandler(this.RadEnabledLevel_CheckedChanged);
            // 
            // radDisabledLevel
            // 
            this.radDisabledLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDisabledLevel.AutoSize = true;
            this.radDisabledLevel.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDisabledLevel.Location = new System.Drawing.Point(121, 30);
            this.radDisabledLevel.Name = "radDisabledLevel";
            this.radDisabledLevel.Size = new System.Drawing.Size(121, 19);
            this.radDisabledLevel.TabIndex = 58;
            this.radDisabledLevel.Text = "نمایش سطوح غیر فعال";
            this.radDisabledLevel.UseVisualStyleBackColor = true;
            this.radDisabledLevel.CheckedChanged += new System.EventHandler(this.RadEnabledLevel_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_Description);
            this.groupBox1.Controls.Add(this.cmbST_Description);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(557, 155);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 95);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(154, 63);
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
            this.btnSearch.Location = new System.Drawing.Point(6, 63);
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
            this.txtST_Description.Location = new System.Drawing.Point(5, 26);
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
            this.cmbST_Description.Location = new System.Drawing.Point(116, 24);
            this.cmbST_Description.Name = "cmbST_Description";
            this.cmbST_Description.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Description.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(207, 27);
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
            this.panel2.Size = new System.Drawing.Size(244, 64);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
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
            this.btnImportDataFromExcel.Location = new System.Drawing.Point(371, 236);
            this.btnImportDataFromExcel.Name = "btnImportDataFromExcel";
            this.btnImportDataFromExcel.Size = new System.Drawing.Size(34, 34);
            this.btnImportDataFromExcel.TabIndex = 80;
            this.btnImportDataFromExcel.UseVisualStyleBackColor = true;
            this.btnImportDataFromExcel.Visible = false;
            this.btnImportDataFromExcel.Click += new System.EventHandler(this.BtnImportDataFromExcel_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 237);
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
            this.btnDeleteAll.Location = new System.Drawing.Point(83, 237);
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
            this.btnAddNew.Location = new System.Drawing.Point(411, 237);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(141, 32);
            this.btnAddNew.TabIndex = 70;
            this.btnAddNew.Text = "تعریف مرحله جدید";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Visible = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(3, 28);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(549, 203);
            this.dgvData.TabIndex = 90;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvData_CellBeginEdit);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOL_Prerequisites,
            this.tsmiVerifyingUsers,
            this.tsmiDeleteAllOL_Prerequisites,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(208, 114);
            // 
            // tsmiOL_Prerequisites
            // 
            this.tsmiOL_Prerequisites.Name = "tsmiOL_Prerequisites";
            this.tsmiOL_Prerequisites.Size = new System.Drawing.Size(207, 22);
            this.tsmiOL_Prerequisites.Text = "تعریف پیش نیازها";
            this.tsmiOL_Prerequisites.Click += new System.EventHandler(this.tsmiOL_Prerequisites_Click);
            // 
            // tsmiVerifyingUsers
            // 
            this.tsmiVerifyingUsers.Name = "tsmiVerifyingUsers";
            this.tsmiVerifyingUsers.Size = new System.Drawing.Size(207, 22);
            this.tsmiVerifyingUsers.Text = "سطح کاربری تأیید کننده";
            this.tsmiVerifyingUsers.Click += new System.EventHandler(this.tsmiVerifyingUsers_Click);
            // 
            // tsmiDeleteAllOL_Prerequisites
            // 
            this.tsmiDeleteAllOL_Prerequisites.Name = "tsmiDeleteAllOL_Prerequisites";
            this.tsmiDeleteAllOL_Prerequisites.Size = new System.Drawing.Size(207, 22);
            this.tsmiDeleteAllOL_Prerequisites.Text = "حذف تمام پیش نیازهای مرحله";
            this.tsmiDeleteAllOL_Prerequisites.Visible = false;
            this.tsmiDeleteAllOL_Prerequisites.Click += new System.EventHandler(this.TsmiDeleteAllOL_Prerequisites_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(207, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 400;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(378, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 94;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // L1110_Order_Levels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(804, 271);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "L1110_Order_Levels";
            this.Text = "مراحل انجام سفارش ها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.L1110_Order_Levels_FormClosing);
            this.Load += new System.EventHandler(this.L1110_Order_Levels_Load);
            this.Shown += new System.EventHandler(this.L1110_Order_Levels_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
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
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radDisabledLevel;
        private System.Windows.Forms.RadioButton radEnabledLevel;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiOL_Prerequisites;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmiVerifyingUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteAllOL_Prerequisites;
        private System.Windows.Forms.Panel panel3;
    }
}