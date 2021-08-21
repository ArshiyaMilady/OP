namespace OrdersProgress
{
    partial class J1000_MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(J1000_MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiInternalFeatures = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers_Show_Change = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsersLevels = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserLevelsFeatures = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoginsHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrdersFeatures = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrdersLevels = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrders_and_Details = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShowOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrdersPriority = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOrders_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrders_Priorities = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrders_ReportProgress = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserPriorities = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSuggestedPriorities = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWarehouse = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWarehouseItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWarehouses = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItems = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModules = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOPC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiActions = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tsmiOtherSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Enabled = false;
            this.menuStrip.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInternalFeatures,
            this.tsmiOrders,
            this.tsmiWarehouse,
            this.tsmiProducts});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(705, 27);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsmiInternalFeatures
            // 
            this.tsmiInternalFeatures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsers,
            this.tsmiOrdersFeatures,
            this.tsmiOtherSettings});
            this.tsmiInternalFeatures.Name = "tsmiInternalFeatures";
            this.tsmiInternalFeatures.Size = new System.Drawing.Size(101, 23);
            this.tsmiInternalFeatures.Text = "ابزارهای جانبی";
            // 
            // tsmiUsers
            // 
            this.tsmiUsers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsers_Show_Change,
            this.tsmiUsersLevels,
            this.tsmiUserLevelsFeatures,
            this.tsmiLoginsHistory});
            this.tsmiUsers.Name = "tsmiUsers";
            this.tsmiUsers.Size = new System.Drawing.Size(180, 24);
            this.tsmiUsers.Text = "کاربران";
            // 
            // tsmiUsers_Show_Change
            // 
            this.tsmiUsers_Show_Change.Name = "tsmiUsers_Show_Change";
            this.tsmiUsers_Show_Change.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.tsmiUsers_Show_Change.Size = new System.Drawing.Size(235, 24);
            this.tsmiUsers_Show_Change.Text = "مشاهده / تغییر";
            this.tsmiUsers_Show_Change.Visible = false;
            this.tsmiUsers_Show_Change.Click += new System.EventHandler(this.TsmiUsers_Show_Change_Click);
            // 
            // tsmiUsersLevels
            // 
            this.tsmiUsersLevels.Name = "tsmiUsersLevels";
            this.tsmiUsersLevels.Size = new System.Drawing.Size(235, 24);
            this.tsmiUsersLevels.Text = "سطوح کاربری";
            this.tsmiUsersLevels.Visible = false;
            this.tsmiUsersLevels.Click += new System.EventHandler(this.TsmiUsersLevels_Click);
            // 
            // tsmiUserLevelsFeatures
            // 
            this.tsmiUserLevelsFeatures.Name = "tsmiUserLevelsFeatures";
            this.tsmiUserLevelsFeatures.Size = new System.Drawing.Size(235, 24);
            this.tsmiUserLevelsFeatures.Text = "امکانات سطوح کاربری";
            this.tsmiUserLevelsFeatures.Visible = false;
            this.tsmiUserLevelsFeatures.Click += new System.EventHandler(this.TsmiUserLevelsFeatures_Click);
            // 
            // tsmiLoginsHistory
            // 
            this.tsmiLoginsHistory.Name = "tsmiLoginsHistory";
            this.tsmiLoginsHistory.Size = new System.Drawing.Size(235, 24);
            this.tsmiLoginsHistory.Text = "تاریخچه ورودها";
            this.tsmiLoginsHistory.Visible = false;
            this.tsmiLoginsHistory.Click += new System.EventHandler(this.TsmiLoginsHistory_Click);
            // 
            // tsmiOrdersFeatures
            // 
            this.tsmiOrdersFeatures.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOrdersLevels,
            this.tsmiOrders_and_Details});
            this.tsmiOrdersFeatures.Name = "tsmiOrdersFeatures";
            this.tsmiOrdersFeatures.Size = new System.Drawing.Size(180, 24);
            this.tsmiOrdersFeatures.Text = "سفارشها";
            // 
            // tsmiOrdersLevels
            // 
            this.tsmiOrdersLevels.Name = "tsmiOrdersLevels";
            this.tsmiOrdersLevels.Size = new System.Drawing.Size(200, 24);
            this.tsmiOrdersLevels.Text = "مراحل انجام سفارشها";
            this.tsmiOrdersLevels.Click += new System.EventHandler(this.TsmiOrdersLevels_Click);
            // 
            // tsmiOrders_and_Details
            // 
            this.tsmiOrders_and_Details.Name = "tsmiOrders_and_Details";
            this.tsmiOrders_and_Details.Size = new System.Drawing.Size(200, 24);
            this.tsmiOrders_and_Details.Text = "سفارشها با تمام جزییات";
            this.tsmiOrders_and_Details.Click += new System.EventHandler(this.TsmiOrders_and_Details_Click);
            // 
            // tsmiOrders
            // 
            this.tsmiOrders.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewOrder,
            this.tsmiShowOrders,
            this.tsmiCustomers,
            this.tsmiOrdersPriority,
            this.toolStripSeparator1,
            this.tsmiOrders_Show,
            this.tsmiOrders_Priorities,
            this.tsmiOrders_ReportProgress});
            this.tsmiOrders.Name = "tsmiOrders";
            this.tsmiOrders.Size = new System.Drawing.Size(65, 23);
            this.tsmiOrders.Text = "سفارشها";
            // 
            // tsmiNewOrder
            // 
            this.tsmiNewOrder.Name = "tsmiNewOrder";
            this.tsmiNewOrder.Size = new System.Drawing.Size(227, 24);
            this.tsmiNewOrder.Text = "ثبت سفارش جدید";
            this.tsmiNewOrder.Click += new System.EventHandler(this.TsmiNewOrder_Click);
            // 
            // tsmiShowOrders
            // 
            this.tsmiShowOrders.Name = "tsmiShowOrders";
            this.tsmiShowOrders.Size = new System.Drawing.Size(227, 24);
            this.tsmiShowOrders.Text = "مشاهده سفارشها";
            this.tsmiShowOrders.Click += new System.EventHandler(this.TsmiShowOrders_Click);
            // 
            // tsmiCustomers
            // 
            this.tsmiCustomers.Name = "tsmiCustomers";
            this.tsmiCustomers.Size = new System.Drawing.Size(227, 24);
            this.tsmiCustomers.Text = "خریداران";
            this.tsmiCustomers.Click += new System.EventHandler(this.TsmiCustomers_Click);
            // 
            // tsmiOrdersPriority
            // 
            this.tsmiOrdersPriority.Name = "tsmiOrdersPriority";
            this.tsmiOrdersPriority.Size = new System.Drawing.Size(227, 24);
            this.tsmiOrdersPriority.Text = "تعیین اولویت ارسال سفارشها";
            this.tsmiOrdersPriority.Visible = false;
            this.tsmiOrdersPriority.Click += new System.EventHandler(this.TsmiOrdersPriority_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(224, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // tsmiOrders_Show
            // 
            this.tsmiOrders_Show.Name = "tsmiOrders_Show";
            this.tsmiOrders_Show.Size = new System.Drawing.Size(227, 24);
            this.tsmiOrders_Show.Text = "- نمایش سفارشها";
            this.tsmiOrders_Show.Visible = false;
            // 
            // tsmiOrders_Priorities
            // 
            this.tsmiOrders_Priorities.Name = "tsmiOrders_Priorities";
            this.tsmiOrders_Priorities.Size = new System.Drawing.Size(227, 24);
            this.tsmiOrders_Priorities.Text = "- اولویتها";
            this.tsmiOrders_Priorities.Visible = false;
            this.tsmiOrders_Priorities.Click += new System.EventHandler(this.TsmiOrders_Priorities_Click);
            // 
            // tsmiOrders_ReportProgress
            // 
            this.tsmiOrders_ReportProgress.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUserPriorities,
            this.tsmiSuggestedPriorities});
            this.tsmiOrders_ReportProgress.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmiOrders_ReportProgress.Name = "tsmiOrders_ReportProgress";
            this.tsmiOrders_ReportProgress.Size = new System.Drawing.Size(227, 24);
            this.tsmiOrders_ReportProgress.Text = "- گزارش پیشرفت";
            this.tsmiOrders_ReportProgress.Visible = false;
            this.tsmiOrders_ReportProgress.Click += new System.EventHandler(this.TsmiOrders_ReportProgress_Click);
            // 
            // tsmiUserPriorities
            // 
            this.tsmiUserPriorities.Name = "tsmiUserPriorities";
            this.tsmiUserPriorities.Size = new System.Drawing.Size(239, 24);
            this.tsmiUserPriorities.Text = "بر اساس اولویت بندی کاربر";
            this.tsmiUserPriorities.Click += new System.EventHandler(this.TsmiUserPriorities_Click);
            // 
            // tsmiSuggestedPriorities
            // 
            this.tsmiSuggestedPriorities.Name = "tsmiSuggestedPriorities";
            this.tsmiSuggestedPriorities.Size = new System.Drawing.Size(239, 24);
            this.tsmiSuggestedPriorities.Text = "بر اساس اولویت بندی پیشنهادی";
            this.tsmiSuggestedPriorities.Click += new System.EventHandler(this.TsmiSuggestedPriorities_Click);
            // 
            // tsmiWarehouse
            // 
            this.tsmiWarehouse.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiWarehouseItems,
            this.tsmiWarehouses});
            this.tsmiWarehouse.Name = "tsmiWarehouse";
            this.tsmiWarehouse.Size = new System.Drawing.Size(44, 23);
            this.tsmiWarehouse.Text = "انبار";
            // 
            // tsmiWarehouseItems
            // 
            this.tsmiWarehouseItems.Name = "tsmiWarehouseItems";
            this.tsmiWarehouseItems.Size = new System.Drawing.Size(152, 24);
            this.tsmiWarehouseItems.Text = "موجودی";
            this.tsmiWarehouseItems.Click += new System.EventHandler(this.TsmiStock_Inventory_Click);
            // 
            // tsmiWarehouses
            // 
            this.tsmiWarehouses.Name = "tsmiWarehouses";
            this.tsmiWarehouses.Size = new System.Drawing.Size(152, 24);
            this.tsmiWarehouses.Text = "مشاهده انبارها";
            this.tsmiWarehouses.Click += new System.EventHandler(this.TsmiWarehouses_Click);
            // 
            // tsmiProducts
            // 
            this.tsmiProducts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProperties,
            this.tsmiItems,
            this.tsmiCategories,
            this.tsmiModules,
            this.tsmiOPC,
            this.tsmiActions});
            this.tsmiProducts.Name = "tsmiProducts";
            this.tsmiProducts.Size = new System.Drawing.Size(115, 23);
            this.tsmiProducts.Text = "تعریف محصولات";
            // 
            // tsmiProperties
            // 
            this.tsmiProperties.Name = "tsmiProperties";
            this.tsmiProperties.Size = new System.Drawing.Size(162, 24);
            this.tsmiProperties.Text = "مشخصات";
            this.tsmiProperties.Click += new System.EventHandler(this.TsmiProperties_Click);
            // 
            // tsmiItems
            // 
            this.tsmiItems.Name = "tsmiItems";
            this.tsmiItems.Size = new System.Drawing.Size(162, 24);
            this.tsmiItems.Text = "قطعات";
            this.tsmiItems.Click += new System.EventHandler(this.tsmiItems_Click);
            // 
            // tsmiCategories
            // 
            this.tsmiCategories.Name = "tsmiCategories";
            this.tsmiCategories.Size = new System.Drawing.Size(162, 24);
            this.tsmiCategories.Text = "طبقه بندی کالاها";
            this.tsmiCategories.Visible = false;
            this.tsmiCategories.Click += new System.EventHandler(this.TsmiCategories_Click);
            // 
            // tsmiModules
            // 
            this.tsmiModules.Name = "tsmiModules";
            this.tsmiModules.Size = new System.Drawing.Size(162, 24);
            this.tsmiModules.Text = "ماژول ها";
            this.tsmiModules.Visible = false;
            this.tsmiModules.Click += new System.EventHandler(this.TsmiModules_Click);
            // 
            // tsmiOPC
            // 
            this.tsmiOPC.Name = "tsmiOPC";
            this.tsmiOPC.Size = new System.Drawing.Size(162, 24);
            this.tsmiOPC.Text = "OPC";
            this.tsmiOPC.Visible = false;
            this.tsmiOPC.Click += new System.EventHandler(this.TsmiOPC_Click);
            // 
            // tsmiActions
            // 
            this.tsmiActions.Name = "tsmiActions";
            this.tsmiActions.Size = new System.Drawing.Size(162, 24);
            this.tsmiActions.Text = "فعالیت ها";
            this.tsmiActions.Visible = false;
            this.tsmiActions.Click += new System.EventHandler(this.TsmiActions_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 266);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(705, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel Worksheets|*.xls;*.xlsx";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(0, 265);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(45, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "خروج";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // tsmiOtherSettings
            // 
            this.tsmiOtherSettings.Name = "tsmiOtherSettings";
            this.tsmiOtherSettings.Size = new System.Drawing.Size(180, 24);
            this.tsmiOtherSettings.Text = "تنظیمات برنامه";
            this.tsmiOtherSettings.Visible = false;
            this.tsmiOtherSettings.Click += new System.EventHandler(this.TsmiOtherSettings_Click);
            // 
            // J1000_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(705, 288);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "J1000_MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   OP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.J1000_MainForm_Load);
            this.Shown += new System.EventHandler(this.J1000_MainForm_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrders;
        private System.Windows.Forms.ToolStripMenuItem tsmiWarehouse;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrders_Show;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrders_Priorities;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrders_ReportProgress;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserPriorities;
        private System.Windows.Forms.ToolStripMenuItem tsmiSuggestedPriorities;
        private System.Windows.Forms.ToolStripMenuItem tsmiWarehouseItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiProducts;
        private System.Windows.Forms.ToolStripMenuItem tsmiModules;
        private System.Windows.Forms.ToolStripMenuItem tsmiItems;
        private System.Windows.Forms.ToolStripMenuItem tsmiOPC;
        private System.Windows.Forms.ToolStripMenuItem tsmiProperties;
        private System.Windows.Forms.ToolStripMenuItem tsmiActions;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewOrder;
        private System.Windows.Forms.ToolStripMenuItem tsmiCustomers;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowOrders;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem tsmiInternalFeatures;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsers;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsers_Show_Change;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsersLevels;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserLevelsFeatures;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrdersFeatures;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrdersLevels;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrders_and_Details;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoginsHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiOrdersPriority;
        private System.Windows.Forms.ToolStripMenuItem tsmiWarehouses;
        private System.Windows.Forms.ToolStripMenuItem tsmiCategories;
        private System.Windows.Forms.ToolStripMenuItem tsmiOtherSettings;
    }
}



