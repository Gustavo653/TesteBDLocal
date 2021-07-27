using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;
using Microsoft.VisualBasic;

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
            string Prompt = "Informe o nome do Banco de Dados a ser criado.Ex: Teste.sdf";
            string Resultado = Interaction.InputBox(Prompt, "CaminhoBD", @"C:\Users\Public\Documents\DB.sdf", 650, 350);
            /* verifica se o resultado é uma string vazia o que indica que foi cancelado. */
            if (Resultado != "")
            {
                if (!Resultado.Contains(".sdf"))
                {
                    MessageBox.Show("Informe a extensão .sdf no arquivo...");
                    return;
                }
                try
                {
                    string connectionString;
                    string nomeArquivoBD = Resultado;
                    string senha = "";

                    if (File.Exists(nomeArquivoBD))
                    {
                        if (MessageBox.Show("O arquivo já existe !. Deseja excluir e criar novamente ? ", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            File.Delete(nomeArquivoBD);
                        }
                        else
                        {
                            return;
                        }
                    }

                    connectionString = string.Format("DataSource=\"{0}\"; Password='{1}'", nomeArquivoBD, senha);

                    if (MessageBox.Show("Será criado arquivo " + connectionString + " Confirma ? ", "Criar", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        SqlCeEngine SqlEng = new SqlCeEngine(connectionString);
                        SqlEng.CreateDatabase();
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("A operação foi cancelada...");
            }
        }
    }    
}
