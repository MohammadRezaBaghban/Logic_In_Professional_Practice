using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LPP.Composite_Pattern.Components;
using LPP.Modules;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;

namespace LPP
{
    public partial class Form1 : Form
    {
        private BinaryTree _binaryTreeNormal;
        private BinaryTree _binaryTreeNandified;
        private readonly Nandify _nandify;
        private readonly Calculator _calculator;
        private readonly InfixFormulaGenerator _formulaGenerator;
        private Dictionary<string, string> _graphImages;

        public Form1()
        {
            InitializeComponent();
            _formulaGenerator = new InfixFormulaGenerator();
            _graphImages = new Dictionary<string, string>();
            _calculator = new Calculator();
            _nandify = new Nandify();
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
                    _binaryTreeNormal = ParsingModule.ParseInput(userInput);
                    var rootOfNormalBinaryTree = _binaryTreeNormal.Root as CompositeComponent;
                    _nandify.Calculate(rootOfNormalBinaryTree);


                    //GenerateGraphVizBinaryGraph(rootOfNormalBinaryTree.GraphVizFormula, PbBinaryGraph);
                    _formulaGenerator.Calculate(rootOfNormalBinaryTree);
                    _formulaGenerator.Calculate(rootOfNormalBinaryTree.Nand);

                    Tb_InfixFormula_Normal.Enabled = true;
                    Tb_InfixFormula_Nandified.Enabled = true;
                    Tb_InfixFormula_Normal.Text = rootOfNormalBinaryTree.InFixFormula;
                    Tb_InfixFormula_Nandified.Text = rootOfNormalBinaryTree.Nand.InFixFormula;
                    TbPropositionalVariables.Text = _binaryTreeNormal.PropositionalVariables.Get_Distinct_PropositionalVariables()
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

        private string GenerateGraphVizBinaryGraph(string input, string fileName)
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
                var dot = new Process {StartInfo = {FileName = "dot.exe", Arguments = $"-Tpng -o{fileName}.png ab.dot"}};
                dot.Start();
                dot.WaitForExit();
                return $"{fileName}.png";
            }
            else
            {
                MessageBox.Show(@"File was not created successfully");
                return "";
            }
        }


        private void CalculateAndPrintTruthTable()
        {
            LbTruthTable.Items.Clear();
            LbSimplifiedTruthTable.Items.Clear();
            var truthTable = new TruthTable(_binaryTreeNormal);

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
