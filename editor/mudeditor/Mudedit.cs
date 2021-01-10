using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUDEdit {
    static class MUDEdit {

        public static FormArea formArea;
        public static FormEditor formEditor;

        public static Area curArea;

        [STAThread]
        static void Main() {
            World.Start();
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formArea = new FormArea();
            formEditor = new FormEditor();
            Application.Run(formEditor);
        }
    }
}
