
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
            this.btnDeleteArea = new System.Windows.Forms.Button();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.btnEditArea = new System.Windows.Forms.Button();
            this.lstArea = new System.Windows.Forms.ListBox();
            this.tabMonster = new System.Windows.Forms.TabPage();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabArea.SuspendLayout();
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
            this.tabArea.Controls.Add(this.btnDeleteArea);
            this.tabArea.Controls.Add(this.btnAddArea);
            this.tabArea.Controls.Add(this.btnEditArea);
            this.tabArea.Controls.Add(this.lstArea);
            this.tabArea.Location = new System.Drawing.Point(4, 38);
            this.tabArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabArea.Name = "tabArea";
            this.tabArea.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabArea.Size = new System.Drawing.Size(963, 684);
            this.tabArea.TabIndex = 0;
            this.tabArea.Text = "Area";
            this.tabArea.Click += new System.EventHandler(this.tabArea_Click);
            // 
            // txtFetchArea
            // 
            this.txtFetchArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFetchArea.Location = new System.Drawing.Point(654, 14);
            this.txtFetchArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtFetchArea.Name = "txtFetchArea";
            this.txtFetchArea.Size = new System.Drawing.Size(150, 49);
            this.txtFetchArea.TabIndex = 5;
            this.txtFetchArea.Text = "Refresh";
            this.txtFetchArea.UseVisualStyleBackColor = true;
            this.txtFetchArea.Click += new System.EventHandler(this.txtFetchArea_Click);
            // 
            // btnDeleteArea
            // 
            this.btnDeleteArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteArea.Location = new System.Drawing.Point(340, 491);
            this.btnDeleteArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnDeleteArea.Name = "btnDeleteArea";
            this.btnDeleteArea.Size = new System.Drawing.Size(150, 49);
            this.btnDeleteArea.TabIndex = 4;
            this.btnDeleteArea.Text = "Delete";
            this.btnDeleteArea.UseVisualStyleBackColor = true;
            // 
            // btnAddArea
            // 
            this.btnAddArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddArea.Location = new System.Drawing.Point(342, 14);
            this.btnAddArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(150, 49);
            this.btnAddArea.TabIndex = 3;
            this.btnAddArea.Text = "Add Area";
            this.btnAddArea.UseVisualStyleBackColor = true;
            this.btnAddArea.Click += new System.EventHandler(this.btnAddArea_Click);
            // 
            // btnEditArea
            // 
            this.btnEditArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditArea.Location = new System.Drawing.Point(498, 14);
            this.btnEditArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnEditArea.Name = "btnEditArea";
            this.btnEditArea.Size = new System.Drawing.Size(150, 49);
            this.btnEditArea.TabIndex = 2;
            this.btnEditArea.Text = "Edit Area";
            this.btnEditArea.UseVisualStyleBackColor = true;
            // 
            // lstArea
            // 
            this.lstArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstArea.FormattingEnabled = true;
            this.lstArea.ItemHeight = 29;
            this.lstArea.Location = new System.Drawing.Point(9, 14);
            this.lstArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lstArea.Name = "lstArea";
            this.lstArea.Size = new System.Drawing.Size(325, 526);
            this.lstArea.TabIndex = 1;
            // 
            // tabMonster
            // 
            this.tabMonster.Location = new System.Drawing.Point(4, 38);
            this.tabMonster.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabMonster.Name = "tabMonster";
            this.tabMonster.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabMonster.Size = new System.Drawing.Size(920, 550);
            this.tabMonster.TabIndex = 1;
            this.tabMonster.Text = "Monster";
            this.tabMonster.UseVisualStyleBackColor = true;
            // 
            // tabItem
            // 
            this.tabItem.Location = new System.Drawing.Point(4, 38);
            this.tabItem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tabItem.Name = "tabItem";
            this.tabItem.Size = new System.Drawing.Size(920, 550);
            this.tabItem.TabIndex = 2;
            this.tabItem.Text = "Item";
            this.tabItem.UseVisualStyleBackColor = true;
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 589);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
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