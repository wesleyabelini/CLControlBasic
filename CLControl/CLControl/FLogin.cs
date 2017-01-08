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
    public partial class FLogin : Form
    {
        Funcao funcao = new Funcao();
        Cadastro cadastro = new Cadastro();
        public FLogin()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, button1);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            funcao.onEnterPress(e, button1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmdSelect = @"SELECT * FROM SYSLOGIN WHERE USUARIO ='" + textBox1.Text + "' AND SENHA = '" + textBox2.Text + "';";

            if(cadastro.verificaTrue(cmdSelect)==true)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
            }
        }
    }
}
