﻿using System;
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

    }
}
