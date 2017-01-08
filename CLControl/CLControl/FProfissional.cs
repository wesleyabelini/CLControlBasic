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
    public partial class FProfissional : Form
    {
        Cadastro cadastro = new Cadastro();
        Funcao funcao = new Funcao();
        public FProfissional()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdInsert = @"INSERT INTO PROFISSIONAL VALUES ('" + textBox1.Text + "');";
            cadastro.executeCmd(cmdInsert);

            textBox1.Clear();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, button1);
        }
    }
}
