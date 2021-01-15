using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUDEdit {
    partial class FormEditor : Form {
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
            if (MUDEdit.executeSQL("DELETE FROM area WHERE name='" + name + "'")) {
                MUDEdit.curArea = null;
                World.area.Remove(name);
                MUDEdit.fetchArea(lstArea);
            }
        }

        private void tabArea_Enter(object sender, EventArgs e) {
            MUDEdit.fetchArea(lstArea);
        }

    }
}
