using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_ORM
{
    public partial class mainform : Form
    {
        public mainform()
        {
            InitializeComponent();
            database_funcs.InitialiseDGVBranches(dgv_branch);
            database_funcs.InitializeDGVNumbers(dgv_numbers_if_inventory, dgv_branch);
        }

        private void dgv_branch_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            int temp = 0;
            if (!dgv_branch.IsCurrentRowDirty)
                return;
            var row = dgv_branch.Rows[e.RowIndex];
            row.ErrorText = "";

            var cellsWithPotentialErrors = new[] {row.Cells["branch_address"],
                                                  row.Cells["branch_phone"],
                                                  row.Cells["branch_working_hours"]};
            foreach (var cell in cellsWithPotentialErrors)
            {
                cell.ErrorText = "";
                if (string.IsNullOrWhiteSpace((string)cell.Value))
                {
                    cell.ErrorText = "Значение не может быть пустым";
                    e.Cancel = true;
                }
            }
            row.Cells["branch_area"].ErrorText = "";
            if (row.Cells["branch_area"] == null
                || !Int32.TryParse(Convert.ToString(row.Cells["branch_area"].Value), out temp))
            {
                row.Cells["branch_area"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                var branch_id = (int?)row.Cells["branch_id"].Value;
                if (branch_id.HasValue)
                {
                    var cur_branch = (branch)row.Tag;
                    using (var db_contex = new opendata_context())
                    {
                        db_contex.branches.Attach(cur_branch);
                        cur_branch.branch_address = (string)row.Cells["branch_address"].Value;
                        cur_branch.branch_area = Convert.ToInt32(row.Cells["branch_area"].Value);
                        cur_branch.branch_phone = (string)row.Cells["branch_phone"].Value;
                        cur_branch.branch_working_hours = (string)row.Cells["branch_working_hours"].Value;
                        db_contex.SaveChanges();
                        row.Tag = cur_branch;
                    }
                }
                else
                {
                    using (var db_context = new opendata_context())
                    {
                        var cur_branch = new branch()
                        {
                            branch_address = (string)row.Cells["branch_address"].Value,
                            branch_area = Convert.ToInt32(row.Cells["branch_area"].Value),
                            branch_phone = (string)row.Cells["branch_phone"].Value,
                            branch_working_hours = (string)row.Cells["branch_working_hours"].Value
                        };
                        db_context.branches.Add(cur_branch);
                        db_context.SaveChanges();
                        row.Tag = cur_branch;
                    }
                }
                ((DataGridViewComboBoxColumn)dgv_numbers_if_inventory.Columns["branch_id"]).DataSource = database_funcs.GetBranchAddresses();
            }
        }
    }
}
