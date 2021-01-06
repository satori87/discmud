namespace WindowsFormsApp1 {
    partial class FrmArea {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.graph = new System.Windows.Forms.Panel();
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.roomPanel = new System.Windows.Forms.Panel();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnNorth = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.over = new CodeProject.GraphicalOverlay(this.components);
            this.roomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.DarkGray;
            this.graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graph.Location = new System.Drawing.Point(20, 20);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(1200, 1200);
            this.graph.TabIndex = 0;
            this.graph.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddRoom.Location = new System.Drawing.Point(1240, 20);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(156, 68);
            this.btnAddRoom.TabIndex = 4;
            this.btnAddRoom.Text = "Add Room";
            this.btnAddRoom.UseVisualStyleBackColor = true;
            this.btnAddRoom.Click += new System.EventHandler(this.button1_Click_1);
            this.btnAddRoom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.btnAddRoom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            // 
            // roomPanel
            // 
            this.roomPanel.BackColor = System.Drawing.Color.Silver;
            this.roomPanel.Controls.Add(this.button1);
            this.roomPanel.Controls.Add(this.label3);
            this.roomPanel.Controls.Add(this.comboBox1);
            this.roomPanel.Controls.Add(this.textBox1);
            this.roomPanel.Controls.Add(this.btnSouth);
            this.roomPanel.Controls.Add(this.btnEast);
            this.roomPanel.Controls.Add(this.btnWest);
            this.roomPanel.Controls.Add(this.btnNorth);
            this.roomPanel.Controls.Add(this.label2);
            this.roomPanel.Controls.Add(this.txtDescription);
            this.roomPanel.Controls.Add(this.label1);
            this.roomPanel.Controls.Add(this.txtName);
            this.roomPanel.Controls.Add(this.lblName);
            this.roomPanel.Controls.Add(this.txtDisplayName);
            this.roomPanel.Location = new System.Drawing.Point(1240, 107);
            this.roomPanel.Name = "roomPanel";
            this.roomPanel.Size = new System.Drawing.Size(1007, 1113);
            this.roomPanel.TabIndex = 5;
            this.roomPanel.Visible = false;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(17, 113);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(970, 26);
            this.txtDisplayName.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(18, 81);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(153, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Room Display Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Room Name (internal e.g. for warping)";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(17, 46);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(970, 26);
            this.txtName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Room Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(17, 188);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(970, 516);
            this.txtDescription.TabIndex = 4;
            // 
            // btnNorth
            // 
            this.btnNorth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNorth.Location = new System.Drawing.Point(149, 732);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(110, 108);
            this.btnNorth.TabIndex = 6;
            this.btnNorth.Text = "Add Room North";
            this.btnNorth.UseVisualStyleBackColor = true;
            // 
            // btnWest
            // 
            this.btnWest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnWest.Location = new System.Drawing.Point(22, 855);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(110, 108);
            this.btnWest.TabIndex = 7;
            this.btnWest.Text = "Add Room West";
            this.btnWest.UseVisualStyleBackColor = true;
            // 
            // btnEast
            // 
            this.btnEast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEast.Location = new System.Drawing.Point(274, 855);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(110, 108);
            this.btnEast.TabIndex = 8;
            this.btnEast.Text = "Add Room East";
            this.btnEast.UseVisualStyleBackColor = true;
            // 
            // btnSouth
            // 
            this.btnSouth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSouth.Location = new System.Drawing.Point(149, 979);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(110, 108);
            this.btnSouth.TabIndex = 9;
            this.btnSouth.Text = "Add Room South";
            this.btnSouth.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(344, 153);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(327, 26);
            this.textBox1.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(677, 151);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(214, 28);
            this.comboBox1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Generator text";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(897, 151);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 31);
            this.button1.TabIndex = 13;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(1411, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 68);
            this.button2.TabIndex = 6;
            this.button2.Text = "Delete Room";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FrmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2268, 1235);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.roomPanel);
            this.Controls.Add(this.btnAddRoom);
            this.Controls.Add(this.graph);
            this.Name = "FrmArea";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.roomPanel.ResumeLayout(false);
            this.roomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel graph;
        private CodeProject.GraphicalOverlay over;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.Panel roomPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button button2;
    }
}

