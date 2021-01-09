
namespace mudeditor
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
            this.lstArea = new System.Windows.Forms.ListBox();
            this.tabMonster = new System.Windows.Forms.TabPage();
            this.btnEditArea = new System.Windows.Forms.Button();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.btnDeleteArea = new System.Windows.Forms.Button();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.txtFetchArea = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabArea);
            this.tabControl1.Controls.Add(this.tabMonster);
            this.tabControl1.Controls.Add(this.tabItem);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(802, 450);
            this.tabControl1.TabIndex = 1;
            // 
            // tabArea
            // 
            this.tabArea.BackColor = System.Drawing.Color.Transparent;
            this.tabArea.Controls.Add(this.txtFetchArea);
            this.tabArea.Controls.Add(this.btnDeleteArea);
            this.tabArea.Controls.Add(this.btnAddArea);
            this.tabArea.Controls.Add(this.btnEditArea);
            this.tabArea.Controls.Add(this.lstArea);
            this.tabArea.Location = new System.Drawing.Point(4, 25);
            this.tabArea.Name = "tabArea";
            this.tabArea.Padding = new System.Windows.Forms.Padding(3);
            this.tabArea.Size = new System.Drawing.Size(794, 421);
            this.tabArea.TabIndex = 0;
            this.tabArea.Text = "Area";
            // 
            // lstArea
            // 
            this.lstArea.FormattingEnabled = true;
            this.lstArea.ItemHeight = 16;
            this.lstArea.Location = new System.Drawing.Point(8, 11);
            this.lstArea.Name = "lstArea";
            this.lstArea.Size = new System.Drawing.Size(289, 404);
            this.lstArea.TabIndex = 1;
            // 
            // tabMonster
            // 
            this.tabMonster.Location = new System.Drawing.Point(4, 25);
            this.tabMonster.Name = "tabMonster";
            this.tabMonster.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonster.Size = new System.Drawing.Size(794, 421);
            this.tabMonster.TabIndex = 1;
            this.tabMonster.Text = "Monster";
            this.tabMonster.UseVisualStyleBackColor = true;
            // 
            // btnEditArea
            // 
            this.btnEditArea.Location = new System.Drawing.Point(409, 11);
            this.btnEditArea.Name = "btnEditArea";
            this.btnEditArea.Size = new System.Drawing.Size(100, 32);
            this.btnEditArea.TabIndex = 2;
            this.btnEditArea.Text = "Edit Area";
            this.btnEditArea.UseVisualStyleBackColor = true;
            // 
            // btnAddArea
            // 
            this.btnAddArea.Location = new System.Drawing.Point(303, 11);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(100, 32);
            this.btnAddArea.TabIndex = 3;
            this.btnAddArea.Text = "Add Area";
            this.btnAddArea.UseVisualStyleBackColor = true;
            // 
            // btnDeleteArea
            // 
            this.btnDeleteArea.Location = new System.Drawing.Point(303, 381);
            this.btnDeleteArea.Name = "btnDeleteArea";
            this.btnDeleteArea.Size = new System.Drawing.Size(100, 32);
            this.btnDeleteArea.TabIndex = 4;
            this.btnDeleteArea.Text = "Delete";
            this.btnDeleteArea.UseVisualStyleBackColor = true;
            // 
            // tabItem
            // 
            this.tabItem.Location = new System.Drawing.Point(4, 25);
            this.tabItem.Name = "tabItem";
            this.tabItem.Size = new System.Drawing.Size(794, 421);
            this.tabItem.TabIndex = 2;
            this.tabItem.Text = "Item";
            this.tabItem.UseVisualStyleBackColor = true;
            // 
            // txtFetchArea
            // 
            this.txtFetchArea.Location = new System.Drawing.Point(515, 11);
            this.txtFetchArea.Name = "txtFetchArea";
            this.txtFetchArea.Size = new System.Drawing.Size(100, 32);
            this.txtFetchArea.TabIndex = 5;
            this.txtFetchArea.Text = "Refresh";
            this.txtFetchArea.UseVisualStyleBackColor = true;
            this.txtFetchArea.Click += new System.EventHandler(this.txtFetchArea_Click);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormEditor";
            this.Text = "MUDEdit";
            this.Load += new System.EventHandler(this.FormAreaList_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabArea.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabArea;
        private System.Windows.Forms.ListBox lstArea;
        private System.Windows.Forms.TabPage tabMonster;
        private System.Windows.Forms.Button btnDeleteArea;
        private System.Windows.Forms.Button btnAddArea;
        private System.Windows.Forms.Button btnEditArea;
        private System.Windows.Forms.TabPage tabItem;
        private System.Windows.Forms.Button txtFetchArea;
    }
}