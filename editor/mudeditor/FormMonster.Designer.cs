namespace MUDEdit {
    partial class FormMonster {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMonster));
            this.tab = new System.Windows.Forms.TabControl();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.txtMagDef = new System.Windows.Forms.TextBox();
            this.txtMagEva = new System.Windows.Forms.TextBox();
            this.txtPhysDef = new System.Windows.Forms.TextBox();
            this.txtPhysEva = new System.Windows.Forms.TextBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtLuck = new System.Windows.Forms.TextBox();
            this.txtWis = new System.Windows.Forms.TextBox();
            this.txtInt = new System.Windows.Forms.TextBox();
            this.txtCon = new System.Windows.Forms.TextBox();
            this.txtDex = new System.Windows.Forms.TextBox();
            this.txtStr = new System.Windows.Forms.TextBox();
            this.txtHP = new System.Windows.Forms.TextBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabBehavior = new System.Windows.Forms.TabPage();
            this.tabLoot = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tab.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabMain);
            this.tab.Controls.Add(this.tabBehavior);
            this.tab.Controls.Add(this.tabLoot);
            this.tab.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(871, 661);
            this.tab.TabIndex = 15;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.txtMagDef);
            this.tabMain.Controls.Add(this.txtMagEva);
            this.tabMain.Controls.Add(this.txtPhysDef);
            this.tabMain.Controls.Add(this.txtPhysEva);
            this.tabMain.Controls.Add(this.txtSpeed);
            this.tabMain.Controls.Add(this.txtLuck);
            this.tabMain.Controls.Add(this.txtWis);
            this.tabMain.Controls.Add(this.txtInt);
            this.tabMain.Controls.Add(this.txtCon);
            this.tabMain.Controls.Add(this.txtDex);
            this.tabMain.Controls.Add(this.txtStr);
            this.tabMain.Controls.Add(this.txtHP);
            this.tabMain.Controls.Add(this.txtDisplayName);
            this.tabMain.Controls.Add(this.txtName);
            this.tabMain.Controls.Add(this.label1);
            this.tabMain.Location = new System.Drawing.Point(4, 38);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(863, 619);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Stats";
            this.tabMain.UseVisualStyleBackColor = true;
            // 
            // txtMagDef
            // 
            this.txtMagDef.AccessibleName = "int";
            this.txtMagDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMagDef.Location = new System.Drawing.Point(228, 426);
            this.txtMagDef.Name = "txtMagDef";
            this.txtMagDef.Size = new System.Drawing.Size(100, 30);
            this.txtMagDef.TabIndex = 29;
            this.txtMagDef.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtMagEva
            // 
            this.txtMagEva.AccessibleName = "int";
            this.txtMagEva.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMagEva.Location = new System.Drawing.Point(228, 394);
            this.txtMagEva.Name = "txtMagEva";
            this.txtMagEva.Size = new System.Drawing.Size(100, 30);
            this.txtMagEva.TabIndex = 28;
            this.txtMagEva.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtPhysDef
            // 
            this.txtPhysDef.AccessibleName = "int";
            this.txtPhysDef.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhysDef.Location = new System.Drawing.Point(228, 362);
            this.txtPhysDef.Name = "txtPhysDef";
            this.txtPhysDef.Size = new System.Drawing.Size(100, 30);
            this.txtPhysDef.TabIndex = 27;
            this.txtPhysDef.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtPhysEva
            // 
            this.txtPhysEva.AccessibleName = "int";
            this.txtPhysEva.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhysEva.Location = new System.Drawing.Point(228, 330);
            this.txtPhysEva.Name = "txtPhysEva";
            this.txtPhysEva.Size = new System.Drawing.Size(100, 30);
            this.txtPhysEva.TabIndex = 26;
            this.txtPhysEva.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtSpeed
            // 
            this.txtSpeed.AccessibleName = "int";
            this.txtSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpeed.Location = new System.Drawing.Point(228, 298);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(100, 30);
            this.txtSpeed.TabIndex = 25;
            this.txtSpeed.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtLuck
            // 
            this.txtLuck.AccessibleName = "int";
            this.txtLuck.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLuck.Location = new System.Drawing.Point(228, 266);
            this.txtLuck.Name = "txtLuck";
            this.txtLuck.Size = new System.Drawing.Size(100, 30);
            this.txtLuck.TabIndex = 24;
            this.txtLuck.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtWis
            // 
            this.txtWis.AccessibleName = "int";
            this.txtWis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWis.Location = new System.Drawing.Point(228, 234);
            this.txtWis.Name = "txtWis";
            this.txtWis.Size = new System.Drawing.Size(100, 30);
            this.txtWis.TabIndex = 23;
            this.txtWis.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtInt
            // 
            this.txtInt.AccessibleName = "int";
            this.txtInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInt.Location = new System.Drawing.Point(228, 202);
            this.txtInt.Name = "txtInt";
            this.txtInt.Size = new System.Drawing.Size(100, 30);
            this.txtInt.TabIndex = 22;
            this.txtInt.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtCon
            // 
            this.txtCon.AccessibleName = "int";
            this.txtCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCon.Location = new System.Drawing.Point(228, 170);
            this.txtCon.Name = "txtCon";
            this.txtCon.Size = new System.Drawing.Size(100, 30);
            this.txtCon.TabIndex = 21;
            this.txtCon.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtDex
            // 
            this.txtDex.AccessibleName = "int";
            this.txtDex.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDex.Location = new System.Drawing.Point(228, 138);
            this.txtDex.Name = "txtDex";
            this.txtDex.Size = new System.Drawing.Size(100, 30);
            this.txtDex.TabIndex = 20;
            this.txtDex.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtStr
            // 
            this.txtStr.AccessibleName = "int";
            this.txtStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStr.Location = new System.Drawing.Point(228, 106);
            this.txtStr.Name = "txtStr";
            this.txtStr.Size = new System.Drawing.Size(100, 30);
            this.txtStr.TabIndex = 19;
            this.txtStr.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtHP
            // 
            this.txtHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHP.Location = new System.Drawing.Point(228, 74);
            this.txtHP.Name = "txtHP";
            this.txtHP.Size = new System.Drawing.Size(100, 30);
            this.txtHP.TabIndex = 18;
            this.txtHP.Text = "1d1+1";
            this.txtHP.Leave += new System.EventHandler(this.txtHP_Leave);
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayName.Location = new System.Drawing.Point(228, 42);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(414, 30);
            this.txtDisplayName.TabIndex = 17;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(228, 10);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(414, 30);
            this.txtName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 448);
            this.label1.TabIndex = 15;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabBehavior
            // 
            this.tabBehavior.Location = new System.Drawing.Point(4, 38);
            this.tabBehavior.Name = "tabBehavior";
            this.tabBehavior.Padding = new System.Windows.Forms.Padding(3);
            this.tabBehavior.Size = new System.Drawing.Size(863, 619);
            this.tabBehavior.TabIndex = 1;
            this.tabBehavior.Text = "Behavior";
            this.tabBehavior.UseVisualStyleBackColor = true;
            // 
            // tabLoot
            // 
            this.tabLoot.Location = new System.Drawing.Point(4, 38);
            this.tabLoot.Name = "tabLoot";
            this.tabLoot.Size = new System.Drawing.Size(863, 619);
            this.tabLoot.TabIndex = 2;
            this.tabLoot.Text = "Loot";
            this.tabLoot.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(490, 551);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 68);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(16, 551);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(156, 68);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormMonster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 628);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMonster";
            this.Text = "Editing Monster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMonster_FormClosing);
            this.Load += new System.EventHandler(this.FormMonster_Load);
            this.Shown += new System.EventHandler(this.FormMonster_Shown);
            this.tab.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.TextBox txtMagDef;
        private System.Windows.Forms.TextBox txtMagEva;
        private System.Windows.Forms.TextBox txtPhysDef;
        private System.Windows.Forms.TextBox txtPhysEva;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtLuck;
        private System.Windows.Forms.TextBox txtWis;
        private System.Windows.Forms.TextBox txtInt;
        private System.Windows.Forms.TextBox txtCon;
        private System.Windows.Forms.TextBox txtDex;
        private System.Windows.Forms.TextBox txtStr;
        private System.Windows.Forms.TextBox txtHP;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabBehavior;
        private System.Windows.Forms.TabPage tabLoot;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}