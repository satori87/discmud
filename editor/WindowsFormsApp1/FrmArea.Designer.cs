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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.over = new CodeProject.GraphicalOverlay(this.components);
            this.button1 = new System.Windows.Forms.Button();
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
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(1466, 387);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 100);
            this.label1.TabIndex = 2;
            this.label1.Text = "The Haunted Inn - Second Floor Hallway by the Sewing room";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(1235, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 65);
            this.label2.TabIndex = 3;
            this.label2.Text = "Add Room";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1236, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 68);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(2268, 1235);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graph);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel graph;
        private CodeProject.GraphicalOverlay over;
        private System.Windows.Forms.Button button1;
    }
}

