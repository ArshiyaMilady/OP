namespace OrdersProgress
{
    partial class L0900_Orders_and_Details
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtST_OrderTitle = new System.Windows.Forms.TextBox();
            this.txtST_CustomerName = new System.Windows.Forms.TextBox();
            this.cmbST_OrderTitle = new System.Windows.Forms.ComboBox();
            this.cmbST_CustomerName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOrderHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrderDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWarehouseChecklist = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangeOrderLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrderSentOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSentToCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.dgvData);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 308);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnShowAll);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtST_OrderTitle);
            this.groupBox1.Controls.Add(this.txtST_CustomerName);
            this.groupBox1.Controls.Add(this.cmbST_OrderTitle);
            this.groupBox1.Controls.Add(this.cmbST_CustomerName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(556, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 130);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "جستجو";
            // 
            // btnShowAll
            // 
            this.btnShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowAll.Location = new System.Drawing.Point(196, 98);
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
            this.btnSearch.Location = new System.Drawing.Point(6, 98);
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
            this.txtST_OrderTitle.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_OrderTitle.Leave += new System.EventHandler(this.TxtST_Leave);
            // 
            // txtST_CustomerName
            // 
            this.txtST_CustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtST_CustomerName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtST_CustomerName.Location = new System.Drawing.Point(6, 58);
            this.txtST_CustomerName.Name = "txtST_CustomerName";
            this.txtST_CustomerName.Size = new System.Drawing.Size(103, 22);
            this.txtST_CustomerName.TabIndex = 25;
            this.txtST_CustomerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtST_CustomerName.Enter += new System.EventHandler(this.TxtST_Enter);
            this.txtST_CustomerName.Leave += new System.EventHandler(this.TxtST_Leave);
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
            this.cmbST_CustomerName.Location = new System.Drawing.Point(114, 57);
            this.cmbST_CustomerName.Name = "cmbST_CustomerName";
            this.cmbST_CustomerName.Size = new System.Drawing.Size(91, 23);
            this.cmbST_CustomerName.TabIndex = 20;
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
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(209, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "نام خریدار :";
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(3, 274);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 29);
            this.btnReturn.TabIndex = 40;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAll.Location = new System.Drawing.Point(94, 274);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(75, 29);
            this.btnDeleteAll.TabIndex = 45;
            this.btnDeleteAll.Text = "حذف همه";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Visible = false;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // dgvData
            // 
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(3, 2);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(548, 268);
            this.dgvData.TabIndex = 50;
            this.dgvData.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvData_CellMouseDown);
            this.dgvData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvData_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOrderHistory,
            this.tsmiOrderDetails,
            this.tsmiWarehouseChecklist,
            this.tsmiChangeOrder,
            this.tsmiDelete,
            this.tsmiChangeOrderLevel});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 136);
            // 
            // tsmiOrderHistory
            // 
            this.tsmiOrderHistory.Name = "tsmiOrderHistory";
            this.tsmiOrderHistory.Size = new System.Drawing.Size(180, 22);
            this.tsmiOrderHistory.Text = "تاریخچه سفارش";
            // 
            // tsmiOrderDetails
            // 
            this.tsmiOrderDetails.Name = "tsmiOrderDetails";
            this.tsmiOrderDetails.Size = new System.Drawing.Size(180, 22);
            this.tsmiOrderDetails.Text = "مشاهده جزییات سفارش";
            this.tsmiOrderDetails.Visible = false;
            // 
            // tsmiWarehouseChecklist
            // 
            this.tsmiWarehouseChecklist.Name = "tsmiWarehouseChecklist";
            this.tsmiWarehouseChecklist.Size = new System.Drawing.Size(180, 22);
            this.tsmiWarehouseChecklist.Text = "مشاهده چک لیست انبار";
            this.tsmiWarehouseChecklist.Visible = false;
            // 
            // tsmiChangeOrder
            // 
            this.tsmiChangeOrder.Name = "tsmiChangeOrder";
            this.tsmiChangeOrder.Size = new System.Drawing.Size(180, 22);
            this.tsmiChangeOrder.Text = "تغییر سفارش";
            this.tsmiChangeOrder.Visible = false;
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(180, 22);
            this.tsmiDelete.Text = "حذف";
            this.tsmiDelete.Visible = false;
            // 
            // tsmiChangeOrderLevel
            // 
            this.tsmiChangeOrderLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOrderSentOut,
            this.tsmiSentToCompany});
            this.tsmiChangeOrderLevel.Name = "tsmiChangeOrderLevel";
            this.tsmiChangeOrderLevel.Size = new System.Drawing.Size(180, 22);
            this.tsmiChangeOrderLevel.Text = "تغییر وضعیت سفارش";
            this.tsmiChangeOrderLevel.Visible = false;
            // 
            // tsmiOrderSentOut
            // 
            this.tsmiOrderSentOut.Name = "tsmiOrderSentOut";
            this.tsmiOrderSentOut.Size = new System.Drawing.Size(197, 22);
            this.tsmiOrderSentOut.Text = "اعلام تکمیل و خروج سفارش";
            this.tsmiOrderSentOut.Visible = false;
            // 
            // tsmiSentToCompany
            // 
            this.tsmiSentToCompany.Name = "tsmiSentToCompany";
            this.tsmiSentToCompany.Size = new System.Drawing.Size(197, 22);
            this.tsmiSentToCompany.Text = "ارسال سفارش به شرکت";
            this.tsmiSentToCompany.Visible = false;
            // 
            // L0900_Orders_and_Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(843, 309);
            this.Controls.Add(this.panel1);
            this.Name = "L0900_Orders_and_Details";
            this.Text = "L0900_Orders_and_Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.L0900_Orders_and_Details_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtST_OrderTitle;
        private System.Windows.Forms.TextBox txtST_CustomerName;
        private System.Windows.Forms.ComboBox cmbST_OrderTitle;
        private System.Windows.Forms.ComboBox cmbST_CustomerName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrderHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrderDetails;
        private System.Windows.Forms.ToolStripMenuItem tsmiWarehouseChecklist;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeOrder;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangeOrderLevel;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrderSentOut;
        private System.Windows.Forms.ToolStripMenuItem tsmiSentToCompany;
    }
}