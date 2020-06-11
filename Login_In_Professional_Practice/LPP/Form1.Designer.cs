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
            this.label2 = new System.Windows.Forms.Label();
            this.Tb_InfixFormula_Normal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Tb_InfixFormula_Nandified = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TbSimplifiedDNF = new System.Windows.Forms.TextBox();
            this.TbNormalDNF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Tb_TruthTableHashCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TbPropositionalVariables = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LbSimplifiedTruthTable = new System.Windows.Forms.ListBox();
            this.LbTruthTable = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Btn_Image_Open = new System.Windows.Forms.Button();
            this.LbImageName = new System.Windows.Forms.Label();
            this.BtnImageNext = new System.Windows.Forms.Button();
            this.BtnImagePrevious = new System.Windows.Forms.Button();
            this.PbBinaryGraph = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).BeginInit();
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
            this.BtnParse.Size = new System.Drawing.Size(107, 143);
            this.BtnParse.TabIndex = 2;
            this.BtnParse.Text = "Parse Recursively";
            this.BtnParse.UseVisualStyleBackColor = true;
            this.BtnParse.Click += new System.EventHandler(this.BtnParse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Propositional Variables:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // Tb_InfixFormula_Normal
            // 
            this.Tb_InfixFormula_Normal.Enabled = false;
            this.Tb_InfixFormula_Normal.Location = new System.Drawing.Point(208, 64);
            this.Tb_InfixFormula_Normal.Name = "Tb_InfixFormula_Normal";
            this.Tb_InfixFormula_Normal.Size = new System.Drawing.Size(310, 29);
            this.Tb_InfixFormula_Normal.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Tb_InfixFormula_Nandified);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.TbSimplifiedDNF);
            this.groupBox1.Controls.Add(this.TbNormalDNF);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.Tb_TruthTableHashCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TbPropositionalVariables);
            this.groupBox1.Controls.Add(this.TbPrefixFormula);
            this.groupBox1.Controls.Add(this.Tb_InfixFormula_Normal);
            this.groupBox1.Controls.Add(this.BtnParse);
            this.groupBox1.Controls.Add(this.lb1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 253);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Abstract Proposition";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // Tb_InfixFormula_Nandified
            // 
            this.Tb_InfixFormula_Nandified.Enabled = false;
            this.Tb_InfixFormula_Nandified.Location = new System.Drawing.Point(208, 102);
            this.Tb_InfixFormula_Nandified.Name = "Tb_InfixFormula_Nandified";
            this.Tb_InfixFormula_Nandified.Size = new System.Drawing.Size(310, 29);
            this.Tb_InfixFormula_Nandified.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 23);
            this.label6.TabIndex = 16;
            this.label6.Text = "NAND Formula:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TbSimplifiedDNF
            // 
            this.TbSimplifiedDNF.Location = new System.Drawing.Point(208, 216);
            this.TbSimplifiedDNF.Name = "TbSimplifiedDNF";
            this.TbSimplifiedDNF.Size = new System.Drawing.Size(423, 29);
            this.TbSimplifiedDNF.TabIndex = 15;
            // 
            // TbNormalDNF
            // 
            this.TbNormalDNF.Location = new System.Drawing.Point(208, 178);
            this.TbNormalDNF.Name = "TbNormalDNF";
            this.TbNormalDNF.Size = new System.Drawing.Size(423, 29);
            this.TbNormalDNF.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "Simplified DNF: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Normal DNF: ";
            // 
            // Tb_TruthTableHashCode
            // 
            this.Tb_TruthTableHashCode.Location = new System.Drawing.Point(418, 140);
            this.Tb_TruthTableHashCode.Name = "Tb_TruthTableHashCode";
            this.Tb_TruthTableHashCode.Size = new System.Drawing.Size(100, 29);
            this.Tb_TruthTableHashCode.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 23);
            this.label4.TabIndex = 10;
            this.label4.Text = "HashCode:";
            // 
            // TbPropositionalVariables
            // 
            this.TbPropositionalVariables.Location = new System.Drawing.Point(208, 140);
            this.TbPropositionalVariables.Name = "TbPropositionalVariables";
            this.TbPropositionalVariables.Size = new System.Drawing.Size(100, 29);
            this.TbPropositionalVariables.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LbSimplifiedTruthTable);
            this.groupBox2.Controls.Add(this.LbTruthTable);
            this.groupBox2.Location = new System.Drawing.Point(12, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 311);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Truth Tables";
            // 
            // LbSimplifiedTruthTable
            // 
            this.LbSimplifiedTruthTable.FormattingEnabled = true;
            this.LbSimplifiedTruthTable.ItemHeight = 21;
            this.LbSimplifiedTruthTable.Location = new System.Drawing.Point(173, 27);
            this.LbSimplifiedTruthTable.Name = "LbSimplifiedTruthTable";
            this.LbSimplifiedTruthTable.Size = new System.Drawing.Size(162, 277);
            this.LbSimplifiedTruthTable.TabIndex = 16;
            // 
            // LbTruthTable
            // 
            this.LbTruthTable.FormattingEnabled = true;
            this.LbTruthTable.ItemHeight = 21;
            this.LbTruthTable.Location = new System.Drawing.Point(6, 27);
            this.LbTruthTable.Name = "LbTruthTable";
            this.LbTruthTable.Size = new System.Drawing.Size(162, 277);
            this.LbTruthTable.TabIndex = 14;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Btn_Image_Open);
            this.groupBox3.Controls.Add(this.LbImageName);
            this.groupBox3.Controls.Add(this.BtnImageNext);
            this.groupBox3.Controls.Add(this.BtnImagePrevious);
            this.groupBox3.Controls.Add(this.PbBinaryGraph);
            this.groupBox3.Location = new System.Drawing.Point(359, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 311);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Binary Trees";
            // 
            // Btn_Image_Open
            // 
            this.Btn_Image_Open.Enabled = false;
            this.Btn_Image_Open.Location = new System.Drawing.Point(191, 28);
            this.Btn_Image_Open.Name = "Btn_Image_Open";
            this.Btn_Image_Open.Size = new System.Drawing.Size(92, 38);
            this.Btn_Image_Open.TabIndex = 18;
            this.Btn_Image_Open.Text = "Open";
            this.Btn_Image_Open.UseVisualStyleBackColor = true;
            this.Btn_Image_Open.Click += new System.EventHandler(this.Btn_Image_Open_Click);
            // 
            // LbImageName
            // 
            this.LbImageName.AutoSize = true;
            this.LbImageName.Location = new System.Drawing.Point(15, 36);
            this.LbImageName.Name = "LbImageName";
            this.LbImageName.Size = new System.Drawing.Size(86, 23);
            this.LbImageName.TabIndex = 17;
            this.LbImageName.Text = "No Image";
            // 
            // BtnImageNext
            // 
            this.BtnImageNext.Enabled = false;
            this.BtnImageNext.Location = new System.Drawing.Point(149, 28);
            this.BtnImageNext.Name = "BtnImageNext";
            this.BtnImageNext.Size = new System.Drawing.Size(36, 38);
            this.BtnImageNext.TabIndex = 16;
            this.BtnImageNext.Text = ">";
            this.BtnImageNext.UseVisualStyleBackColor = true;
            this.BtnImageNext.Click += new System.EventHandler(this.Btn_Image_Next_Click);
            // 
            // BtnImagePrevious
            // 
            this.BtnImagePrevious.Enabled = false;
            this.BtnImagePrevious.Location = new System.Drawing.Point(107, 28);
            this.BtnImagePrevious.Name = "BtnImagePrevious";
            this.BtnImagePrevious.Size = new System.Drawing.Size(36, 38);
            this.BtnImagePrevious.TabIndex = 15;
            this.BtnImagePrevious.Text = "<";
            this.BtnImagePrevious.UseVisualStyleBackColor = true;
            this.BtnImagePrevious.Click += new System.EventHandler(this.Btn_Image_Previous_Click);
            // 
            // PbBinaryGraph
            // 
            this.PbBinaryGraph.Location = new System.Drawing.Point(6, 71);
            this.PbBinaryGraph.Name = "PbBinaryGraph";
            this.PbBinaryGraph.Size = new System.Drawing.Size(277, 234);
            this.PbBinaryGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbBinaryGraph.TabIndex = 14;
            this.PbBinaryGraph.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(656, 595);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12.14286F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbBinaryGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.TextBox TbPrefixFormula;
        private System.Windows.Forms.Button BtnParse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Tb_InfixFormula_Normal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TbPropositionalVariables;
        private System.Windows.Forms.TextBox Tb_TruthTableHashCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox LbSimplifiedTruthTable;
        private System.Windows.Forms.ListBox LbTruthTable;
        private System.Windows.Forms.TextBox TbSimplifiedDNF;
        private System.Windows.Forms.TextBox TbNormalDNF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Tb_InfixFormula_Nandified;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox PbBinaryGraph;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button BtnImageNext;
        private System.Windows.Forms.Button BtnImagePrevious;
        private System.Windows.Forms.Label LbImageName;
        private System.Windows.Forms.Button Btn_Image_Open;
    }
}

