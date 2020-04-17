using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_ORM
{
    class database_funcs
    {
        public static void InitialiseDGVBranches(DataGridView dgv_branch)
        {
            dgv_branch.Rows.Clear();
            dgv_branch.Columns.Clear();
            dgv_branch.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "branch_id",
                Visible = false
            });
            dgv_branch.Columns.Add("branch_name", "Адрес филиала");
            dgv_branch.Columns.Add("branch_phone", "Телефон ресепшена");
            dgv_branch.Columns.Add("branch_area", "Площадь помещения");
            dgv_branch.Columns.Add("branch_working_hours", "Часы работы");
            using (var db_context = new opendata_context())
            {
                foreach (var branch in db_context.branches)
                {
                    var row_idx = dgv_branch.Rows.Add(branch.branch_id,
                                                      branch.branch_address,
                                                      branch.branch_phone,
                                                      branch.branch_area,
                                                      branch.branch_working_hours);
                    dgv_branch.Rows[row_idx].Tag = branch;
                }
            }
        }
    }
}
