using Microsoft.VisualBasic;
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

namespace MUDEdit {
    public partial class FormEditor : Form {
        public FormEditor() {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
            fetchArea();
        }

        private void FormAreaList_Load(object sender, EventArgs e) {

        }

        private void txtFetchArea_Click(object sender, EventArgs e) {
            fetchArea();
        }

        public void fetchArea() {
            String cs = @"server=18.223.190.165;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            var stm = "SELECT name,json FROM area";
            World.area = new Dictionary<string, Area>();
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            MySql.Data.MySqlClient.MySqlDataReader rdr = cmd.ExecuteReader();
            lstArea.Items.Clear();
            Area a = new Area("a");
            while (rdr.Read()) {
                String name = rdr.GetString(0);
                World.area[name] = (Area)Util.JSONToObject(rdr.GetString(1), a.GetType());
                lstArea.Items.Add(rdr.GetString(0));
            }
            con.Close();
        }

        private void tabArea_Click(object sender, EventArgs e) {

        }

        bool addArea(String name) {
            if (!World.area.ContainsKey(name)) {
                Area a = new Area(name);
                World.area[name] = a;
                String cs = @"server=18.223.190.165;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
                var con = new MySqlConnection(cs);
                con.Open();
                String stm = "INSERT INTO area(name, json) VALUES('" + name + "','" + a.GetJSON() + "')";
                var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
                if (cmd.ExecuteNonQuery() < 1) {
                    Interaction.MsgBox("New area failed SQL");
                }
                con.Close();
                fetchArea();
                MUDEdit.curArea = World.area[name];
                MUDEdit.formArea.Show();
                this.Hide();
                return true;
            } else {
                return false;
            }
        }

        private void btnAddArea_Click(object sender, EventArgs e) {
            if (!addArea(Interaction.InputBox("Enter area internal name"))) {
                Interaction.MsgBox("That area already exists!");
            }
        }

        private void btnEditArea_Click(object sender, EventArgs e) {
            if (lstArea.Items.Count > 0 && lstArea.SelectedItem != null) {
                editArea(lstArea.SelectedItem.ToString());
            }
        }

        private void lstArea_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void lstArea_DoubleClick(object sender, EventArgs e) {
            editArea(lstArea.SelectedItem.ToString());
        }

        void editArea(String name) {
            MUDEdit.curArea = World.area[name];
            MUDEdit.formArea.Show();
            this.Hide();
        }

        private void btnDeleteArea_Click(object sender, EventArgs e) {
            if (lstArea.Items.Count > 0 && lstArea.SelectedItem != null) {
                deleteArea(lstArea.SelectedItem.ToString());
            }
        }

        void deleteArea(String name) {
            String cs = @"server=18.223.190.165;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
            var con = new MySqlConnection(cs);
            con.Open();
            String stm = "DELETE FROM area WHERE name='" + name + "'";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
            if (cmd.ExecuteNonQuery() < 1) {
                Interaction.MsgBox("Delete area failed SQL");
            }
            con.Close();
            MUDEdit.curArea = null;
            World.area.Remove(name);
            fetchArea();

        }
    }
}
