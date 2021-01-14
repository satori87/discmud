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

        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Hide();
            MUDEdit.formEditor.Show();
        }

        private void btnSave_Click(object sender, EventArgs e) {

        }

        private void txtHP_Leave(object sender, EventArgs e) {
            fix();            
        }

        void fix() {
            //fix hp dice, TODO move to function somewhere nice and static
            String s = txtHP.Text;
            int numDice = 1;
            int numSides = 1;
            int bonus = 0;
            try {
                int dpos = s.IndexOf("d");
                String num = s.Substring(0, dpos);
                String rest = s.Substring(dpos);
                numDice = int.Parse(num);
                int ppos = rest.IndexOf("+");
                String sides = rest;
                if (ppos > 0) {
                    sides = rest.Substring(0, ppos);
                    String sbonus = rest.Substring(ppos);
                    bonus = int.Parse(sbonus);
                }
                numSides = int.Parse(sides);
                String dice = numDice + "d" + numSides + "+" + bonus;
                txtHP.Text = dice;
            } catch (Exception ex) {
                Console.WriteLine(ex);
                txtHP.Text = "";
            }

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
            txtStr.Text = (String)m.fields["strength"];
            txtDex.Text = (String)m.fields["dexterity"];
            txtCon.Text = (String)m.fields["constitution"];
            txtInt.Text = (String)m.fields["intelligence"];
            txtWis.Text = (String)m.fields["wisdom"];
            txtLuck.Text = (String)m.fields["luck"];
            txtPhysEva.Text = (String)m.fields["physeva"];
            txtPhysDef.Text = (String)m.fields["physdef"];
            txtMagEva.Text = (String)m.fields["mageva"];
            txtMagDef.Text = (String)m.fields["magdef"];
            txtSpeed.Text = (String)m.fields["speed"];

            fix();
        }
    }
}
