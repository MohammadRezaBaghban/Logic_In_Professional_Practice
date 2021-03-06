﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LPP.Composite_Pattern.Components;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;

namespace LPP
{
    public partial class Form1 : Form
    {
        //Fields and Variables
        private BinaryTree _binaryTreeNormal;
        private BinaryTree _binaryTreeNandified;
        private TruthTable _truthTable;
        private TruthTable _truthTableNand;
        private TableauxNode _tableauxRoot;
        private readonly Dictionary<int, string> _graphImages;
        private readonly int numberOfImage = 4;
        private int _imageIndex = 0;

        //Dependencies
        private readonly Nandify _nandify;
        private readonly Tableaux _tableaux;
        private readonly InfixFormulaGenerator _formulaGenerator;

        public Form1()
        {
            InitializeComponent();
            _formulaGenerator = InfixFormulaGenerator.Calculator;
            _tableaux = new Tableaux();
            _graphImages = new Dictionary<int, string>();
            _nandify = new Nandify();
        }

        //Methods 
        private void PopulateListBoxesWithValues()
        {
            LbHashCodes.Items.Clear();
            LbTruthTable.Items.Clear();
            LbSimplifiedTruthTable.Items.Clear();

            var rowsOfTruthTable = _truthTable.ToString().Split('\n').ToList();
            var rowsOfSimplifiedTruthTable = _truthTable.SimplifiedToString().Split('\n').ToList();
            rowsOfTruthTable.ForEach(row => LbTruthTable.Items.Add(row));
            rowsOfSimplifiedTruthTable.ForEach(row => LbSimplifiedTruthTable.Items.Add(row));

            LbHashCodes.Items.Add("HashCodes");
            LbHashCodes.Items.Add($"Normal - Original: {_truthTable.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Normal - NAND: {_truthTableNand.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Normal - DNF: {_truthTable.DnfTruthTable?.GetHexadecimalHashCode()}");
            LbHashCodes.Items.Add($"Simplified - Original: {_truthTable.GetHexadecimalSimplifiedHashCode()}");
            LbHashCodes.Items.Add($"Simplified - DNF: {_truthTable.DnfTruthTable?.GetHexadecimalSimplifiedHashCode()}");
        }
        private void PopulateTextBoxesWithValues(CompositeComponent root)
        {
            _formulaGenerator.Calculate(root);
            _formulaGenerator.Calculate(root.Nand);
            Tb_InfixFormula_Normal.Enabled = true;
            Tb_InfixFormula_Nandified.Enabled = true;
            Tb_InfixFormula_Normal.Text = root.InFixFormula;
            Tb_InfixFormula_Nandified.Text = root.Nand.InFixFormula;
            Tb_TruthTableHashCode.Text = $@"{_truthTable.GetHexadecimalHashCode()}";
            TbNormalDNF.Text = $@"{Dnf.DnfFormula(_truthTable.DnfNormalComponents)}";
            TbSimplifiedDNF.Text = $@"{Dnf.DnfFormula(_truthTable.DnfSimplifiedComponents)}";
            TbPropositionalVariables.Text = _binaryTreeNormal.PropositionalVariables.Get_Distinct_PropositionalVariables()
                .SelectMany(x => x.Symbol.ToString()).Aggregate("", (current, next) => current + next);

            BtnParseRecursively.BackColor = _truthTable.GetHexadecimalHashCode() == _truthTableNand.GetHexadecimalHashCode()
                    ? Color.MediumSeaGreen : Color.PaleVioletRed;
        }
        private void PopulatePictureBoxWithImages(CompositeComponent root)
        {
            _graphImages.Add(0, GenerateGraphVizBinaryGraph(root.GraphVizFormula, "Normal"));
            _graphImages.Add(1, GenerateGraphVizBinaryGraph(root.Nand.GraphVizFormula, "NAND"));
            _graphImages.Add(2, GenerateGraphVizBinaryGraph(_truthTable.DnfNormalBinaryTree?.Root.GraphVizFormula, "DNF"));

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
            string userInput = TbFormulaInput.Text.Trim();
            try
            {
                if (userInput.Length < 4)
                {
                    throw new Exception("Format of input is not correct");
                }
                else
                {
                    _graphImages.Clear();
                    _binaryTreeNormal = ParsingModule.Parse(userInput);
                    var rootOfNormalBinaryTree = _binaryTreeNormal.Root as CompositeComponent;
                    _nandify.Calculate(rootOfNormalBinaryTree);

                    _truthTable = new TruthTable(_binaryTreeNormal);
                    _truthTableNand = new TruthTable(Nandify.BinaryTree);
                    _truthTable.ProcessDnf();

                    PopulateTextBoxesWithValues(rootOfNormalBinaryTree);
                    PopulateListBoxesWithValues();
                    PopulatePictureBoxWithImages(rootOfNormalBinaryTree);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"{ex.Message}");
            }

        }

        private void BtnSemanticTableaux_Click(object sender, EventArgs e)
        {
            try
            {
                string userInput = TbFormulaInput.Text.Trim();
                if (userInput.Length < 4)
                {
                    throw new Exception("Format of input is not correct");
                }
                else
                {
                    _graphImages.Clear();
                    userInput = $"~({userInput})";
                    var binaryTree = ParsingModule.Parse(userInput);
                    _tableauxRoot = new TableauxNode(binaryTree.Root as CompositeComponent);
                    _tableauxRoot.IsClosed();

                    _graphImages.Add(0, GenerateGraphVizBinaryGraph(binaryTree.Root.GraphVizFormula, "Normal"));
                    _graphImages.Add(1, GenerateGraphVizBinaryGraph(_tableauxRoot.GraphVizFormula(), "Tableaux"));
                    BtnSemanticTableaux.BackColor = _tableauxRoot.LeafIsClosed == true ? Color.ForestGreen : Color.Tomato;
                    _imageIndex = 0;
                    BtnImageNext.Enabled = true;
                    Btn_Image_Open.Enabled = true;
                    Tb_InfixFormula_Normal.Enabled = true;
                    PbBinaryGraph.ImageLocation = _graphImages[0];
                    _formulaGenerator.Calculate(binaryTree.Root);
                    Tb_InfixFormula_Normal.Text = binaryTree.Root.InFixFormula;
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
            if (_imageIndex < numberOfImage-1 && _imageIndex+1<_graphImages.Count)
            {
                _imageIndex++;
            }
            else
            {
                _imageIndex = 0;
            }
            var imageName = _graphImages[_imageIndex];
            PbBinaryGraph.ImageLocation = imageName;
            LbImageName.Text = _graphImages[_imageIndex].Substring(0, imageName.IndexOf(".", StringComparison.Ordinal));
        }
        private void Btn_Image_Previous_Click(object sender, EventArgs e)
        {

            if (_imageIndex > 0 && _imageIndex -1 > 0)
            {
                _imageIndex--;
            }
            else
            {
                _imageIndex = _graphImages.Count- 1;
            }
            var imageName = _graphImages[_imageIndex];
            PbBinaryGraph.ImageLocation = imageName;
            LbImageName.Text = _graphImages[_imageIndex].Substring(0, imageName.IndexOf(".", StringComparison.Ordinal));
        }
    }
}
