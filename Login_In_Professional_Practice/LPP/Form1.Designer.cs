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
            this.TbPrefixFormula = new System.Windows.Forms.TextBox();
            this.BtnParse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.PbBinaryGraph = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TbInfixFormula = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LbTruthTable = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TbPropositionalVariables = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(11, 32);
            this.lb1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(140, 23);
            this.lb1.TabIndex = 0;
            this.lb1.Text = "Post-Fix Formula:";
            // 
            // TbPrefixFormula
            // 
            this.TbPrefixFormula.Location = new System.Drawing.Point(208, 26);
            this.TbPrefixFormula.Name = "TbPrefixFormula";
            this.TbPrefixFormula.Size = new System.Drawing.Size(310, 29);
            this.TbPrefixFormula.TabIndex = 1;
            this.TbPrefixFormula.Text = ">(|(H,>(T,Y)),=(B,&(0,C))";
            // 
            // BtnParse
            // 
            this.BtnParse.Location = new System.Drawing.Point(524, 26);
            this.BtnParse.Name = "BtnParse";
            this.BtnParse.Size = new System.Drawing.Size(107, 103);
            this.BtnParse.TabIndex = 2;
            this.BtnParse.Text = "Parse Recursively";
            this.BtnParse.UseVisualStyleBackColor = true;
            this.BtnParse.Click += new System.EventHandler(this.BtnParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Propositional Variables:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PbBinaryGraph
            // 
            this.PbBinaryGraph.Location = new System.Drawing.Point(359, 153);
            this.PbBinaryGraph.Name = "PbBinaryGraph";
            this.PbBinaryGraph.Size = new System.Drawing.Size(290, 240);
            this.PbBinaryGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbBinaryGraph.TabIndex = 6;
            this.PbBinaryGraph.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "In-Fix Formula:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TbInfixFormula
            // 
            this.TbInfixFormula.Enabled = false;
            this.TbInfixFormula.Location = new System.Drawing.Point(208, 63);
            this.TbInfixFormula.Name = "TbInfixFormula";
            this.TbInfixFormula.Size = new System.Drawing.Size(310, 29);
            this.TbInfixFormula.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TbPropositionalVariables);
            this.groupBox1.Controls.Add(this.TbPrefixFormula);
            this.groupBox1.Controls.Add(this.TbInfixFormula);
            this.groupBox1.Controls.Add(this.BtnParse);
            this.groupBox1.Controls.Add(this.lb1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 135);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Abstract Proposition";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // LbTruthTable
            // 
            this.LbTruthTable.FormattingEnabled = true;
            this.LbTruthTable.ItemHeight = 21;
            this.LbTruthTable.Location = new System.Drawing.Point(12, 179);
            this.LbTruthTable.Name = "LbTruthTable";
            this.LbTruthTable.Size = new System.Drawing.Size(151, 214);
            this.LbTruthTable.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "Truth Table";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TbPropositionalVariables
            // 
            this.TbPropositionalVariables.Location = new System.Drawing.Point(208, 97);
            this.TbPropositionalVariables.Name = "TbPropositionalVariables";
            this.TbPropositionalVariables.Size = new System.Drawing.Size(100, 29);
            this.TbPropositionalVariables.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(656, 494);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LbTruthTable);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PbBinaryGraph);
            this.Font = new System.Drawing.Font("Segoe UI", 12.14286F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox TbPrefixFormula;
        private System.Windows.Forms.Button BtnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PbBinaryGraph;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbInfixFormula;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox LbTruthTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbPropositionalVariables;
    }
}

