using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mudeditor {
    public partial class FormEditor : Form {
        public FormEditor() {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
        }

        private void FormAreaList_Load(object sender, EventArgs e) {

        }

        private void txtFetchArea_Click(object sender, EventArgs e) {
            fetchArea();
        }

        void fetchArea() {
            String cs = @"server=18.223.190.165;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            var stm = "SELECT name,json FROM area";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) {
                Console.WriteLine("aa");
                Console.WriteLine(rdr.GetString(0) + " " + rdr.GetString(1));
            }
            //Console.WriteLine(result.ToString());
            //,;

            lstArea.Items.Clear();
        }
    }
}
