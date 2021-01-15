using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
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


        public static MySqlResponse querySQL(string stm) {
            String cs = @"server=127.0.0.1;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            Console.WriteLine(stm);
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            return new MySqlResponse(rdr, con);
        }

        public static Boolean executeSQL(string stm) {
            String cs = @"server=127.0.0.1;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            if (cmd.ExecuteNonQuery() < 1) {
                Interaction.MsgBox("Failed SQL");
                con.Close();
                return false;
            }
            con.Close();
            return true;
        }

    }
}
