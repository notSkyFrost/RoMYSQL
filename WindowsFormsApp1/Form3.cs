﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using KrnlAPI;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        Thread th;
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(OForm1);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OForm1(object obj)
        {
            Application.Run(new Form1());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainAPI.Inject();
        }
    }
}
