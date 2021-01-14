using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUDEdit {
    static class MUDEdit {

        public static string monsterModel = "";


        public static FormArea formArea;
        public static FormEditor formEditor;
        public static FormMonster formMonster;

        public static Area curArea;
        public static Monster curMonster;

        [STAThread]
        static void Main() {
            World.Start();
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formArea = new FormArea();
            formEditor = new FormEditor();
            formMonster = new FormMonster();
            Application.Run(formEditor);
        }
    }
}
