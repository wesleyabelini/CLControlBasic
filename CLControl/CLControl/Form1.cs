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
    public partial class Form1 : Form
    {
        Cadastro cadastro = new Cadastro();
        Funcao funcao = new Funcao();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCliente_Click(object sender, EventArgs e)
        {
            FCliente cliente = new FCliente();
            cliente.Show();
        }

        private void buttonAgenda_Click(object sender, EventArgs e)
        {
            FAgenda agenda = new FAgenda();
            agenda.Show();
        }

        private void buttonProfissional_Click(object sender, EventArgs e)
        {
            FProfissional profissional = new FProfissional();
            profissional.Show();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FCliente cliente = new FCliente();
            cliente.Show();
        }

        private void agendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAgenda agenda = new FAgenda();
            agenda.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            limpaCampo();
            DateTime data = Convert.ToDateTime(monthCalendar1.SelectionStart.ToString());

            string cmdSelect = @"SELECT A.ID_AGENDA AS 'ID', CLI.NOME AS 'NOME' FROM AGENDA AS A INNER JOIN CLIENTE AS CLI ON CLI.ID_CLIENTE = A.IDCLIENTE WHERE DATAS BETWEEN '" + 
                data.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + data.ToString("yyyy-MM-dd") + " 23:59:59';";
            cadastro.preencheDatagrid(cmdSelect, dataGridView1);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            limpaCampo();

            if (dataGridView1.Rows.Count >1)
            {
                string idAgenda = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int idCliente = cadastro.returnID(@"SELECT IDCLIENTE FROM AGENDA WHERE ID_AGENDA=" + idAgenda, "IDCLIENTE");


                string cmdSelectID = @"SELECT A.IDPROFISSIONAL, P.PROFISSIONAL FROM AGENDA AS A INNER JOIN PROFISSIONAL AS P ON P.ID_PROF = A.IDPROFISSIONAL 
WHERE A.ID_AGENDA =" + idAgenda;
                textBoxProfissional.Text = cadastro.returnText(cmdSelectID, "PROFISSIONAL");


                string cmdSelect = @"SELECT * FROM CLIENTE WHERE ID_CLIENTE =" + idCliente;
                cadastro.formularioCliente(cmdSelect, textBoxNome, maskedTextBoxCPF2, maskedTextBoxCPF2, maskedTextBoxNascimento, maskedTextBoxTelefone,
                    maskedTextBoxCelular, maskedTextBoxCEP, textBoxLogradouro, textBoxNumero, textBoxComplemento, textBoxBairro, textBoxCidade, textBoxEstado);
            }
        }

        private void buttonBusca_Click(object sender, EventArgs e)
        {
            if(funcao.regularExpression(maskedTextBoxCPF.Text, "cpf"))
            {
                string cmdSelect = @"SELECT A.ID_AGENDA AS 'ID', A.DATAS AS 'DATA', CLI.NOME AS 'NOME' FROM AGENDA AS A 
INNER JOIN CLIENTE AS CLI ON CLI.ID_CLIENTE = A.IDCLIENTE WHERE DOCUMENTO='" + maskedTextBoxCPF.Text + "';";

                cadastro.preencheDatagrid(cmdSelect, dataGridView1);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                maskedTextBoxCPF.Clear();
                MessageBox.Show("Os caracteres não confirmam.", "Erro Expressão Regular", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void limpaCampo()
        {
            textBoxNome.Clear();
            maskedTextBoxCPF2.Clear();
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

            textBoxProfissional.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime data = monthCalendar1.SelectionStart;
            monthCalendar1_DateSelected(monthCalendar1, new DateRangeEventArgs(data, data));
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Deseja deletar o registro?", "Delete Registro", MessageBoxButtons.OK, MessageBoxIcon.Question);

            if(dialog == DialogResult.OK)
            {
                string cmdDelete = @"DELETE FROM AGENDA WHERE ID_AGENDA =" + dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cadastro.executeCmd(cmdDelete);

                DateTime data = Convert.ToDateTime(DateTime.Now);
                monthCalendar1_DateSelected(monthCalendar1, new DateRangeEventArgs(data, data));
                dataGridView1_SelectionChanged(dataGridView1, new EventArgs());
            }
        }

        private void profissionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FProfissional profissional = new FProfissional();
            profissional.Show();
        }

        private void maskedTextBoxCPF_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, buttonBusca);
        }

        private void sobreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox box = new AboutBox();
            box.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime data = DateTime.Now;
            toolStripLabel1.Text = data.ToString();
        }
    }
}
