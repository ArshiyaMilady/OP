namespace OrdersProgress
{
    partial class J1900_OtherSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAutomaticWarehouseBooking = new System.Windows.Forms.CheckBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chkAutomaticWarehouseBooking);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 288);
            this.panel1.TabIndex = 0;
            // 
            // chkAutomaticWarehouseBooking
            // 
            this.chkAutomaticWarehouseBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutomaticWarehouseBooking.AutoSize = true;
            this.chkAutomaticWarehouseBooking.Location = new System.Drawing.Point(31, 12);
            this.chkAutomaticWarehouseBooking.Name = "chkAutomaticWarehouseBooking";
            this.chkAutomaticWarehouseBooking.Size = new System.Drawing.Size(483, 23);
            this.chkAutomaticWarehouseBooking.TabIndex = 1;
            this.chkAutomaticWarehouseBooking.Text = "فعال کردن رزرو اتوماتیک کالاهای درخواست شده یا سفارش شده از انبار بعد تأیید نهایی" +
    "";
            this.chkAutomaticWarehouseBooking.UseVisualStyleBackColor = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.Location = new System.Drawing.Point(0, 294);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(85, 30);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "بستن صفحه";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(395, 294);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "ثبت تغییرات و بستن";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // J1900_OtherSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 327);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.panel1);
            this.Name = "J1900_OtherSettings";
            this.Text = "   تنظیمات برنامه";
            this.Load += new System.EventHandler(this.J1900_OtherSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAutomaticWarehouseBooking;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSave;
    }
}