namespace OrdersProgress
{
    partial class M1130_Warehouse_RequestItems
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M1130_Warehouse_RequestItems));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radMyRequests = new System.Windows.Forms.RadioButton();
            this.radConfirmedRequests = new System.Windows.Forms.RadioButton();
            this.radRequests_Need_Confirmation = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_RequestIndex = new System.Windows.Forms.TextBox();
            this.txtST_OrderTitle = new System.Windows.Forms.TextBox();
            this.txtST_CustomerName = new System.Windows.Forms.TextBox();
            this.cmbST_OrderTitle = new System.Windows.Forms.ComboBox();
            this.cmbST_CustomerName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnImportOrdersFromExcel = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnCostCenters = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOrderHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrderDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnImportOrdersFromExcel);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.btnCostCenters);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(-1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 321);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.radMyRequests);
            this.panel2.Controls.Add(this.radConfirmedRequests);
            this.panel2.Controls.Add(this.radRequests_Need_Confirmation);
            this.panel2.Location = new System.Drawing.Point(600, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 94);
            this.panel2.TabIndex = 51;
            this.panel2.Visible = false;
            // 
            // radMyRequests
            // 
            this.radMyRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radMyRequests.AutoSize = true;
            this.radMyRequests.Location = new System.Drawing.Point(125, 38);
            this.radMyRequests.Name = "radMyRequests";
            this.radMyRequests.Size = new System.Drawing.Size(109, 23);
            this.radMyRequests.TabIndex = 0;
            this.radMyRequests.Text = "درخواستهای من";
            this.radMyRequests.UseVisualStyleBackColor = true;
            this.radMyRequests.CheckedChanged += new System.EventHandler(this.RadRequests_CheckedChanged);
            // 
            // radConfirmedRequests
            // 
            this.radConfirmedRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radConfirmedRequests.AutoSize = true;
            this.radConfirmedRequests.Location = new System.Drawing.Point(29, 67);
            this.radConfirmedRequests.Name = "radConfirmedRequests";
            this.radConfirmedRequests.Size = new System.Drawing.Size(206, 23);
            this.radConfirmedRequests.TabIndex = 0;
            this.radConfirmedRequests.Text = "درخواستهایی که تأیید یا لغو شده اند";
            this.radConfirmedRequests.UseVisualStyleBackColor = true;
            this.radConfirmedRequests.CheckedChanged += new System.EventHandler(this.RadRequests_CheckedChanged);
            // 
            // radRequests_Need_Confirmation
            // 
            this.radRequests_Need_Confirmation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radRequests_Need_Confirmation.AutoSize = true;
            this.radRequests_Need_Confirmation.Location = new System.Drawing.Point(64, 9);
            this.radRequests_Need_Confirmation.Name = "radRequests_Need_Confirmation";
            this.radRequests_Need_Confirmation.Size = new System.Drawing.Size(169, 23);
            this.radRequests_Need_Confirmation.TabIndex = 0;
            this.radRequests_Need_Confirmation.Text = "درخواستهای در انتظار تأیید";
            this.radRequests_Need_Confirmation.UseVisualStyleBackColor = true;
            this.radRequests_Need_Confirmation.CheckedChanged += new System.EventHandler(this.RadRequests_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_RequestIndex);
            this.groupBox1.Controls.Add(this.txtST_OrderTitle);
            this.groupBox1.Controls.Add(this.txtST_CustomerName);
            this.groupBox1.Controls.Add(this.cmbST_OrderTitle);
            this.groupBox1.Controls.Add(this.cmbST_CustomerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(600, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 88);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(153, 56);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(79, 26);
            this.btnShowAll.TabIndex = 35;
            this.btnShowAll.Text = "نمایش همه";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(6, 56);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtST_RequestIndex
            // 
            this.txtST_RequestIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_RequestIndex.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_RequestIndex.Location = new System.Drawing.Point(64, 26);
            this.txtST_RequestIndex.Name = "txtST_RequestIndex";
            this.txtST_RequestIndex.Size = new System.Drawing.Size(85, 22);
            this.txtST_RequestIndex.TabIndex = 15;
            this.txtST_RequestIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtST_OrderTitle
            // 
            this.txtST_OrderTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_OrderTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_OrderTitle.Location = new System.Drawing.Point(-37, 55);
            this.txtST_OrderTitle.Name = "txtST_OrderTitle";
            this.txtST_OrderTitle.Size = new System.Drawing.Size(103, 22);
            this.txtST_OrderTitle.TabIndex = 15;
            this.txtST_OrderTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_OrderTitle.Visible = false;
            // 
            // txtST_CustomerName
            // 
            this.txtST_CustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_CustomerName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_CustomerName.Location = new System.Drawing.Point(-37, 84);
            this.txtST_CustomerName.Name = "txtST_CustomerName";
            this.txtST_CustomerName.Size = new System.Drawing.Size(103, 22);
            this.txtST_CustomerName.TabIndex = 25;
            this.txtST_CustomerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_CustomerName.Visible = false;
            // 
            // cmbST_OrderTitle
            // 
            this.cmbST_OrderTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_OrderTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_OrderTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_OrderTitle.FormattingEnabled = true;
            this.cmbST_OrderTitle.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_OrderTitle.Location = new System.Drawing.Point(71, 54);
            this.cmbST_OrderTitle.Name = "cmbST_OrderTitle";
            this.cmbST_OrderTitle.Size = new System.Drawing.Size(91, 23);
            this.cmbST_OrderTitle.TabIndex = 10;
            this.cmbST_OrderTitle.Visible = false;
            // 
            // cmbST_CustomerName
            // 
            this.cmbST_CustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbST_CustomerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbST_CustomerName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbST_CustomerName.FormattingEnabled = true;
            this.cmbST_CustomerName.Items.AddRange(new object[] {
            "شامل",
            "شروع شود با",
            "برابر باشد با"});
            this.cmbST_CustomerName.Location = new System.Drawing.Point(71, 83);
            this.cmbST_CustomerName.Name = "cmbST_CustomerName";
            this.cmbST_CustomerName.Size = new System.Drawing.Size(91, 23);
            this.cmbST_CustomerName.TabIndex = 20;
            this.cmbST_CustomerName.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(155, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "شماره درخواست :";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(165, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "عنوان سفارش :";
            this.label5.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(166, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "نام خریدار :";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(228, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "برای استفاده از امکانات بیشتر بر روی سفارش مورد نظر کلیک راست نمایید.";
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 287);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 40;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            // 
            // btnImportOrdersFromExcel
            // 
            this.btnImportOrdersFromExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImportOrdersFromExcel.Location = new System.Drawing.Point(600, 252);
            this.btnImportOrdersFromExcel.Name = "btnImportOrdersFromExcel";
            this.btnImportOrdersFromExcel.Size = new System.Drawing.Size(240, 31);
            this.btnImportOrdersFromExcel.TabIndex = 45;
            this.btnImportOrdersFromExcel.Text = "دریافت اطلاعات سفارش های موجود از فایل اکسل";
            this.btnImportOrdersFromExcel.UseVisualStyleBackColor = true;
            this.btnImportOrdersFromExcel.Visible = false;
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNew.Location = new System.Drawing.Point(491, 287);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(103, 29);
            this.btnAddNew.TabIndex = 45;
            this.btnAddNew.Text = "درخواست جدید";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Visible = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // btnCostCenters
            // 
            this.btnCostCenters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCostCenters.Location = new System.Drawing.Point(383, 287);
            this.btnCostCenters.Name = "btnCostCenters";
            this.btnCostCenters.Size = new System.Drawing.Size(89, 29);
            this.btnCostCenters.TabIndex = 45;
            this.btnCostCenters.Text = "مراکز هزینه";
            this.btnCostCenters.UseVisualStyleBackColor = true;
            this.btnCostCenters.Visible = false;
            this.btnCostCenters.Click += new System.EventHandler(this.BtnCostCenters_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAll.Location = new System.Drawing.Point(84, 287);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAll.TabIndex = 45;
            this.btnDeleteAll.Text = "حذف همه";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Visible = false;
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
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(591, 255);
            this.dgvData.TabIndex = 50;
            this.dgvData.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellDoubleClick);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOrderHistory,
            this.tsmiOrderDetails,
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(191, 70);
            // 
            // tsmiOrderHistory
            // 
            this.tsmiOrderHistory.Name = "tsmiOrderHistory";
            this.tsmiOrderHistory.Size = new System.Drawing.Size(190, 22);
            this.tsmiOrderHistory.Text = "تاریخچه درخواست";
            this.tsmiOrderHistory.Click += new System.EventHandler(this.TsmiRequestHistory_Click);
            // 
            // tsmiOrderDetails
            // 
            this.tsmiOrderDetails.Name = "tsmiOrderDetails";
            this.tsmiOrderDetails.Size = new System.Drawing.Size(190, 22);
            this.tsmiOrderDetails.Text = "مشاهده جزییات درخواست";
            this.tsmiOrderDetails.Click += new System.EventHandler(this.TsmiRequestDetails_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(190, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(396, 134);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(371, 150);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // M1130_Warehouse_RequestItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(843, 322);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Name = "M1130_Warehouse_RequestItems";
            this.Text = "   درخواست کالا از انبار";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.M1130_Warehouse_RequestItems_Load);
            this.Shown += new System.EventHandler(this.M1130_Warehouse_RequestItems_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radConfirmedRequests;
        private System.Windows.Forms.RadioButton radRequests_Need_Confirmation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtST_RequestIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnImportOrdersFromExcel;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox txtST_OrderTitle;
        private System.Windows.Forms.TextBox txtST_CustomerName;
        private System.Windows.Forms.ComboBox cmbST_OrderTitle;
        private System.Windows.Forms.ComboBox cmbST_CustomerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrderHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrderDetails;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.RadioButton radMyRequests;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Button btnCostCenters;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
    }
}