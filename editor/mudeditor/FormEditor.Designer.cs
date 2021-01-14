
namespace MUDEdit
{
    partial class FormEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabArea = new System.Windows.Forms.TabPage();
            this.txtFetchArea = new System.Windows.Forms.Button();
            this.lstArea = new System.Windows.Forms.ListBox();
            this.btnEditArea = new System.Windows.Forms.Button();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.btnDeleteArea = new System.Windows.Forms.Button();
            this.tabMonster = new System.Windows.Forms.TabPage();
            this.btnFetchMob = new System.Windows.Forms.Button();
            this.lstMonster = new System.Windows.Forms.ListBox();
            this.btnDeleteMob = new System.Windows.Forms.Button();
            this.btnEditMob = new System.Windows.Forms.Button();
            this.btnAddMob = new System.Windows.Forms.Button();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabArea.SuspendLayout();
            this.tabMonster.SuspendLayout();
            this.tabItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabArea);
            this.tabControl1.Controls.Add(this.tabMonster);
            this.tabControl1.Controls.Add(this.tabItem);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(971, 726);
            this.tabControl1.TabIndex = 1;
            // 
            // tabArea
            // 
            this.tabArea.BackColor = System.Drawing.Color.Transparent;
            this.tabArea.Controls.Add(this.txtFetchArea);
            this.tabArea.Controls.Add(this.lstArea);
            this.tabArea.Controls.Add(this.btnEditArea);
            this.tabArea.Controls.Add(this.btnAddArea);
            this.tabArea.Controls.Add(this.btnDeleteArea);
            this.tabArea.Location = new System.Drawing.Point(4, 38);
            this.tabArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabArea.Name = "tabArea";
            this.tabArea.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabArea.Size = new System.Drawing.Size(963, 684);
            this.tabArea.TabIndex = 0;
            this.tabArea.Text = "Area";
            this.tabArea.Enter += new System.EventHandler(this.tabArea_Enter);
            // 
            // txtFetchArea
            // 
            this.txtFetchArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFetchArea.Location = new System.Drawing.Point(652, 9);
            this.txtFetchArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtFetchArea.Name = "txtFetchArea";
            this.txtFetchArea.Size = new System.Drawing.Size(150, 49);
            this.txtFetchArea.TabIndex = 25;
            this.txtFetchArea.Text = "Refresh";
            this.txtFetchArea.UseVisualStyleBackColor = true;
            // 
            // lstArea
            // 
            this.lstArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstArea.FormattingEnabled = true;
            this.lstArea.ItemHeight = 29;
            this.lstArea.Location = new System.Drawing.Point(7, 9);
            this.lstArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lstArea.Name = "lstArea";
            this.lstArea.Size = new System.Drawing.Size(325, 526);
            this.lstArea.TabIndex = 21;
            // 
            // btnEditArea
            // 
            this.btnEditArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditArea.Location = new System.Drawing.Point(496, 9);
            this.btnEditArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEditArea.Name = "btnEditArea";
            this.btnEditArea.Size = new System.Drawing.Size(150, 49);
            this.btnEditArea.TabIndex = 22;
            this.btnEditArea.Text = "Edit Area";
            this.btnEditArea.UseVisualStyleBackColor = true;
            // 
            // btnAddArea
            // 
            this.btnAddArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddArea.Location = new System.Drawing.Point(340, 9);
            this.btnAddArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(150, 49);
            this.btnAddArea.TabIndex = 23;
            this.btnAddArea.Text = "Add Area";
            this.btnAddArea.UseVisualStyleBackColor = true;
            // 
            // btnDeleteArea
            // 
            this.btnDeleteArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteArea.Location = new System.Drawing.Point(338, 486);
            this.btnDeleteArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDeleteArea.Name = "btnDeleteArea";
            this.btnDeleteArea.Size = new System.Drawing.Size(150, 49);
            this.btnDeleteArea.TabIndex = 24;
            this.btnDeleteArea.Text = "Delete";
            this.btnDeleteArea.UseVisualStyleBackColor = true;
            // 
            // tabMonster
            // 
            this.tabMonster.Controls.Add(this.btnFetchMob);
            this.tabMonster.Controls.Add(this.lstMonster);
            this.tabMonster.Controls.Add(this.btnDeleteMob);
            this.tabMonster.Controls.Add(this.btnEditMob);
            this.tabMonster.Controls.Add(this.btnAddMob);
            this.tabMonster.Location = new System.Drawing.Point(4, 38);
            this.tabMonster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabMonster.Name = "tabMonster";
            this.tabMonster.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabMonster.Size = new System.Drawing.Size(963, 684);
            this.tabMonster.TabIndex = 1;
            this.tabMonster.Text = "Monster";
            this.tabMonster.UseVisualStyleBackColor = true;
            this.tabMonster.Enter += new System.EventHandler(this.tabMonster_Enter);
            // 
            // btnFetchMob
            // 
            this.btnFetchMob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchMob.Location = new System.Drawing.Point(652, 9);
            this.btnFetchMob.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnFetchMob.Name = "btnFetchMob";
            this.btnFetchMob.Size = new System.Drawing.Size(150, 49);
            this.btnFetchMob.TabIndex = 30;
            this.btnFetchMob.Text = "Refresh";
            this.btnFetchMob.UseVisualStyleBackColor = true;
            this.btnFetchMob.Click += new System.EventHandler(this.btnFetchMob_Click);
            // 
            // lstMonster
            // 
            this.lstMonster.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstMonster.FormattingEnabled = true;
            this.lstMonster.ItemHeight = 29;
            this.lstMonster.Location = new System.Drawing.Point(7, 9);
            this.lstMonster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lstMonster.Name = "lstMonster";
            this.lstMonster.Size = new System.Drawing.Size(325, 526);
            this.lstMonster.TabIndex = 26;
            // 
            // btnDeleteMob
            // 
            this.btnDeleteMob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteMob.Location = new System.Drawing.Point(338, 486);
            this.btnDeleteMob.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDeleteMob.Name = "btnDeleteMob";
            this.btnDeleteMob.Size = new System.Drawing.Size(150, 49);
            this.btnDeleteMob.TabIndex = 29;
            this.btnDeleteMob.Text = "Delete";
            this.btnDeleteMob.UseVisualStyleBackColor = true;
            // 
            // btnEditMob
            // 
            this.btnEditMob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMob.Location = new System.Drawing.Point(496, 9);
            this.btnEditMob.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEditMob.Name = "btnEditMob";
            this.btnEditMob.Size = new System.Drawing.Size(150, 49);
            this.btnEditMob.TabIndex = 27;
            this.btnEditMob.Text = "Edit Mob";
            this.btnEditMob.UseVisualStyleBackColor = true;
            // 
            // btnAddMob
            // 
            this.btnAddMob.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMob.Location = new System.Drawing.Point(340, 9);
            this.btnAddMob.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnAddMob.Name = "btnAddMob";
            this.btnAddMob.Size = new System.Drawing.Size(150, 49);
            this.btnAddMob.TabIndex = 28;
            this.btnAddMob.Text = "Add Mob";
            this.btnAddMob.UseVisualStyleBackColor = true;
            this.btnAddMob.Click += new System.EventHandler(this.btnAddMob_Click);
            // 
            // tabItem
            // 
            this.tabItem.Controls.Add(this.button1);
            this.tabItem.Controls.Add(this.listBox1);
            this.tabItem.Controls.Add(this.button2);
            this.tabItem.Controls.Add(this.button3);
            this.tabItem.Controls.Add(this.button4);
            this.tabItem.Location = new System.Drawing.Point(4, 38);
            this.tabItem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabItem.Name = "tabItem";
            this.tabItem.Size = new System.Drawing.Size(963, 684);
            this.tabItem.TabIndex = 2;
            this.tabItem.Text = "Item";
            this.tabItem.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(652, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 49);
            this.button1.TabIndex = 25;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 29;
            this.listBox1.Location = new System.Drawing.Point(7, 9);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(325, 526);
            this.listBox1.TabIndex = 21;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(496, 9);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 49);
            this.button2.TabIndex = 22;
            this.button2.Text = "Edit Area";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(340, 9);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 49);
            this.button3.TabIndex = 23;
            this.button3.Text = "Add Area";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(338, 486);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 49);
            this.button4.TabIndex = 24;
            this.button4.Text = "Delete";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 583);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "FormEditor";
            this.Text = "MUDEdit";
            this.tabControl1.ResumeLayout(false);
            this.tabArea.ResumeLayout(false);
            this.tabMonster.ResumeLayout(false);
            this.tabItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabArea;
        private System.Windows.Forms.TabPage tabMonster;
        private System.Windows.Forms.TabPage tabItem;
        private System.Windows.Forms.Button btnFetchMob;
        private System.Windows.Forms.ListBox lstMonster;
        private System.Windows.Forms.Button btnDeleteMob;
        private System.Windows.Forms.Button btnEditMob;
        private System.Windows.Forms.Button btnAddMob;
        private System.Windows.Forms.Button txtFetchArea;
        private System.Windows.Forms.ListBox lstArea;
        private System.Windows.Forms.Button btnEditArea;
        private System.Windows.Forms.Button btnAddArea;
        private System.Windows.Forms.Button btnDeleteArea;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}