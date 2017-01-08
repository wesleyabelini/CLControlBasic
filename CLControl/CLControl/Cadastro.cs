using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CLControl
{
    class Cadastro
    {
        string conecstring = ConfigurationManager.ConnectionStrings["CLControl.Properties.Settings.DbControlConnectionString"].ToString();

        public void executeCmd(string comando)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                cmd.ExecuteNonQuery();

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public bool verificaTrue(string comando)
        {
            bool verifica = false;

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                verifica = reader.HasRows;

                conexao.Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return verifica;
        }

        public void formularioCliente(string comando, TextBox nome, MaskedTextBox documento, MaskedTextBox documento2, MaskedTextBox nascimento, 
            MaskedTextBox telefone, MaskedTextBox celular, MaskedTextBox cep, TextBox logradouro, TextBox numero, TextBox complemento, TextBox bairro, 
            TextBox cidade, TextBox estado)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    nome.Text = reader["NOME"].ToString();
                    documento.Text = reader["DOCUMENTO"].ToString();
                    documento2.Text = documento.Text;
                    nascimento.Text = reader["NASCIMENTO"].ToString();
                    telefone.Text = reader["TELEFONE"].ToString();
                    celular.Text = reader["CELULAR"].ToString();
                    cep.Text = reader["CEP"].ToString();
                    logradouro.Text = reader["LOGRADOURO"].ToString();
                    numero.Text = reader["NUMERO"].ToString();
                    complemento.Text = reader["COMPLEMENTO"].ToString();
                    bairro.Text = reader["BAIRRO"].ToString();
                    cidade.Text = reader["CIDADE"].ToString();
                    estado.Text = reader["ESTADO"].ToString();
                }

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public void listaCombo(string comando, ComboBox combo)
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlDataAdapter ad = new SqlDataAdapter(comando, conexao);

                DataTable table = new DataTable();
                table.Clear();
                ad.Fill(table);

                combo.DataSource = table;
                combo.DisplayMember = "PROFISSIONAL";
                combo.ValueMember = "ID_PROF";
                combo.SelectedIndex = -1;
                combo.Refresh();

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }

        public int returnID(string comando, string tabela)
        {
            int id = 0;

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    id = Convert.ToInt16(reader[tabela].ToString());
                }

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return id;
        }

        public string returnText(string comando, string tabela)
        {
            string texto = "";

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlCommand cmd = new SqlCommand(comando, conexao);
                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    texto = reader[tabela].ToString();
                }

                conexao.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }

            return texto;
        }

        public void preencheDatagrid(string comando, DataGridView data)
        {
            data.DataSource = "";
            data.Refresh();

            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = conecstring;

            try
            {
                conexao.Open();

                SqlDataAdapter da = new SqlDataAdapter(comando, conexao);

                DataTable table = new DataTable();
                table.Clear();
                da.Fill(table);

                data.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
        }
    }
}
