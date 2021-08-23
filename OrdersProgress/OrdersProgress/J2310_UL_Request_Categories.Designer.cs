namespace OrdersProgress
{
    partial class J2310_UL_Request_Categories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(J2310_UL_Request_Categories));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.btnChooseAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvUL_RC = new System.Windows.Forms.DataGridView();
            this.colCategory_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupervisor_UL_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dgvULs = new System.Windows.Forms.DataGridView();
            this.dgvCats = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUL_RC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvULs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCats)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btnAddNew);
            this.panel1.Controls.Add(this.btnChooseAll);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnDeleteAll);
            this.panel1.Controls.Add(this.btnReturn);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.dgvUL_RC);
            this.panel1.Controls.Add(this.dgvULs);
            this.panel1.Controls.Add(this.dgvCats);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(843, 465);
            this.panel1.TabIndex = 4;
            this.panel1.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Maroon;
            this.checkBox1.Location = new System.Drawing.Point(71, 211);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(332, 21);
            this.checkBox1.TabIndex = 99;
            this.checkBox1.Text = "برای دسته کالای انتخاب شده ، نیاز به تأیید سرپرست نمی باشد";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // btnAddNew
            // 
            this.btnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddNew.BackgroundImage")));
            this.btnAddNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNew.Location = new System.Drawing.Point(13, 238);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(817, 37);
            this.btnAddNew.TabIndex = 98;
            this.btnAddNew.UseVisualStyleBackColor = false;
            this.btnAddNew.Click += new System.EventHandler(this.BtnAddNew_Click);
            // 
            // btnChooseAll
            // 
            this.btnChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChooseAll.Location = new System.Drawing.Point(236, 434);
            this.btnChooseAll.Name = "btnChooseAll";
            this.btnChooseAll.Size = new System.Drawing.Size(127, 26);
            this.btnChooseAll.TabIndex = 97;
            this.btnChooseAll.Text = "انتخاب همه";
            this.btnChooseAll.UseVisualStyleBackColor = true;
            this.btnChooseAll.Visible = false;
            this.btnChooseAll.Click += new System.EventHandler(this.BtnChooseAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 38);
            this.label1.TabIndex = 95;
            this.label1.Text = "از جدول زیر سطح کاربری سرپرست مربوط به دسته انتخاب شده جهت تأیید درخواست کالا را " +
    "مشخص نمایید";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(476, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 38);
            this.label2.TabIndex = 95;
            this.label2.Text = "از جدول زیر دسته کالاهایی که این سطح کاربری می تواند از انبار درخواست نماید را مش" +
    "خص نمایید";
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDeleteAll.Location = new System.Drawing.Point(93, 432);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(128, 30);
            this.btnDeleteAll.TabIndex = 94;
            this.btnDeleteAll.Text = "حذف تمام رابطه ها";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAll_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReturn.Location = new System.Drawing.Point(12, 432);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(75, 30);
            this.btnReturn.TabIndex = 94;
            this.btnReturn.Text = "بازگشت";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.BtnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(730, 432);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 30);
            this.btnSave.TabIndex = 94;
            this.btnSave.Text = "ثبت تغییرات";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // dgvUL_RC
            // 
            this.dgvUL_RC.AllowUserToAddRows = false;
            this.dgvUL_RC.AllowUserToDeleteRows = false;
            this.dgvUL_RC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUL_RC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUL_RC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCategory_Name,
            this.colSupervisor_UL_Description,
            this.colRemove});
            this.dgvUL_RC.Location = new System.Drawing.Point(13, 281);
            this.dgvUL_RC.Name = "dgvUL_RC";
            this.dgvUL_RC.ReadOnly = true;
            this.dgvUL_RC.Size = new System.Drawing.Size(817, 145);
            this.dgvUL_RC.TabIndex = 93;
            this.dgvUL_RC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvUL_RC_CellContentClick);
            // 
            // colCategory_Name
            // 
            this.colCategory_Name.HeaderText = "نام دسته";
            this.colCategory_Name.Name = "colCategory_Name";
            this.colCategory_Name.ReadOnly = true;
            this.colCategory_Name.Width = 250;
            // 
            // colSupervisor_UL_Description
            // 
            this.colSupervisor_UL_Description.HeaderText = "سطح کاربری";
            this.colSupervisor_UL_Description.Name = "colSupervisor_UL_Description";
            this.colSupervisor_UL_Description.ReadOnly = true;
            this.colSupervisor_UL_Description.Width = 250;
            // 
            // colRemove
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colRemove.DefaultCellStyle = dataGridViewCellStyle1;
            this.colRemove.HeaderText = " ";
            this.colRemove.Name = "colRemove";
            this.colRemove.ReadOnly = true;
            this.colRemove.Text = "حذف";
            this.colRemove.UseColumnTextForButtonValue = true;
            this.colRemove.Width = 50;
            // 
            // dgvULs
            // 
            this.dgvULs.AllowUserToAddRows = false;
            this.dgvULs.AllowUserToDeleteRows = false;
            this.dgvULs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvULs.Location = new System.Drawing.Point(13, 48);
            this.dgvULs.Name = "dgvULs";
            this.dgvULs.ReadOnly = true;
            this.dgvULs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvULs.Size = new System.Drawing.Size(390, 158);
            this.dgvULs.TabIndex = 93;
            // 
            // dgvCats
            // 
            this.dgvCats.AllowUserToAddRows = false;
            this.dgvCats.AllowUserToDeleteRows = false;
            this.dgvCats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCats.Location = new System.Drawing.Point(439, 48);
            this.dgvCats.Name = "dgvCats";
            this.dgvCats.ReadOnly = true;
            this.dgvCats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCats.Size = new System.Drawing.Size(391, 186);
            this.dgvCats.TabIndex = 93;
            // 
            // J2310_UL_Request_Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnReturn;
            this.ClientSize = new System.Drawing.Size(841, 465);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "J2310_UL_Request_Categories";
            this.Text = "J2310_UL_Request_Categories";
            this.Shown += new System.EventHandler(this.J2310_UL_Request_Categories_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUL_RC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvULs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCats)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnChooseAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvCats;
        private System.Windows.Forms.DataGridView dgvULs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUL_RC;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupervisor_UL_Description;
        private System.Windows.Forms.DataGridViewButtonColumn colRemove;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}