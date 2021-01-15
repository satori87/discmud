using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MUDEdit {
    partial class FormEditor : Form {

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

        void deleteMonster(String name) {
            if (MUDEdit.executeSQL("DELETE FROM monster WHERE name='" + name + "'")) {
                MUDEdit.curMonster = null;
                World.monster.Remove(name);
                MUDEdit.fetchMonster(lstMonster);
            }
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

        void editMonster(String name) {
            MUDEdit.curMonster = World.monster[name];
            MUDEdit.formMonster = new FormMonster();
            MUDEdit.formMonster.Show();
            this.Hide();
        }

    }
}
