using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formSkiRentColumbia
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

            StartWin runApp = new StartWin();
            if(runApp.DialogResult != DialogResult.Cancel)
            {
                runApp.ShowDialog();
            }

            if(runApp.DialogResult == DialogResult.Yes)
            {
                while(runApp.IsUserWantSaveAgain == DialogResult.Yes && runApp.DialogResult == DialogResult.Cancel)
                {
                    runApp.Save_data_to_excel();
                }
            }
        }
    }
}
