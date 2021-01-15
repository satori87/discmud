using Microsoft.VisualBasic;
using MUDEdit;
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
        }

        private void btnFetchArea_Click(object sender, EventArgs e) {
            MUDEdit.fetchArea(lstArea);
        }

        bool addArea(String name) {
            if (!World.area.ContainsKey(name)) {
                Area a = new Area(name);
                World.area[name] = a;
                if (MUDEdit.executeSQL("INSERT INTO area(name, json) VALUES('" + name + "','" + a.GetJSON() + "')")) {
                    MUDEdit.fetchArea(lstArea);
                    MUDEdit.curArea = World.area[name];
                    MUDEdit.formArea = new FormArea();
                    MUDEdit.formArea.Show();
                    this.Hide();
                    return true;
                }
            }
            return false;
        }

        bool addMonster(String name) {
            if (!World.monster.ContainsKey(name)) {
                Monster m = new Monster(name);
                World.monster[name] = m;
                if (MUDEdit.executeSQL("INSERT INTO area(name) VALUES('" + name + "')")) {
                    MUDEdit.fetchMonster(lstMonster);
                    MUDEdit.curMonster = World.monster[name];
                    MUDEdit.formMonster = new FormMonster();
                    MUDEdit.formMonster.Show();
                    this.Hide();
                    return true;
                }
            }
            return false;
        }

        private void btnAddMob_Click(object sender, EventArgs e) {
            if (!addMonster(Interaction.InputBox("Enter monster internal name"))) {
                Interaction.MsgBox("That monster already exists!");
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

        private void lstArea_DoubleClick(object sender, EventArgs e) {
            if (lstArea.Items.Count > 0 && lstArea.SelectedItem != null) {
                editArea(lstArea.SelectedItem.ToString());
            }
        }

        void editArea(String name) {
            MUDEdit.curArea = World.area[name];
            MUDEdit.formArea = new FormArea();
            MUDEdit.formArea.Show();
            this.Hide();
        }

        void editMonster(String name) {
            MUDEdit.curMonster = World.monster[name];
            MUDEdit.formMonster = new FormMonster();
            MUDEdit.formMonster.Show();
            this.Hide();
        }

        private void btnDeleteArea_Click(object sender, EventArgs e) {
            if (lstArea.Items.Count > 0 && lstArea.SelectedItem != null) {
                deleteArea(lstArea.SelectedItem.ToString());
            }
        }

        void deleteArea(String name) {
            if(MUDEdit.executeSQL("DELETE FROM area WHERE name='" + name + "'")) {
                MUDEdit.curArea = null;
                World.area.Remove(name);
                MUDEdit.fetchArea(lstArea);
            }
        }

        void deleteMonster(String name) {
            if (MUDEdit.executeSQL("DELETE FROM monster WHERE name='" + name + "'")) {
                MUDEdit.curMonster = null;
                World.monster.Remove(name);
                MUDEdit.fetchMonster(lstMonster);
            }
        }

        private void tabArea_Enter(object sender, EventArgs e) {
            MUDEdit.fetchArea(lstArea);
        }

        private void tabMonster_Enter(object sender, EventArgs e) {
            MUDEdit.fetchMonster(lstMonster);
        }

        private void btnFetchMob_Click(object sender, EventArgs e) {
            MUDEdit.fetchMonster(lstMonster);
        }

        private void lstMonster_DoubleClick(object sender, EventArgs e) {
            if (lstMonster.Items.Count > 0 && lstMonster.SelectedItem != null) {
                editMonster(lstMonster.SelectedItem.ToString());
            }
        }

        private void btnEditMob_Click(object sender, EventArgs e) {
            if (lstMonster.Items.Count > 0 && lstMonster.SelectedItem != null) {
                editMonster(lstMonster.SelectedItem.ToString());
            }
        }

        private void btnDeleteMob_Click(object sender, EventArgs e) {
            if (lstMonster.Items.Count > 0 && lstMonster.SelectedItem != null) {
                deleteMonster(lstMonster.SelectedItem.ToString());
            }
        }

    }
}
