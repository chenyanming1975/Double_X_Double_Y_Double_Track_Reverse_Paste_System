﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralMachine
{
    public partial class frm_Permission : Form
    {
        private frm_Main frm_Main = null;
        public frm_Permission(Object obj)
        {
            InitializeComponent();
            frm_Main = (frm_Main)obj;
        }
        public frm_Permission()
        {
            InitializeComponent();
        }
    }
}
