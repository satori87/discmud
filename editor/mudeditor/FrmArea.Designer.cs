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
            this.over = new CodeProject.GraphicalOverlay(this.components);
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
            // FrmArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2268, 1235);
            this.Controls.Add(this.btnAddRoom);
            this.Controls.Add(this.graph);
            this.Name = "FrmArea";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel graph;
        private CodeProject.GraphicalOverlay over;
        private System.Windows.Forms.Button btnAddRoom;
    }
}

