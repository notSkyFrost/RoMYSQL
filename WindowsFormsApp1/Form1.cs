using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using KrnlAPI;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Thread th;
        MySqlConnection conectate = new MySqlConnection("server=; database=; Uid=; pwd=; ");
        MySqlCommand Comando = new MySqlCommand();
        public Form1()
        {
       InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            timer1.Enabled = true;
           
            checkBox3.Enabled = true;
            checkBox3.Visible = true;
            checkBox1.Visible = false;
            checkBox1.Enabled = false;
            MainAPI.Inject();
            conectate.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            conectate.Open();
            Comando.Connection = conectate;
            Comando.CommandText = ("SELECT * FROM Scripts ORDER BY RAND() LIMIT 1;");
            MySqlDataReader reader = Comando.ExecuteReader();
            richTextBox1.Clear();
            richTextBox2.Clear();
            if (reader.Read())
            {
                richTextBox1.AppendText(reader.GetString("Script") + "");
                richTextBox2.AppendText(richTextBox1.Text.Replace("'", "\\'"));
                string query = "delete from Scripts where Script= '" + this.richTextBox2.Text + "';";
                reader.Close();
                MySqlCommand MyCommand2 = new MySqlCommand(query, conectate);
                MySqlDataReader MyReader2;
                MyReader2 = MyCommand2.ExecuteReader();
                conectate.Close();
                MainAPI.Execute(richTextBox1.Text);
            }
            else
            {
                reader.Close();
                conectate.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            checkBox3.Enabled = false;
            checkBox3.Visible = false;
            checkBox1.Visible = true;
            checkBox1.Enabled = true;
            conectate.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(Form2);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void Form2(object obj)
        {
            Application.Run(new Form2());
        }
    }
    }

