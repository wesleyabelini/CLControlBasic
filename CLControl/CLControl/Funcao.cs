using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CLControl
{
    class Funcao
    {
        public bool regularExpression(string expressao, string indet)
        {
            bool ismath = false;
            Regex nome = new Regex(@"^[A-Z\s]*$");
            Regex cpf = new Regex(@"^([0-9]{3})[. ,]([0-9]{3})[. ,]([0-9]{3})[-]([0-9]{2})$");
            Regex nascimento = new Regex(@"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$");
            Regex telefone = new Regex(@"^[(]\d{2}[)]\d{4}[-]\d{4}$");
            Regex celular = new Regex(@"^\(\d{2}\)\d{5}-\d{4}$");

            Regex cep = new Regex(@"^([0-9]{5})[-]([0-9]{3})$");
            Regex logradouro = new Regex(@"^[A-Z\s]*$");
            Regex numero = new Regex(@"^\d*$");
            Regex complemento = new Regex(@"^[A-Z0-9\s]*$");
            Regex bairro = new Regex(@"^[A-Z\s]*$");
            Regex cidade = new Regex(@"^[A-Z\s]*$");
            Regex estado = new Regex(@"^[A-Z\s]*$");

            Regex login = new Regex(@"^[A-Za-z0-9]");

            if((nome.IsMatch(expressao) && indet=="nome") || 
                (cpf.IsMatch(expressao) && indet == "cpf") || 
                (nascimento.IsMatch(expressao) && indet == "nascimento") || 
                (telefone.IsMatch(expressao) && indet == "telefone") || 
                (celular.IsMatch(expressao) && indet == "celular") || 
                (cep.IsMatch(expressao) && indet == "cep") || 
                (logradouro.IsMatch(expressao) && indet == "logradouro") || 
                (numero.IsMatch(expressao) && indet == "numero") || 
                (complemento.IsMatch(expressao) && indet == "complemento") || 
                (bairro.IsMatch(expressao) && indet == "bairro") || 
                (cidade.IsMatch(expressao) && indet == "cidade") || 
                (estado.IsMatch(expressao) && indet == "estado") ||
                (login.IsMatch(expressao) && indet=="login"))
            {
                ismath = true;
            }

            return ismath;
        }

        public void onEnterPress(KeyEventArgs e, Button butonx)
        {
            if (e.KeyCode == Keys.Enter)
            {
                butonx.PerformClick();
            }
        }
    }
}
