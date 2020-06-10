using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Modules;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;

namespace LPP
{
    public partial class Form1 : Form
    {
        private BinaryTree _binaryTree;
        private CompositeComponent _rootOfBinaryTree;
        private readonly Calculator _calculator;
        private readonly InfixFormulaGenerator _formulaGenerator;

        public Form1()
        {
            InitializeComponent();
            _formulaGenerator = new InfixFormulaGenerator();
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
                    _binaryTree = ParsingModule.ParseInput(userInput);
                    _rootOfBinaryTree = _binaryTree.Root as CompositeComponent;
                    GenerateGraphVizBinaryGraph(_rootOfBinaryTree.GraphVizFormula, PbBinaryGraph);
                    _formulaGenerator.Calculate(_rootOfBinaryTree);

                    TbInfixFormula.Enabled = true;
                    TbInfixFormula.Text = _rootOfBinaryTree.InFixFormula;
                    TbPropositionalVariables.Text = _binaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables()
                                                    .SelectMany(x => x.Symbol.ToString())
                                                    .Aggregate("", (current, next) => current + next);

                    CalculateAndPrintTruthTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{ex.Message}");
            }

        }

        private void GenerateGraphVizBinaryGraph(string input, PictureBox pictureBox)
        {
            var text = @"graph logic {" + "\nnode[fontname = \"Arial\"]\n" + input + "\n" + "}";

            using (FileStream fs = new FileStream("ab.dot", FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(text);
                }
            }

            if (File.Exists("ab.dot"))
            {
                var dot = new Process {StartInfo = {FileName = "dot.exe", Arguments = "-Tpng -oab.png ab.dot"}};
                dot.Start();
                dot.WaitForExit();
                pictureBox.ImageLocation = @"ab.png";
            }
            else
            {
                MessageBox.Show(@"File was not created successfully");
            }
        }


        private void CalculateAndPrintTruthTable()
        {
            LbTruthTable.Items.Clear();
            LbSimplifiedTruthTable.Items.Clear();
            var truthTable = new TruthTable(_binaryTree);

            var rowsOfTruthTable = truthTable.ToString().Split('\n').ToList();
            var rowsOfSimplifiedTruthTable = truthTable.SimplifiedToString().Split('\n').ToList();

            TbTruthTableHashCode.Text = $@"{truthTable.GetHexadecimalHashCode()}";
            TbNormalDNF.Text = $"{DNF.DNFFormula(truthTable.DNF_Normal_Components)}";
            TbSimplifiedDNF.Text = $"{DNF.DNFFormula(truthTable.DNF_Simplified_Components)}";

            rowsOfTruthTable.ForEach(row => LbTruthTable.Items.Add(row));
            rowsOfSimplifiedTruthTable.ForEach(row => LbSimplifiedTruthTable.Items.Add(row));
        }
    }
}
