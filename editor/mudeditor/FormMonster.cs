using Microsoft.VisualBasic;
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
    public partial class FormMonster : Form {
        public FormMonster() {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
        }

        private void FormMonster_FormClosing(object sender, FormClosingEventArgs e) {
            MUDEdit.formEditor.Show();
        }

        private void FormMonster_Load(object sender, EventArgs e) {
            btnSave.Parent = this;
            btnCancel.Parent = this;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Hide();
            MUDEdit.formEditor.Show();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //fix hp dice, TODO move to function somewhere nice and static
            String s = txtHP.Text;
            int numDice = 1;
            int numSides = 1;
            int bonus = 0;
            bool good = false;
            String sbonus = "";
            try {
                int dpos = s.IndexOf("d");
                String num = s.Substring(0, dpos);
                String rest = s.Substring(dpos + 1);
                numDice = int.Parse(num);
                int ppos = rest.IndexOf("+");
                String sides = rest;
                if (ppos > 0) {
                    sides = rest.Substring(0, ppos);
                    sbonus = rest.Substring(ppos + 1);
                    bonus = int.Parse(sbonus);
                }
                numSides = int.Parse(sides);
                good = true;
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            if(good) {
                
                //Area a = new Area(txtName.Text);
                //a.displayName = txtDisplayName.Text;
               // a.rooms = rooms;
               // String cs = @"server=127.0.0.1;port=3306;userid=bear;password=%Pb?fYW@ydP9RLqeTnfSW-u!23c$f=%#;database=mud";
                //var con = new MySqlConnection(cs);
                //con.Open();
               // String stm = "UPDATE area SET json='" + a.GetJSON() + "' WHERE name='" + a.name + "'";
                //var cmd = new MySql.Data.MySqlClient.MySqlCommand(stm, con);
                //if (cmd.ExecuteNonQuery() < 1) {
                //    Interaction.MsgBox("Save area failed SQL");
                //} else {
                //    this.Hide();
                   // MUDEdit.formEditor.Show();
                  //  MUDEdit.formEditor.fetchArea();
                //}
                //con.Close();
                
            }

        }

        private void txtHP_Leave(object sender, EventArgs e) {
                     
        }

        void fix() {
            foreach (Control c in tabMain.Controls) {
                if (c is TextBox && c.AccessibleName != null && c.AccessibleName.Equals("int")) {
                    TextBox t = (TextBox)c;
                    try {
                        int n = int.Parse(t.Text);
                    } catch (Exception ex) {
                        Console.WriteLine(ex);
                        t.Text = "0";
                    }
                }
            }
        }

        private void txtLeave(object sender, EventArgs e) {
            fix();
        }

        private void FormMonster_Shown(object sender, EventArgs e) {
            Monster m =  MUDEdit.curMonster;
            txtName.Text = (String)m.fields["name"];
            txtDisplayName.Text = (String)m.fields["display_name"];
            txtHP.Text = (String)m.fields["hp_dice"];
            txtStr.Text = "" + (int)m.fields["strength"];
            txtDex.Text = "" + (int)m.fields["dexterity"];
            txtCon.Text = "" + (int)m.fields["constitution"];
            txtInt.Text = "" + (int)m.fields["intelligence"];
            txtWis.Text = "" + (int)m.fields["wisdom"];
            txtLuck.Text = "" + (int)m.fields["luck"];
            txtPhysEva.Text = "" + (int)m.fields["physeva"];
            txtPhysDef.Text = "" + (int)m.fields["physdef"];
            txtMagEva.Text = "" + (int)m.fields["mageva"];
            txtMagDef.Text = "" + (int)m.fields["magdef"];
            txtSpeed.Text = "" + (int)m.fields["speed"];

            fix();
        }

        private void tabLoot_Enter(object sender, EventArgs e) {

        }

        private void textBox7_TextChanged(object sender, EventArgs e) {

        }

        private void textBox6_TextChanged(object sender, EventArgs e) {

        }

        private void textBox5_TextChanged(object sender, EventArgs e) {

        }
    }
}
