namespace db_ORM
{
    partial class mainform
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_branch = new System.Windows.Forms.DataGridView();
            this.dgv_numbers_if_inventory = new System.Windows.Forms.DataGridView();
            this.tlp = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_branch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_numbers_if_inventory)).BeginInit();
            this.tlp.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_branch
            // 
            this.dgv_branch.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_branch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_branch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_branch.Location = new System.Drawing.Point(3, 3);
            this.dgv_branch.Name = "dgv_branch";
            this.dgv_branch.RowHeadersWidth = 51;
            this.dgv_branch.RowTemplate.Height = 24;
            this.dgv_branch.Size = new System.Drawing.Size(1236, 302);
            this.dgv_branch.TabIndex = 0;
            this.dgv_branch.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgv_branch_RowValidating);
            this.dgv_branch.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgv_branch_UserDeletingRow);
            // 
            // dgv_numbers_if_inventory
            // 
            this.dgv_numbers_if_inventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_numbers_if_inventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_numbers_if_inventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_numbers_if_inventory.Location = new System.Drawing.Point(3, 311);
            this.dgv_numbers_if_inventory.Name = "dgv_numbers_if_inventory";
            this.dgv_numbers_if_inventory.RowHeadersWidth = 51;
            this.dgv_numbers_if_inventory.RowTemplate.Height = 24;
            this.dgv_numbers_if_inventory.Size = new System.Drawing.Size(1236, 303);
            this.dgv_numbers_if_inventory.TabIndex = 1;
            // 
            // tlp
            // 
            this.tlp.ColumnCount = 1;
            this.tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Controls.Add(this.dgv_branch, 0, 0);
            this.tlp.Controls.Add(this.dgv_numbers_if_inventory, 0, 1);
            this.tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp.Location = new System.Drawing.Point(0, 0);
            this.tlp.Name = "tlp";
            this.tlp.RowCount = 2;
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp.Size = new System.Drawing.Size(1242, 617);
            this.tlp.TabIndex = 2;
            // 
            // mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 617);
            this.Controls.Add(this.tlp);
            this.Name = "mainform";
            this.Text = "Инвентарь в филиалах";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_branch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_numbers_if_inventory)).EndInit();
            this.tlp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_branch;
        private System.Windows.Forms.DataGridView dgv_numbers_if_inventory;
        private System.Windows.Forms.TableLayoutPanel tlp;
    }
}

