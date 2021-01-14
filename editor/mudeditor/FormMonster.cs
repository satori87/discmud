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
    }
}
