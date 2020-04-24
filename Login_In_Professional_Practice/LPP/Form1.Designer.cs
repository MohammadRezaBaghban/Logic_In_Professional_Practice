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
            this.PbBinaryGraph = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(11, 27);
            this.lb1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(236, 23);
            this.lb1.TabIndex = 0;
            this.lb1.Text = "Abstract Proposition Formula:";
            // 
            // TbProposition
            // 
            this.TbProposition.Location = new System.Drawing.Point(252, 21);
            this.TbProposition.Name = "TbProposition";
            this.TbProposition.Size = new System.Drawing.Size(613, 29);
            this.TbProposition.TabIndex = 1;
            this.TbProposition.Text = ">(|(H,>(T,Y)),=(B,&(0,C))";
            // 
            // BtnParse
            // 
            this.BtnParse.Location = new System.Drawing.Point(890, 13);
            this.BtnParse.Name = "BtnParse";
            this.BtnParse.Size = new System.Drawing.Size(154, 43);
            this.BtnParse.TabIndex = 2;
            this.BtnParse.Text = "Parse Recursively ";
            this.BtnParse.UseVisualStyleBackColor = true;
            this.BtnParse.Click += new System.EventHandler(this.BtnParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Propositional Variables";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PropositionalVariables
            // 
            this.PropositionalVariables.FormattingEnabled = true;
            this.PropositionalVariables.Location = new System.Drawing.Point(252, 89);
            this.PropositionalVariables.Name = "PropositionalVariables";
            this.PropositionalVariables.Size = new System.Drawing.Size(121, 29);
            this.PropositionalVariables.TabIndex = 5;
            // 
            // PbBinaryGraph
            // 
            this.PbBinaryGraph.Location = new System.Drawing.Point(615, 89);
            this.PbBinaryGraph.Name = "PbBinaryGraph";
            this.PbBinaryGraph.Size = new System.Drawing.Size(429, 492);
            this.PbBinaryGraph.TabIndex = 6;
            this.PbBinaryGraph.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1060, 639);
            this.Controls.Add(this.PbBinaryGraph);
            this.Controls.Add(this.PropositionalVariables);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnParse);
            this.Controls.Add(this.TbProposition);
            this.Controls.Add(this.lb1);
            this.Font = new System.Drawing.Font("Segoe UI", 12.14286F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox TbProposition;
        private System.Windows.Forms.Button BtnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox PropositionalVariables;
        private System.Windows.Forms.PictureBox PbBinaryGraph;
    }
}

