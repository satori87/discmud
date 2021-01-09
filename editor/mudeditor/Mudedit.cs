using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mudeditor {
    static class Mudedit {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {

            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmArea frm = new FrmArea();
            Application.Run(frm);
        }
    }
}
