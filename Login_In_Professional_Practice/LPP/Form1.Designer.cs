namespace LPP
{
    partial class Form1
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
            this.lb1 = new System.Windows.Forms.Label();
            this.TbProposition = new System.Windows.Forms.TextBox();
            this.BtnParse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PropositionalVariables = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(11, 27);
            this.lb1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(395, 40);
            this.lb1.TabIndex = 0;
            this.lb1.Text = "Abstract Proposition Formula:";
            // 
            // TbProposition
            // 
            this.TbProposition.Location = new System.Drawing.Point(425, 24);
            this.TbProposition.Name = "TbProposition";
            this.TbProposition.Size = new System.Drawing.Size(613, 45);
            this.TbProposition.TabIndex = 1;
            // 
            // BtnParse
            // 
            this.BtnParse.Location = new System.Drawing.Point(1071, 19);
            this.BtnParse.Name = "BtnParse";
            this.BtnParse.Size = new System.Drawing.Size(248, 54);
            this.BtnParse.TabIndex = 2;
            this.BtnParse.Text = "ParseRecursively Recursively";
            this.BtnParse.UseVisualStyleBackColor = true;
            this.BtnParse.Click += new System.EventHandler(this.BtnParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Propositional Variables";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PropositionalVariables
            // 
            this.PropositionalVariables.FormattingEnabled = true;
            this.PropositionalVariables.Location = new System.Drawing.Point(425, 89);
            this.PropositionalVariables.Name = "PropositionalVariables";
            this.PropositionalVariables.Size = new System.Drawing.Size(121, 46);
            this.PropositionalVariables.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 38F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1326, 639);
            this.Controls.Add(this.PropositionalVariables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnParse);
            this.Controls.Add(this.TbProposition);
            this.Controls.Add(this.lb1);
            this.Font = new System.Drawing.Font("Segoe UI", 12.14286F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox TbProposition;
        private System.Windows.Forms.Button BtnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PropositionalVariables;
    }
}

