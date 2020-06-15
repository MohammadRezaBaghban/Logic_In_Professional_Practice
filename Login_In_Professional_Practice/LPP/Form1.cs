using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        //Fields and Variables
        private BinaryTree _binaryTreeNormal;
        private BinaryTree _binaryTreeNandified;
        private TruthTable truthTable;
        private TruthTable truthTableNand;
        private readonly Nandify _nandify;
        private readonly Calculator _calculator;
        private readonly InfixFormulaGenerator _formulaGenerator;
        private Dictionary<int, string> _graphImages;
        private int _imageIndex = 0;
        private readonly int numberOfImage = 3;

        public Form1()
        {
            InitializeComponent();
            _formulaGenerator = new InfixFormulaGenerator();
            _graphImages = new Dictionary<int, string>();
            _calculator = new Calculator();
            _nandify = new Nandify();
        }

        //Methods 
        private void PopulateListBoxesWithValues()
        {
            LbHashCodes.Items.Clear();
            LbTruthTable.Items.Clear();
            LbSimplifiedTruthTable.Items.Clear();

            var rowsOfTruthTable = truthTable.ToString().Split('\n').ToList();
            var rowsOfSimplifiedTruthTable = truthTable.SimplifiedToString().Split('\n').ToList();
            rowsOfTruthTable.ForEach(row => LbTruthTable.Items.Add(row));
            rowsOfSimplifiedTruthTable.ForEach(row => LbSimplifiedTruthTable.Items.Add(row));

            LbHashCodes.Items.Add("HashCodes");
            LbHashCodes.Items.Add($"Normal - Original: {truthTable.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Normal - NAND: {truthTableNand.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Normal - DNF: {truthTable.DnftTruthTable?.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Simplified - Original: {truthTable.GetHexadecimalSimplifiedHashCode()}");
            LbHashCodes.Items.Add($"Simplified - DNF: {truthTable.DnftTruthTable?.GetHexadecimalSimplifiedHashCode()}");
        }
        private void PopulateTextBoxesWithValues(CompositeComponent root)
        {
            _formulaGenerator.Calculate(root);
            _formulaGenerator.Calculate(root.Nand);
            Tb_InfixFormula_Normal.Enabled = true;
            Tb_InfixFormula_Nandified.Enabled = true;
            Tb_InfixFormula_Normal.Text = root.InFixFormula;
            Tb_InfixFormula_Nandified.Text = root.Nand.InFixFormula;
            Tb_TruthTableHashCode.Text = $@"{truthTable.GetHexadecimalHashCode()}";
            TbNormalDNF.Text = $"{DNF.DNFFormula(truthTable.DNF_Normal_Components)}";
            TbSimplifiedDNF.Text = $"{DNF.DNFFormula(truthTable.DNF_Simplified_Components)}";
            TbPropositionalVariables.Text = _binaryTreeNormal.PropositionalVariables.Get_Distinct_PropositionalVariables()
                .SelectMany(x => x.Symbol.ToString()).Aggregate("", (current, next) => current + next);

            if (truthTable.GetHexadecimalHashCode() == truthTableNand.GetHexadecimalHashCode())
                Tb_TruthTableHashCode.BackColor = Color.MediumSeaGreen;
            else
                Tb_TruthTableHashCode.BackColor = Color.PaleVioletRed;

        }
        private void PopulatePictureBoxWithImages(CompositeComponent root)
        {
            _graphImages.Add(0, GenerateGraphVizBinaryGraph(root.GraphVizFormula, "Normal"));
            _graphImages.Add(1, GenerateGraphVizBinaryGraph(root.Nand.GraphVizFormula, "NAND"));
            _graphImages.Add(2, GenerateGraphVizBinaryGraph(truthTable.DNF_Normal_BinaryTree?.Root.GraphVizFormula, "DNF"));
            LbImageName.Text = _graphImages[0].Substring(0, _graphImages[0].IndexOf("."));
            PbBinaryGraph.ImageLocation = _graphImages[0];
            BtnImagePrevious.Enabled = true;
            Btn_Image_Open.Enabled = true;
            BtnImageNext.Enabled = true;
            _imageIndex = 0;
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
                var dot = new Process { StartInfo = { FileName = "dot.exe", Arguments = $"-Tpng -o{fileName}.png ab.dot" } };
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


        //Form Event Handlers
        private void BtnParse_Click(object sender, EventArgs e)
        {
            string userInput = TbPrefixFormula.Text.Trim();
            try
            {
                if (userInput.Length < 4)
                {
                    throw new Exception("Format of input is not correct");
                }
                else
                {
                    _graphImages.Clear();
                    _binaryTreeNormal = ParsingModule.ParseInput(userInput);
                    var rootOfNormalBinaryTree = _binaryTreeNormal.Root as CompositeComponent;
                    _nandify.Calculate(rootOfNormalBinaryTree);

                    truthTable = new TruthTable(_binaryTreeNormal);
                    truthTableNand = new TruthTable(Nandify.binaryTree);
                    truthTable.ProcessDNF();

                    PopulateListBoxesWithValues();
                    PopulateTextBoxesWithValues(rootOfNormalBinaryTree);
                    PopulatePictureBoxWithImages(rootOfNormalBinaryTree);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{ex.Message}");
            }

        }
        private void Btn_Image_Open_Click(object sender, EventArgs e)
        {
            Process.Start($@"{ _graphImages[_imageIndex]}");
        }
        private void Btn_Image_Next_Click(object sender, EventArgs e)
        {
            if (_imageIndex < numberOfImage-1)
            {
                _imageIndex++;
            }
            else
            {
                _imageIndex = 0;
            }
            var imageName = _graphImages[_imageIndex];
            PbBinaryGraph.ImageLocation = imageName;
            LbImageName.Text = _graphImages[_imageIndex].Substring(0, imageName.IndexOf("."));

        }
        private void Btn_Image_Previous_Click(object sender, EventArgs e)
        {
            if (_imageIndex > 0)
            {
                _imageIndex--;
            }
            else
            {
                _imageIndex = numberOfImage-1;
            }
            var imageName = _graphImages[_imageIndex];
            PbBinaryGraph.ImageLocation = imageName;
            LbImageName.Text = _graphImages[_imageIndex].Substring(0, imageName.IndexOf("."));
        }
    }
}
