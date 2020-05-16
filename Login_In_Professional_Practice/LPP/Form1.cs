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
using LPP.Modules;
using LPP.NodeComponents;
using LPP.Visitor_Pattern;

namespace LPP
{
    public partial class Form1 : Form
    {
        private CompositeComponent _rootOfBinaryTree;
        private readonly Calculator _calculator;
        private readonly InfixFormula_Generator _formulaFormulaGenerator;

        public Form1()
        {
            InitializeComponent();
            _formulaFormulaGenerator = new InfixFormula_Generator();
            _calculator = new Calculator();
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
                    _rootOfBinaryTree = ParsingModule.ParseInput(userInput);

                    GenerateGraphVizBinaryGraph(_rootOfBinaryTree.GraphVizFormula, PbBinaryGraph);
                    _formulaFormulaGenerator.Calculate(_rootOfBinaryTree);

                    TbInfixFormula.Enabled = true;
                    TbInfixFormula.Text = _rootOfBinaryTree.InFixFormula;
                    TbPropositionalVariables.Text = _rootOfBinaryTree
                                                    .PropositionalVariables.GetPropositionalVariables()
                                                    .SelectMany(x=>x.Symbol.ToString())
                                                    .Aggregate("",(current,next) => current+next);

                    CalculateAndPrintTruthTable(_rootOfBinaryTree);
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
                MessageBox.Show(@"File was not created successfully");
            }
        }


        private void CalculateAndPrintTruthTable(CompositeComponent root)
        {
            LbTruthTable.Items.Clear();
            var truthTable = new TruthTable(_rootOfBinaryTree,_calculator);
            var rowsOfTruthTable = truthTable.ToString().Split('\n').ToList();
            TbTruthTableHashCode.Text = $"{truthTable.GetHexadecimalHashCode()}";
            rowsOfTruthTable.ForEach(row => LbTruthTable.Items.Add(row));
        }
    }
}
