using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
        }

        private void InitData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UP_SelectInfo", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 3600;
            cn.Open();

            SqlDataAdapter sdap = new SqlDataAdapter();
            sdap.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sdap.Fill(dt);
            cn.Close();

            this.dataGridView1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            InitData();
        }
    }
}
