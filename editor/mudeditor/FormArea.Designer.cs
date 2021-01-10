namespace MUDEdit {
    partial class FormArea {
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
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.roomPanel = new System.Windows.Forms.Panel();
            this.btnLinkNearby = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClearExit = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSouth = new System.Windows.Forms.Button();
            this.btnEast = new System.Windows.Forms.Button();
            this.btnWest = new System.Windows.Forms.Button();
            this.btnNorth = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.graph = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.txtAreaDisplayName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLinkAll = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.over = new CodeProject.GraphicalOverlay(this.components);
            this.roomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRoom.Location = new System.Drawing.Point(974, 881);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(156, 68);
            this.btnAddRoom.TabIndex = 1;
            this.btnAddRoom.Text = "Add Room";
            this.btnAddRoom.UseVisualStyleBackColor = true;
            this.btnAddRoom.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // roomPanel
            // 
            this.roomPanel.BackColor = System.Drawing.Color.Silver;
            this.roomPanel.Controls.Add(this.btnLinkNearby);
            this.roomPanel.Controls.Add(this.btnDelete);
            this.roomPanel.Controls.Add(this.btnClearExit);
            this.roomPanel.Controls.Add(this.btnDown);
            this.roomPanel.Controls.Add(this.btnUp);
            this.roomPanel.Controls.Add(this.btnUpdate);
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
            this.roomPanel.Location = new System.Drawing.Point(974, 138);
            this.roomPanel.Name = "roomPanel";
            this.roomPanel.Size = new System.Drawing.Size(686, 725);
            this.roomPanel.TabIndex = 5;
            this.roomPanel.Visible = false;
            this.roomPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.roomPanel_Paint);
            // 
            // btnLinkNearby
            // 
            this.btnLinkNearby.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLinkNearby.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkNearby.Location = new System.Drawing.Point(123, 610);
            this.btnLinkNearby.Name = "btnLinkNearby";
            this.btnLinkNearby.Size = new System.Drawing.Size(100, 100);
            this.btnLinkNearby.TabIndex = 19;
            this.btnLinkNearby.Text = "Link Nearby";
            this.btnLinkNearby.UseVisualStyleBackColor = true;
            this.btnLinkNearby.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(573, 610);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 100);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete Room";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClearExit
            // 
            this.btnClearExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClearExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearExit.Location = new System.Drawing.Point(17, 610);
            this.btnClearExit.Name = "btnClearExit";
            this.btnClearExit.Size = new System.Drawing.Size(100, 100);
            this.btnClearExit.TabIndex = 16;
            this.btnClearExit.Text = "Clear Exits";
            this.btnClearExit.UseVisualStyleBackColor = true;
            this.btnClearExit.Click += new System.EventHandler(this.btnClearExit_Click);
            // 
            // btnDown
            // 
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(269, 906);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(100, 100);
            this.btnDown.TabIndex = 15;
            this.btnDown.Text = "Add Room Down";
            this.btnDown.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(398, 906);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(100, 100);
            this.btnUp.TabIndex = 14;
            this.btnUp.Text = "Add Room Up";
            this.btnUp.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(229, 610);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(100, 100);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update Room";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.txtUpdate_Click);
            // 
            // btnSouth
            // 
            this.btnSouth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSouth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSouth.Location = new System.Drawing.Point(144, 906);
            this.btnSouth.Name = "btnSouth";
            this.btnSouth.Size = new System.Drawing.Size(100, 100);
            this.btnSouth.TabIndex = 10;
            this.btnSouth.Text = "Add Room South";
            this.btnSouth.UseVisualStyleBackColor = true;
            // 
            // btnEast
            // 
            this.btnEast.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEast.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEast.Location = new System.Drawing.Point(637, 906);
            this.btnEast.Name = "btnEast";
            this.btnEast.Size = new System.Drawing.Size(100, 100);
            this.btnEast.TabIndex = 12;
            this.btnEast.Text = "Add Room East";
            this.btnEast.UseVisualStyleBackColor = true;
            // 
            // btnWest
            // 
            this.btnWest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnWest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWest.Location = new System.Drawing.Point(17, 906);
            this.btnWest.Name = "btnWest";
            this.btnWest.Size = new System.Drawing.Size(100, 100);
            this.btnWest.TabIndex = 11;
            this.btnWest.Text = "Add Room West";
            this.btnWest.UseVisualStyleBackColor = true;
            // 
            // btnNorth
            // 
            this.btnNorth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnNorth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNorth.Location = new System.Drawing.Point(520, 897);
            this.btnNorth.Name = "btnNorth";
            this.btnNorth.Size = new System.Drawing.Size(100, 100);
            this.btnNorth.TabIndex = 9;
            this.btnNorth.Text = "Add Room North";
            this.btnNorth.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Room Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(17, 185);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(656, 353);
            this.txtDescription.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(339, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Room Name (internal e.g. for warping)";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(17, 43);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(656, 26);
            this.txtName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(12, 76);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(189, 25);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Room Display Name";
            this.lblName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.lblName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(17, 110);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(656, 26);
            this.txtDisplayName.TabIndex = 3;
            // 
            // graph
            // 
            this.graph.BackColor = System.Drawing.Color.DarkGray;
            this.graph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graph.Location = new System.Drawing.Point(8, 8);
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(950, 950);
            this.graph.TabIndex = 7;
            this.graph.Click += new System.EventHandler(this.FixFocus);
            this.graph.Paint += new System.Windows.Forms.PaintEventHandler(this.graph_Paint);
            this.graph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMouseDown);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(969, 13);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(162, 25);
            this.lbl.TabIndex = 9;
            this.lbl.Text = "Current Elevation";
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeight.Location = new System.Drawing.Point(1152, 10);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(31, 32);
            this.lblHeight.TabIndex = 10;
            this.lblHeight.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(969, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Area Display Name";
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(1155, 55);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(505, 26);
            this.txtAreaName.TabIndex = 0;
            // 
            // txtAreaDisplayName
            // 
            this.txtAreaDisplayName.Location = new System.Drawing.Point(1155, 93);
            this.txtAreaDisplayName.Name = "txtAreaDisplayName";
            this.txtAreaDisplayName.Size = new System.Drawing.Size(505, 26);
            this.txtAreaDisplayName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(969, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Area Internal Name";
            // 
            // btnLinkAll
            // 
            this.btnLinkAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnLinkAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinkAll.Location = new System.Drawing.Point(1147, 881);
            this.btnLinkAll.Name = "btnLinkAll";
            this.btnLinkAll.Size = new System.Drawing.Size(156, 68);
            this.btnLinkAll.TabIndex = 8;
            this.btnLinkAll.Text = "Link All Nearby";
            this.btnLinkAll.UseVisualStyleBackColor = true;
            this.btnLinkAll.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1504, 881);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 68);
            this.button1.TabIndex = 14;
            this.button1.Text = "Link All Nearby";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // over
            // 
            this.over.Paint += new System.EventHandler<System.Windows.Forms.PaintEventArgs>(this.over_paint);
            // 
            // FormArea
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1673, 965);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtAreaDisplayName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblHeight);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.btnLinkAll);
            this.Controls.Add(this.graph);
            this.Controls.Add(this.roomPanel);
            this.Controls.Add(this.btnAddRoom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormArea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editing Area";
            this.Load += new System.EventHandler(this.Form_Area_Load);
            this.Shown += new System.EventHandler(this.FormArea_Shown);
            this.Click += new System.EventHandler(this.FrmArea_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyUp);
            this.roomPanel.ResumeLayout(false);
            this.roomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CodeProject.GraphicalOverlay over;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.Panel roomPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Button btnSouth;
        private System.Windows.Forms.Button btnEast;
        private System.Windows.Forms.Button btnWest;
        private System.Windows.Forms.Button btnNorth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel graph;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnClearExit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.TextBox txtAreaDisplayName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnLinkNearby;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLinkAll;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
    }
}

