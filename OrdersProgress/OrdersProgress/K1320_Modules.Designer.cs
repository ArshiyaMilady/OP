namespace OrdersProgress
{
    partial class K1320_Modules
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(K1320_Modules));
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radModule = new System.Windows.Forms.RadioButton();
            this.radNotModule = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkJustShowSubstructure = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_Name = new System.Windows.Forms.TextBox();
            this.txtST_SmallCode = new System.Windows.Forms.TextBox();
            this.cmbST_Name = new System.Windows.Forms.ComboBox();
            this.cmbST_SmallCode = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnFlowchart = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnFlowchart);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 345);
            this.panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(213, 162);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.radModule);
            this.panel3.Controls.Add(this.radNotModule);
            this.panel3.Controls.Add(this.radAll);
            this.panel3.Location = new System.Drawing.Point(536, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 96);
            this.panel3.TabIndex = 4;
            // 
            // radModule
            // 
            this.radModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radModule.AutoSize = true;
            this.radModule.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radModule.Location = new System.Drawing.Point(132, 7);
            this.radModule.Name = "radModule";
            this.radModule.Size = new System.Drawing.Size(103, 21);
            this.radModule.TabIndex = 10;
            this.radModule.TabStop = true;
            this.radModule.Text = "نمایش ماژول ها";
            this.radModule.UseVisualStyleBackColor = true;
            this.radModule.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // radNotModule
            // 
            this.radNotModule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radNotModule.AutoSize = true;
            this.radNotModule.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radNotModule.Location = new System.Drawing.Point(142, 36);
            this.radNotModule.Name = "radNotModule";
            this.radNotModule.Size = new System.Drawing.Size(92, 21);
            this.radNotModule.TabIndex = 15;
            this.radNotModule.TabStop = true;
            this.radNotModule.Text = "نمایش قطعات";
            this.radNotModule.UseVisualStyleBackColor = true;
            this.radNotModule.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAll.AutoSize = true;
            this.radAll.Checked = true;
            this.radAll.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radAll.Location = new System.Drawing.Point(84, 63);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(150, 21);
            this.radAll.TabIndex = 20;
            this.radAll.TabStop = true;
            this.radAll.Text = "نمایش ماژول ها و قطعات";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.RadModule_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.chkJustShowSubstructure);
            this.panel2.Location = new System.Drawing.Point(536, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 34);
            this.panel2.TabIndex = 1;
            // 
            // chkJustShowSubstructure
            // 
            this.chkJustShowSubstructure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkJustShowSubstructure.AutoSize = true;
            this.chkJustShowSubstructure.Checked = true;
            this.chkJustShowSubstructure.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJustShowSubstructure.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkJustShowSubstructure.Location = new System.Drawing.Point(25, 5);
            this.chkJustShowSubstructure.Name = "chkJustShowSubstructure";
            this.chkJustShowSubstructure.Size = new System.Drawing.Size(210, 21);
            this.chkJustShowSubstructure.TabIndex = 2;
            this.chkJustShowSubstructure.Text = "فقط قطعات زیرساخت نمایش داده شود";
            this.chkJustShowSubstructure.UseVisualStyleBackColor = true;
            this.chkJustShowSubstructure.CheckedChanged += new System.EventHandler(this.ChkJustShowSubstructure_CheckedChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(69, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(407, 17);
            this.label7.TabIndex = 77;
            this.label7.Text = "برای ذخیرۀ تغییرات و ثبت ساختار جدید بر روی دکمه «ثبت ساختار» کلیک نمایید";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_Name);
            this.groupBox1.Controls.Add(this.txtST_SmallCode);
            this.groupBox1.Controls.Add(this.cmbST_Name);
            this.groupBox1.Controls.Add(this.cmbST_SmallCode);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(536, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 129);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(158, 97);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(79, 26);
            this.btnShowAll.TabIndex = 55;
            this.btnShowAll.Text = "نمایش همه";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(11, 97);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 50;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtST_Name
            // 
            this.txtST_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_Name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_Name.Location = new System.Drawing.Point(7, 62);
            this.txtST_Name.Name = "txtST_Name";
            this.txtST_Name.Size = new System.Drawing.Size(109, 22);
            this.txtST_Name.TabIndex = 45;
            this.txtST_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtST_SmallCode
            // 
            this.txtST_SmallCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_SmallCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_SmallCode.Location = new System.Drawing.Point(7, 33);
            this.txtST_SmallCode.Name = "txtST_SmallCode";
            this.txtST_SmallCode.Size = new System.Drawing.Size(109, 22);
            this.txtST_SmallCode.TabIndex = 34;
            this.txtST_SmallCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_SmallCode.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_SmallCode.Leave += new System.EventHandler(this.TxtST_Leave);
            // 
            // cmbST_Name
            // 
            this.cmbST_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_Name.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_Name.FormattingEnabled = true;
            this.cmbST_Name.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_Name.Location = new System.Drawing.Point(122, 60);
            this.cmbST_Name.Name = "cmbST_Name";
            this.cmbST_Name.Size = new System.Drawing.Size(91, 23);
            this.cmbST_Name.TabIndex = 40;
            // 
            // cmbST_SmallCode
            // 
            this.cmbST_SmallCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_SmallCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_SmallCode.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_SmallCode.FormattingEnabled = true;
            this.cmbST_SmallCode.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_SmallCode.Location = new System.Drawing.Point(122, 31);
            this.cmbST_SmallCode.Name = "cmbST_SmallCode";
            this.cmbST_SmallCode.Size = new System.Drawing.Size(91, 23);
            this.cmbST_SmallCode.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(213, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "نام :";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "کد :";
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 309);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 70;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(435, 309);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 29);
            this.btnSave.TabIndex = 65;
            this.btnSave.Text = "ثبت ساختار";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(1, 29);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(530, 274);
            this.dgvData.TabIndex = 60;
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgvData_DataError);
            // 
            // btnFlowchart
            // 
            this.btnFlowchart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlowchart.BackColor = System.Drawing.Color.Transparent;
            this.btnFlowchart.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFlowchart.BackgroundImage")));
            this.btnFlowchart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFlowchart.Location = new System.Drawing.Point(395, 307);
            this.btnFlowchart.Name = "btnFlowchart";
            this.btnFlowchart.Size = new System.Drawing.Size(34, 33);
            this.btnFlowchart.TabIndex = 78;
            this.toolTip1.SetToolTip(this.btnFlowchart, "مشاهده دیاگرام ساختار");
            this.btnFlowchart.UseVisualStyleBackColor = false;
            this.btnFlowchart.Click += new System.EventHandler(this.BtnFlowchart_Click);
            // 
            // K1320_Modules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(783, 346);
            this.Controls.Add(this.panel1);
            this.Name = "K1320_Modules";
            this.Text = "K1320_Modules";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.K1320_Modules_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.RadioButton radNotModule;
        private System.Windows.Forms.RadioButton radModule;
        private System.Windows.Forms.TextBox txtST_Name;
        private System.Windows.Forms.TextBox txtST_SmallCode;
        private System.Windows.Forms.ComboBox cmbST_Name;
        private System.Windows.Forms.ComboBox cmbST_SmallCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkJustShowSubstructure;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnFlowchart;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}