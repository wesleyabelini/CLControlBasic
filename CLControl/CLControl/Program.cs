using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (FLogin login = new FLogin())
            {
                if(login.ShowDialog()==DialogResult.OK)
                {
                    login.Close();
                    Application.Run(new Form1());
                }
            }
                
        }
    }
}
