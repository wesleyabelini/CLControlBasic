using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLControl
{
    public partial class FAgenda : Form
    {
        Cadastro cadastro = new Cadastro();
        Funcao funcao = new Funcao();
        public FAgenda()
        {
            InitializeComponent();
        }

        private void FAgenda_Load(object sender, EventArgs e)
        {
            listaProfissional();
        }

        private void buttonCliente_Click(object sender, EventArgs e)
        {
            FCliente cliente = new FCliente();
            cliente.Show();
        }

        private void buttonProfissional_Click(object sender, EventArgs e)
        {
            FProfissional profissional = new FProfissional();
            profissional.Show();
        }
        private void buttonBusca_Click(object sender, EventArgs e)
        {

            if(funcao.regularExpression(maskedTextBoxCPF.Text, "cpf")==true)
            {
                string cmdSelect = @"SELECT * FROM CLIENTE WHERE DOCUMENTO ='" + maskedTextBoxCPF.Text + "';";

                if (cadastro.verificaTrue(cmdSelect) == true)
                {
                    cadastro.formularioCliente(cmdSelect, textBoxNome, maskedTextBoxCPF, maskedTextBoxCPF2, maskedTextBoxNascimento, maskedTextBoxTelefone, maskedTextBoxCelular,
                        maskedTextBoxCEP, textBoxLogradouro, textBoxNumero, textBoxComplemento, textBoxBairro, textBoxCidade, textBoxEstado);
                }
                else
                {
                    MessageBox.Show("O Registro " + maskedTextBoxCPF.Text + " não existe. Faça o Registro!", "Registro não encontrado", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    buttonCliente.PerformClick();
                }
            }
            else
            {
                maskedTextBoxCPF.Clear();
                MessageBox.Show("Os caracteres não confirmam.", "Erro Expressão Regular", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonAlterar_Click(object sender, EventArgs e)
        {
            readOnly(false);
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            string cmdUpdate = @"UPDATE CLIENTE SET NOME='" + textBoxNome.Text + "', NASCIMENTO = '" + maskedTextBoxNascimento.Text + "', TELEFONE = '" + 
                maskedTextBoxTelefone.Text + "', CELULAR ='" + maskedTextBoxCelular.Text + "', CEP='" + maskedTextBoxCEP.Text + "', LOGRADOURO ='" + 
                textBoxLogradouro.Text + "', NUMERO =" + textBoxNumero.Text + ", COMPLEMENTO = '" + textBoxComplemento.Text + "', BAIRRO = '" + 
                textBoxBairro.Text + "', CIDADE ='" + textBoxCidade.Text + "', ESTADO ='" + textBoxEstado.Text + "' WHERE DOCUMENTO = '" + 
                maskedTextBoxCPF.Text + "';";

            cadastro.executeCmd(cmdUpdate);
            readOnly(true);
            MessageBox.Show("Atualizado com Sucesso!", "Atualização de dados.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void readOnly(bool valor)
        {
            textBoxNome.ReadOnly = valor;
            maskedTextBoxNascimento.ReadOnly = valor;
            maskedTextBoxTelefone.ReadOnly = valor;
            maskedTextBoxCelular.ReadOnly = valor;
            maskedTextBoxCEP.ReadOnly = valor;
            textBoxLogradouro.ReadOnly = valor;
            textBoxNumero.ReadOnly = valor;
            textBoxComplemento.ReadOnly = valor;
            textBoxBairro.ReadOnly = valor;
            textBoxCidade.ReadOnly = valor;
            textBoxEstado.ReadOnly = valor;
        }

        private void comboBoxProfissional_Click(object sender, EventArgs e)
        {
            listaProfissional();
        }

        private void listaProfissional()
        {
            string cmdSelect = @"SELECT * FROM PROFISSIONAL";
            cadastro.listaCombo(cmdSelect, comboBoxProfissional);
        }

        private void buttonAgendar_Click(object sender, EventArgs e)
        {
            DateTime data = Convert.ToDateTime(monthCalendar1.SelectionStart.ToString());
            string hora = convertHora();

            string dataDia = data.ToString("yyyy-MM-dd") + " " + hora;

            string cmdSelectID = @"SELECT ID_CLIENTE FROM CLIENTE WHERE DOCUMENTO = '" + maskedTextBoxCPF2.Text + "';";
            int idCli = cadastro.returnID(cmdSelectID, "ID_CLIENTE");

            int idProf = Convert.ToInt16(comboBoxProfissional.SelectedValue);

            string cmdInsert = @"INSERT INTO AGENDA VALUES(" + idCli + ", " + idProf + ", '" + dataDia + "');";
            cadastro.executeCmd(cmdInsert);

            limpaCampo();
        }

        private string convertHora()
        {
            string hora = numericUpDownHora.Value.ToString();
            string minuto = numericUpDownMinuto.Value.ToString();

            if(Convert.ToInt16(hora) < 10)
            {
                hora = "0" + hora;
            }

            if (Convert.ToInt16(minuto) < 10)
            {
                minuto = "0" + minuto;
            }

            string tempo = hora + ":" + minuto + ":00";

            return tempo;
        }

        private void limpaCampo()
        {
            textBoxNome.Clear();
            maskedTextBoxCPF.Clear();
            maskedTextBoxCPF2.Clear();
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

            numericUpDownHora.Value = 0;
            numericUpDownMinuto.Value = 0;

            comboBoxProfissional.SelectedIndex = -1;
        }

        private void maskedTextBoxCPF_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, buttonBusca);
        }
    }
}
