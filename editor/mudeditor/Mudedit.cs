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

        public static void fetchArea(ListBox lstArea) {
            World.area = new Dictionary<string, Area>();
            lstArea.Items.Clear();
            Area a = new Area("a");
            var response = MUDEdit.querySQL("SELECT name,json FROM area");
            var rdr = response.rdr;
            while (rdr.Read()) {
                String name = rdr.GetString(0);
                Console.WriteLine(name);
                World.area[name] = (Area)Util.JSONToObject(rdr.GetString(1), a.GetType());
                lstArea.Items.Add(rdr.GetString(0));
            }
            response.con.Close();
        }

        public static void fetchMonster(ListBox lstMonster) {
            World.monster = new Dictionary<string, Monster>();
            MUDEdit.monsterModel = "";
            var response = MUDEdit.querySQL("SHOW COLUMNS FROM monster");
            var rdr = response.rdr;
            while (rdr.Read()) {
                MUDEdit.monsterModel += rdr.GetString(0) + ",";
            }
            response.con.Close();
            MUDEdit.monsterModel = MUDEdit.monsterModel.Substring(0, MUDEdit.monsterModel.Length - 1);
            Monster m;
            response = MUDEdit.querySQL("SELECT " + MUDEdit.monsterModel + " FROM monster");
            rdr = response.rdr;
            lstMonster.Items.Clear();
            while (rdr.Read()) {
                String name = rdr.GetString(0);
                m = new Monster(name);
                World.monster[name] = m;
                lstMonster.Items.Add(rdr.GetString(0));
                string[] split = MUDEdit.monsterModel.Split(',');
                int c = 0;
                foreach (string s in split) {
                    m.fields[s] = rdr.GetValue(c);
                    c++;
                }
            }
            response.con.Close();
        }

    }
}
