using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mudeditor
{
    public partial class FormEditor : Form
    {
        public FormEditor()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
        }

        private void FormAreaList_Load(object sender, EventArgs e)
        {

        }

        private void txtFetchArea_Click(object sender, EventArgs e)
        {
            fetchArea();
        }

        void fetchArea()
        {

        }
    }
}
