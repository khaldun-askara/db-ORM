using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private static string DelSpaces(string str)
        {
            if (str == "" || str == null)
                return "";
            Regex spaces1 = new Regex("(?<![^ \f\n\r\t\v])[ \f\n\r\t\v]{1,}(?=[^ \f\n\r\t\v])");
            Regex spaces2 = new Regex("(?<=[^ \f\n\r\t\v])[ \f\n\r\t\v]{1,}(?![^ \f\n\r\t\v])");
            return spaces2.Replace(spaces1.Replace(str, ""), "");
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
                cell.Value = DelSpaces((string)cell.Value);
                if (string.IsNullOrWhiteSpace((string)cell.Value))
                {
                    cell.ErrorText = "Значение не может быть пустым";
                    e.Cancel = true;
                }
            }
            if (row.Tag != null && (string)row.Cells["branch_address"].Value != ((branch)row.Tag).branch_address && database_funcs.FindBranchName((string)row.Cells["branch_address"].Value)
                || row.Tag == null && database_funcs.FindBranchName((string)row.Cells["branch_address"].Value))
            {
                row.Cells["branch_address"].ErrorText = "Филиал по данному адресу уже существует.";
                e.Cancel = true;
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
                    using (var db_context = new opendata_context())
                    {
                        db_context.branches.Attach(cur_branch);
                        cur_branch.branch_address = (string)row.Cells["branch_address"].Value;
                        cur_branch.branch_area = Convert.ToInt32(row.Cells["branch_area"].Value);
                        cur_branch.branch_phone = (string)row.Cells["branch_phone"].Value;
                        cur_branch.branch_working_hours = (string)row.Cells["branch_working_hours"].Value;
                        try
                        {
                            db_context.SaveChanges();
                            row.Tag = cur_branch;
                        }
                        catch (Exception except)
                        {
                            row.ErrorText = "Что-то пошло не так! " + except.Message;
                            e.Cancel = true;
                        }
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
                        try
                        {
                            db_context.SaveChanges();
                            row.Tag = cur_branch;
                        }
                        catch (Exception except)
                        {
                            row.ErrorText ="Что-то пошло не так! Возможно, такой адрес филиала уже есть. " + except.Message;
                            e.Cancel = true;
                        }
                    }
                }
                ((DataGridViewComboBoxColumn)dgv_numbers_if_inventory.Columns["branch_id"]).DataSource = database_funcs.GetBranchAddresses();
            }
        }

        private void dgv_branch_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var cur_branch_id = (int?)e.Row.Cells["branch_id"].Value;
            if (!cur_branch_id.HasValue)
                return;
            using (var db_context = new opendata_context())
            {
                var curr_branch = (branch)dgv_branch.Rows[e.Row.Index].Tag;
                db_context.branches.Attach(curr_branch);
                db_context.branches.Remove(curr_branch);
                db_context.SaveChanges();
                database_funcs.InitializeDGVNumbers(dgv_numbers_if_inventory, dgv_branch);
            }
        }

        private void dgv_numbers_if_inventory_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            int temp = 0;
            if (!dgv_numbers_if_inventory.IsCurrentRowDirty)
                return;
            var row = dgv_numbers_if_inventory.Rows[e.RowIndex];
            row.ErrorText = "";

            var comboboxCells = new[]
            {
                row.Cells["branch_id"],
                row.Cells["inventory_id"]
            };

            foreach (var cell in comboboxCells)
            {
                cell.ErrorText = "";
                if (cell.Value == null)
                {
                    cell.ErrorText = "Выберите значение!";
                    e.Cancel = true;
                }
            }
            row.Cells["number"].ErrorText = "";
            if (row.Cells["number"] == null
                || !Int32.TryParse(Convert.ToString(row.Cells["number"].Value), out temp))
            {
                row.Cells["number"].ErrorText = "Введите число";
                e.Cancel = true;
            }

            if (!e.Cancel)
            {
                if (row.Tag == null)
                {
                    using (var db_context = new opendata_context())
                    {
                        var cur_number = new number_of_inventory_in_branch()
                        {
                            branch_id = Convert.ToInt32(row.Cells["branch_id"].Value),
                            inventory_id = Convert.ToInt32(row.Cells["inventory_id"].Value),
                            number = Convert.ToInt32(row.Cells["number"].Value)
                        };
                        row.Cells["branch_id"].ReadOnly = true;
                        row.Cells["inventory_id"].ReadOnly = true;
                        db_context.number_of_inventory_in_branch.Add(cur_number);
                        try { 
                            db_context.SaveChanges();
                            row.Tag = cur_number;
                        }
                        catch (Exception except)
                        {
                            row.ErrorText = "Что-то пошло не так! Возможно, для этого инвентаря в этом филиале уже есть информация. " + except.Message;
                            e.Cancel = true;
                            row.Cells["branch_id"].ReadOnly = row.Cells["inventory_id"].ReadOnly = false;
                        }
                    }
                }
                else
                {
                    var cur_number = (number_of_inventory_in_branch)row.Tag;
                    using (var db_context = new opendata_context())
                    {
                        db_context.number_of_inventory_in_branch.Attach(cur_number);
                        //cur_number.branch_id = Convert.ToInt32(row.Cells["branch_id"].Value);
                        //cur_number.inventory_id = Convert.ToInt32(row.Cells["inventory_id"].Value);
                        cur_number.number = Convert.ToInt32(row.Cells["number"].Value);
                        db_context.SaveChanges();
                        row.Tag = cur_number;
                    }
                }
            }
        }

        private void dgv_numbers_if_inventory_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (dgv_numbers_if_inventory.Rows[e.Row.Index].Tag == null)
                return;
            using (var db_context = new opendata_context())
            {
                var curr_number = (number_of_inventory_in_branch)dgv_numbers_if_inventory.Rows[e.Row.Index].Tag;
                db_context.number_of_inventory_in_branch.Attach(curr_number);
                db_context.number_of_inventory_in_branch.Remove(curr_number);
                db_context.SaveChanges();
            }
        }

        private void dgv_branch_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgv_branch.IsCurrentRowDirty)
            {
                dgv_branch.CancelEdit();
                if (dgv_branch.CurrentRow.Cells["branch_id"].Value != null)
                {
                    dgv_branch.CurrentRow.ErrorText = "";
                    dgv_branch.CurrentRow.Cells["branch_address"].Value = ((branch)dgv_branch.CurrentRow.Tag).branch_address;
                    dgv_branch.CurrentRow.Cells["branch_phone"].Value = ((branch)dgv_branch.CurrentRow.Tag).branch_phone;
                    dgv_branch.CurrentRow.Cells["branch_area"].Value = ((branch)dgv_branch.CurrentRow.Tag).branch_area;
                    dgv_branch.CurrentRow.Cells["branch_working_hours"].Value = ((branch)dgv_branch.CurrentRow.Tag).branch_working_hours;
                    foreach (DataGridViewCell cell in dgv_branch.CurrentRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
                else
                {
                    dgv_branch.Rows.Remove(dgv_branch.CurrentRow);
                }
            }
        }

        private void dgv_numbers_if_inventory_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && dgv_numbers_if_inventory.IsCurrentRowDirty)
            {
                dgv_numbers_if_inventory.CancelEdit();
                if (dgv_numbers_if_inventory.CurrentRow.Tag != null)
                {
                    dgv_numbers_if_inventory.CurrentRow.ErrorText = "";
                    dgv_numbers_if_inventory.CurrentRow.Cells["branch_id"].Value = ((number_of_inventory_in_branch)dgv_numbers_if_inventory.CurrentRow.Tag).branch_id;
                    dgv_numbers_if_inventory.CurrentRow.Cells["inventory_id"].Value = ((number_of_inventory_in_branch)dgv_numbers_if_inventory.CurrentRow.Tag).inventory_id;
                    dgv_numbers_if_inventory.CurrentRow.Cells["number"].Value = ((number_of_inventory_in_branch)dgv_numbers_if_inventory.CurrentRow.Tag).number;
                    foreach (DataGridViewCell cell in dgv_numbers_if_inventory.CurrentRow.Cells)
                    {
                        cell.ErrorText = "";
                    }
                }
                else
                {
                    dgv_numbers_if_inventory.Rows.Remove(dgv_numbers_if_inventory.CurrentRow);
                }
            }
        }
    }
}
