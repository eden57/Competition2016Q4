using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        const string connStr = "Data Source=192.168.34.12,1433;Initial Catalog=Intersect_Data;User ID=sa;Password=sqlserver12";

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                   
                    string sql = "Select * from [AB02_SECTION]";
                    IEnumerable<dynamic> result = conn.Query<dynamic>(sql);


                    dataGridView.DataSource = DataConvert.ToDataTable(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    

                    int w = 14000, h = 35000, maxW = 57000, maxH = 57000;

                    for(; w<maxW; w += 1000)
                    {
                        h = 35000;
                        for(; h<maxH; h += 1000)
                        {
                            string sql = "INSERT INTO AB02_SECTION Select * from [AB02非建设用地图斑2009] WHERE shape.STIntersects(geometry::STGeomFromText('POLYGON(({0} {1}, {0} {3}, {2} {3}, {2} {1}, {0} {1}))', 0))=1 ";
                            sql = string.Format(sql, w, h, w+ 1000, h+ 1000);

                            conn.Execute(sql);
                        }
                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    dynamic result = conn.Query<dynamic>("SearchIntersectGeometry", null, null, true, null, CommandType.StoredProcedure);

                    MessageBox.Show("该过程漫长，请先去看部电影");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
