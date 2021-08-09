namespace OrdersProgress
{
    partial class M2100_OrdersPriority
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M2100_OrdersPriority));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkCreateOrder_StockItems = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_OrderTitle = new System.Windows.Forms.TextBox();
            this.cmbST_OrderTitle = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkCanEdit = new System.Windows.Forms.CheckBox();
            this.chkShowUpdateMessage = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnSetDefaultPriorities = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbWarehouses = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSystemPrioritiesRecommended = new System.Windows.Forms.Button();
            this.btnDoPriorities = new System.Windows.Forms.Button();
            this.btnSaveForAllOrders = new System.Windows.Forms.Button();
            this.btnPdf_from_All = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDoChangesForOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShow_StockItems = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.panel1.Controls.Add(this.chkCreateOrder_StockItems);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btnSetDefaultPriorities);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSystemPrioritiesRecommended);
            this.panel1.Controls.Add(this.btnDoPriorities);
            this.panel1.Controls.Add(this.btnSaveForAllOrders);
            this.panel1.Controls.Add(this.btnPdf_from_All);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 319);
            this.panel1.TabIndex = 0;
            // 
            // chkCreateOrder_StockItems
            // 
            this.chkCreateOrder_StockItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCreateOrder_StockItems.AutoSize = true;
            this.chkCreateOrder_StockItems.Location = new System.Drawing.Point(761, 219);
            this.chkCreateOrder_StockItems.Name = "chkCreateOrder_StockItems";
            this.chkCreateOrder_StockItems.Size = new System.Drawing.Size(130, 23);
            this.chkCreateOrder_StockItems.TabIndex = 81;
            this.chkCreateOrder_StockItems.Text = "لیست اقلام تهیه شود";
            this.chkCreateOrder_StockItems.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgv2);
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_OrderTitle);
            this.groupBox1.Controls.Add(this.cmbST_OrderTitle);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(610, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 96);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // dgv2
            // 
            this.dgv2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Location = new System.Drawing.Point(99, 74);
            this.dgv2.Name = "dgv2";
            this.dgv2.Size = new System.Drawing.Size(63, 16);
            this.dgv2.TabIndex = 94;
            this.dgv2.Visible = false;
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(196, 64);
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
            this.btnSearch.Location = new System.Drawing.Point(6, 64);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 26);
            this.btnSearch.TabIndex = 30;
            this.btnSearch.Text = "جستجو";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // txtST_OrderTitle
            // 
            this.txtST_OrderTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_OrderTitle.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_OrderTitle.Location = new System.Drawing.Point(6, 29);
            this.txtST_OrderTitle.Name = "txtST_OrderTitle";
            this.txtST_OrderTitle.Size = new System.Drawing.Size(103, 22);
            this.txtST_OrderTitle.TabIndex = 15;
            this.txtST_OrderTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.cmbST_OrderTitle.Location = new System.Drawing.Point(114, 28);
            this.cmbST_OrderTitle.Name = "cmbST_OrderTitle";
            this.cmbST_OrderTitle.Size = new System.Drawing.Size(91, 23);
            this.cmbST_OrderTitle.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(208, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "عنوان سفارش :";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.chkCanEdit);
            this.panel3.Controls.Add(this.chkShowUpdateMessage);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Location = new System.Drawing.Point(624, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(270, 50);
            this.panel3.TabIndex = 79;
            // 
            // chkCanEdit
            // 
            this.chkCanEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkCanEdit.AutoSize = true;
            this.chkCanEdit.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCanEdit.Location = new System.Drawing.Point(38, 3);
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
            this.chkShowUpdateMessage.Location = new System.Drawing.Point(85, 29);
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
            "نمایش موارد فعال",
            "نمایش موارد غیرفعال",
            "نمایش همه موارد"});
            this.comboBox1.Location = new System.Drawing.Point(10, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(229, 25);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.Visible = false;
            // 
            // btnSetDefaultPriorities
            // 
            this.btnSetDefaultPriorities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSetDefaultPriorities.Location = new System.Drawing.Point(191, 283);
            this.btnSetDefaultPriorities.Name = "btnSetDefaultPriorities";
            this.btnSetDefaultPriorities.Size = new System.Drawing.Size(116, 29);
            this.btnSetDefaultPriorities.TabIndex = 78;
            this.btnSetDefaultPriorities.Text = "اولویت پیش فرض";
            this.btnSetDefaultPriorities.UseVisualStyleBackColor = true;
            this.btnSetDefaultPriorities.Click += new System.EventHandler(this.BtnSetDefaultPriorities_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAll.Location = new System.Drawing.Point(95, 283);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAll.TabIndex = 78;
            this.btnDeleteAll.Text = "حذف همه";
            this.btnDeleteAll.UseVisualStyleBackColor = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.cmbWarehouses);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(624, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 31);
            this.panel2.TabIndex = 77;
            // 
            // cmbWarehouses
            // 
            this.cmbWarehouses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWarehouses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouses.Enabled = false;
            this.cmbWarehouses.FormattingEnabled = true;
            this.cmbWarehouses.Location = new System.Drawing.Point(3, 2);
            this.cmbWarehouses.Name = "cmbWarehouses";
            this.cmbWarehouses.Size = new System.Drawing.Size(176, 27);
            this.cmbWarehouses.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = " انتخاب انبار :";
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(2, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(602, 274);
            this.dgvData.TabIndex = 1;
            this.dgvData.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DgvData_CellBeginEdit);
            this.dgvData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvData_CellEndEdit);
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgvData_DataError);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Maroon;
            this.label2.Location = new System.Drawing.Point(42, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(565, 18);
            this.label2.TabIndex = 76;
            this.label2.Text = "* برای تأیید و ثبت تغییرات سفارش و موجودی انبار ، پس از تغییر مقادیر بر روی آن کل" +
    "یک سمت راست نمایید";
            this.label2.Visible = false;
            // 
            // btnSystemPrioritiesRecommended
            // 
            this.btnSystemPrioritiesRecommended.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSystemPrioritiesRecommended.Location = new System.Drawing.Point(695, 248);
            this.btnSystemPrioritiesRecommended.Name = "btnSystemPrioritiesRecommended";
            this.btnSystemPrioritiesRecommended.Size = new System.Drawing.Size(196, 29);
            this.btnSystemPrioritiesRecommended.TabIndex = 61;
            this.btnSystemPrioritiesRecommended.Text = "اعمال اولویتهای پیشنهادی سیستم";
            this.btnSystemPrioritiesRecommended.UseVisualStyleBackColor = true;
            this.btnSystemPrioritiesRecommended.Click += new System.EventHandler(this.BtnSystemPrioritiesRecommended_Click);
            // 
            // btnDoPriorities
            // 
            this.btnDoPriorities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoPriorities.Location = new System.Drawing.Point(695, 282);
            this.btnDoPriorities.Name = "btnDoPriorities";
            this.btnDoPriorities.Size = new System.Drawing.Size(196, 29);
            this.btnDoPriorities.TabIndex = 61;
            this.btnDoPriorities.Text = "اعمال تغییرات اولویتهای کاربر";
            this.btnDoPriorities.UseVisualStyleBackColor = true;
            this.btnDoPriorities.Click += new System.EventHandler(this.BtnDoUserPriorities_Click);
            // 
            // btnSaveForAllOrders
            // 
            this.btnSaveForAllOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveForAllOrders.Location = new System.Drawing.Point(411, 283);
            this.btnSaveForAllOrders.Name = "btnSaveForAllOrders";
            this.btnSaveForAllOrders.Size = new System.Drawing.Size(194, 29);
            this.btnSaveForAllOrders.TabIndex = 61;
            this.btnSaveForAllOrders.Text = "ثبت تغییرات برای کل سفارش ها";
            this.btnSaveForAllOrders.UseVisualStyleBackColor = true;
            this.btnSaveForAllOrders.Visible = false;
            this.btnSaveForAllOrders.Click += new System.EventHandler(this.BtnSaveForAllOrders_Click);
            // 
            // btnPdf_from_All
            // 
            this.btnPdf_from_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf_from_All.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPdf_from_All.BackgroundImage")));
            this.btnPdf_from_All.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPdf_from_All.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPdf_from_All.Location = new System.Drawing.Point(313, 279);
            this.btnPdf_from_All.Name = "btnPdf_from_All";
            this.btnPdf_from_All.Size = new System.Drawing.Size(92, 37);
            this.btnPdf_from_All.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnPdf_from_All, "تهیه فایل پی دی اف از چک لیست ارسال سفارشهای نمایش داده شده");
            this.btnPdf_from_All.UseVisualStyleBackColor = true;
            this.btnPdf_from_All.Click += new System.EventHandler(this.BtnPdf_from_All_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 282);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 31);
            this.btnReturn.TabIndex = 0;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDoChangesForOrder,
            this.tsmiShow_StockItems});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(220, 48);
            // 
            // tsmiDoChangesForOrder
            // 
            this.tsmiDoChangesForOrder.Name = "tsmiDoChangesForOrder";
            this.tsmiDoChangesForOrder.Size = new System.Drawing.Size(219, 22);
            this.tsmiDoChangesForOrder.Text = "ثبت تغییرات سفارش";
            this.tsmiDoChangesForOrder.Visible = false;
            this.tsmiDoChangesForOrder.Click += new System.EventHandler(this.tsmiDoChangesForOrder_Click);
            // 
            // tsmiShow_StockItems
            // 
            this.tsmiShow_StockItems.Name = "tsmiShow_StockItems";
            this.tsmiShow_StockItems.Size = new System.Drawing.Size(219, 22);
            this.tsmiShow_StockItems.Text = "مشاهده وضعیت کالاهای سفارش";
            this.tsmiShow_StockItems.Visible = false;
            this.tsmiShow_StockItems.Click += new System.EventHandler(this.TsmiShow_StockItems_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(424, 133);
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
            this.progressBar1.Location = new System.Drawing.Point(399, 149);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            // 
            // M2100_OrdersPriority
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(899, 321);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.panel1);
            this.Name = "M2100_OrdersPriority";
            this.Text = "تعیین اولویت سفارشها";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.M2100_OrdersPriority_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSaveForAllOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoChangesForOrder;
        private System.Windows.Forms.Button btnDoPriorities;
        private System.Windows.Forms.ToolStripMenuItem tsmiShow_StockItems;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbWarehouses;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnSetDefaultPriorities;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkCanEdit;
        private System.Windows.Forms.CheckBox chkShowUpdateMessage;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtST_OrderTitle;
        private System.Windows.Forms.ComboBox cmbST_OrderTitle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSystemPrioritiesRecommended;
        private System.Windows.Forms.CheckBox chkCreateOrder_StockItems;
        private System.Windows.Forms.Button btnPdf_from_All;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView dgv2;
    }
}