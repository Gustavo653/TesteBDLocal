using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;

namespace TesteBDLocal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString;
            string nomeArquivoBD = @"C:\Users\Public\Documents\DB.sdf";
            string senha = "";
            connectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", nomeArquivoBD, senha);
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }

            SqlCeCommand cmd;
            string sql = "insert into  tabelaBD"
                        + "(nome) "
                        + "values (@nome)";

            cmd = new SqlCeCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nome", textBox1.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString;
            string nomeArquivoBD = @"C:\Users\Public\Documents\DB.sdf";
            string senha = "";
            connectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", nomeArquivoBD, senha);
            if (!File.Exists(nomeArquivoBD))
            {
                SqlCeEngine SqlEng = new SqlCeEngine(connectionString);
                SqlEng.CreateDatabase();
                connectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", nomeArquivoBD, senha);
                SqlCeConnection cn = new SqlCeConnection(connectionString);
                cn.Open();
                SqlCeCommand cmd;
                string sql = "create table " + "tabelaBD" + "("
                           + "Nome nvarchar (60) not null)";
                cmd = new SqlCeCommand(sql, cn);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString;
            string nomeArquivoBD = @"C:\Users\Public\Documents\DB.sdf";
            string senha = "";
            connectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", nomeArquivoBD, senha);
            SqlCeConnection cn = new SqlCeConnection(connectionString);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            SqlCeCommand cmd = new SqlCeCommand("tabelaBD", cn);
            cmd.CommandType = CommandType.TableDirect;
            SqlCeResultSet rs = cmd.ExecuteResultSet(ResultSetOptions.Scrollable);
            dataGridView1.DataSource = rs;
        }
    }
}
