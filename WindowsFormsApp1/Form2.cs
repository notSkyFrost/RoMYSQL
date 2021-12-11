using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;
using KrnlAPI;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        MySqlConnection conectate = new MySqlConnection("server=; database=; Uid=; pwd=; ");
        MySqlCommand Comando = new MySqlCommand();
        Thread th;
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(OForm3);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void OForm3(Object obj)
        {
            Application.Run(new Form3());
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                if ((Control.ModifierKeys & Keys.Control) != 0)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.ShowInTaskbar = true;
                    timer2.Enabled = false;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            MainAPI.Inject();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            conectate.Open();
            Comando.Connection = conectate;
            Comando.CommandText = ("SELECT * FROM Scripts ORDER BY RAND() LIMIT 1;");
            MySqlDataReader reader = Comando.ExecuteReader();
            richTextBox1.Clear();
            if (reader.Read())
            {
                richTextBox1.AppendText(reader.GetString("Script") + "");
                
                reader.Close();
                conectate.Close();
                timer1.Enabled = false;
                button3.Enabled = true;
                button3.Visible = true;
                MainAPI.Execute(richTextBox1.Text);
            }
            else
            {
                reader.Close();
                conectate.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection conectate2 = new MySqlConnection("server=; database=; Uid=; pwd=; ");
            conectate2.Open();
            richTextBox2.AppendText(richTextBox1.Text.Replace("'", "\\'"));
            string query = "delete from Scripts where Script= '" + this.richTextBox2.Text + "';";
            MySqlCommand MyCommand2 = new MySqlCommand(query, conectate2);
            MySqlDataReader MyReader2;
            MyReader2 = MyCommand2.ExecuteReader();
            conectate2.Close();
            this.Close();
            th = new Thread(OForm2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void OForm2(object obj)
        {
            Application.Run(new Form2());
        }
    }
    }

