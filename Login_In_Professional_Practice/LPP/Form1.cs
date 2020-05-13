using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LPP.Composite_Pattern.Node;
using LPP.Visitor_Pattern;

namespace LPP
{
    public partial class Form1 : Form
    {
        private readonly InfixFormula_Generator _formulaFormulaGenerator;
        public Form1()
        {
            InitializeComponent();
            _formulaFormulaGenerator = new InfixFormula_Generator();
        }

        private void BtnParse_Click(object sender, EventArgs e)
        {
            string userInput = TbPrefixFormula.Text.Trim();

            try
            {
                if (userInput.Length < 4)
                {
                    throw new LPPException("Format of input is not correct");
                }
                else
                {
                    PropositionalVariables.Items.Clear();

                    var rootOfBinaryTree = ParsingModule.ParseInput(userInput);
                    
                    GenerateGraphVizBinaryGraph(rootOfBinaryTree.GraphVizFormula, PbBinaryGraph);
                    _formulaFormulaGenerator.Calculate(rootOfBinaryTree);
                    TbInfixFormula.Text = rootOfBinaryTree.InFixFormula;
                    TbInfixFormula.Enabled = true;

                    rootOfBinaryTree.PropositionalVariables.GetPropositionalVariables().ToList().ForEach(p=> PropositionalVariables.Items.Add(p.Symbol));
                    PropositionalVariables.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");

            }

        }

        private void GenerateGraphVizBinaryGraph(string input, PictureBox pictureBox)
        {
            string text = @"graph logic {" + "\nnode[fontname = \"Arial\"]\n" + input + "\n" + "}";

            using (FileStream fs = new FileStream("ab.dot", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(text);
                }
            }

            if (File.Exists("ab.dot"))
            {
                Process dot = new Process();
                dot.StartInfo.FileName = "dot.exe";
                dot.StartInfo.Arguments = "-Tpng -oab.png ab.dot";
                dot.Start();
                dot.WaitForExit();
                pictureBox.ImageLocation = "ab.png";
            }
            else
            {
                MessageBox.Show("File was not created successfullu");
            }
        }
    }
}
