using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CLControl
{
    public partial class FCliente : Form
    {
        Cadastro cadastro = new Cadastro();
        Funcao funcao = new Funcao();

        public FCliente()
        {
            InitializeComponent();
        }

        private void buttonCadastro_Click(object sender, EventArgs e)
        {
            bool nome = funcao.regularExpression(textBoxName.Text, "nome");
            bool cpf = funcao.regularExpression(maskedTextBoxCPF.Text, "cpf");
            bool nascimento = funcao.regularExpression(maskedTextBoxNascimento.Text, "nascimento");
            bool telefone = funcao.regularExpression(maskedTextBoxTelefone.Text, "telefone");
            bool celular = funcao.regularExpression(maskedTextBoxCelular.Text, "celular");
            bool cep = funcao.regularExpression(maskedTextBoxCEP.Text, "cep");
            bool logradouro = funcao.regularExpression(textBoxLogradouro.Text, "logradouro");
            bool numero = funcao.regularExpression(textBoxNumero.Text, "numero");
            bool complemento = funcao.regularExpression(textBoxComplemento.Text, "complemento");
            bool bairro = funcao.regularExpression(textBoxBairro.Text, "bairro");
            bool cidade = funcao.regularExpression(textBoxCidade.Text, "cidade");
            bool estado = funcao.regularExpression(textBoxEstado.Text, "estado");

            if(nome == true && cpf == true && nascimento ==true && telefone==true && (celular == true || maskedTextBoxCelular.Text == "(  )     -") && cep == true && 
                logradouro==true && numero ==true && (complemento==true || textBoxComplemento.Text=="") && bairro==true && cidade==true && estado==true)
            {
                MessageBox.Show("ok");
            }
            
            string cmdSelect = @"SELECT * FROM CLIENTE WHERE DOCUMENTO = '" + maskedTextBoxCPF.Text + "';";

            if(cadastro.verificaTrue(cmdSelect)==false)
            {
                string cmdInsert = @"INSERT INTO CLIENTE VALUES('" + textBoxName.Text + "', '" + maskedTextBoxCPF.Text + "', '" + maskedTextBoxNascimento.Text +
                "', '" + maskedTextBoxTelefone.Text + "', '" + maskedTextBoxCelular.Text + "', '" + maskedTextBoxCEP.Text + "', '" + textBoxLogradouro.Text +
                "', " + textBoxNumero.Text + ", '" + textBoxComplemento.Text + "', '" + textBoxBairro.Text + "', '" + textBoxCidade.Text + "', '" + textBoxEstado.Text + "')";

                cadastro.executeCmd(cmdInsert);
                limpaCampo();
            }
            else
            {
                MessageBox.Show("O CPF " + maskedTextBoxCPF.Text + " já consta no sistema. Verefique o cadastro!", "Registro Exsitente", MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }
            
        }

        private void limpaCampo()
        {
            textBoxName.Clear();
            maskedTextBoxCPF.Clear();
            maskedTextBoxNascimento.Clear();
            maskedTextBoxTelefone.Clear();
            maskedTextBoxCelular.Clear();
            maskedTextBoxCEP.Clear();
            textBoxLogradouro.Clear();
            textBoxNumero.Clear();
            textBoxComplemento.Clear();
            textBoxBairro.Clear();
            textBoxCidade.Clear();
            textBoxEstado.Clear();
        }

        private void textBoxEstado_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, buttonCadastro);
        }
    }
}
