using System;
using System.Collections.Generic;
using System.Data;
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
            dgv_branch.Columns.Add("branch_address", "Адрес филиала");
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

        public static DataTable GetInventoryNames()
        {
            using (var db_context = new opendata_context())
            {
                var table = new DataTable();
                table.Columns.Add(new DataColumn { ColumnName = "inventory_id", DataType = typeof(int) });
                table.Columns.Add("inventory_name");
                foreach (var inventory in db_context.inventories)
                {
                    table.Rows.Add(inventory.inventory_id,
                                   inventory.inventory_name);
                }
                return table;
            }
        }

        public static DataTable GetBranchAddresses()
        {
            using (var db_context = new opendata_context())
            {
                var table = new DataTable();
                table.Columns.Add(new DataColumn { ColumnName = "branch_id", DataType = typeof(int) });
                table.Columns.Add(new DataColumn { ColumnName = "branch_address" });
                foreach (var branch in db_context.branches)
                {
                    table.Rows.Add(branch.branch_id,
                                   branch.branch_address);
                }
                return table;
            }
        }

        public static void InitializeDGVNumbers(DataGridView dgv_numbers_if_inventory, DataGridView dgv_branches)
        {
            dgv_numbers_if_inventory.Rows.Clear();
            dgv_numbers_if_inventory.Columns.Clear();
            dgv_numbers_if_inventory.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "branch_id",
                HeaderText = "Филиал клуба",
                DisplayMember = "branch_address",
                ValueMember = "branch_id",
                DataSource = GetBranchAddresses()
            });
            dgv_numbers_if_inventory.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "inventory_id",
                HeaderText = "Название инвентаря",
                DisplayMember = "inventory_name",
                ValueMember = "inventory_id",
                DataSource = GetInventoryNames()
            });
            dgv_numbers_if_inventory.Columns.Add("number", "Количество инвентаря");
            using (var db_context = new opendata_context())
            {
                foreach (var number_of_inventory_in_branch in
                                    db_context.number_of_inventory_in_branch)
                {
                    var row_idx = dgv_numbers_if_inventory.Rows.Add(number_of_inventory_in_branch.branch_id,
                                                                    number_of_inventory_in_branch.inventory_id,
                                                                    number_of_inventory_in_branch.number);
                    dgv_numbers_if_inventory.Rows[row_idx].Tag = number_of_inventory_in_branch;
                }
            }
        }
    }
}
