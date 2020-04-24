﻿using System;
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

namespace LPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnParse_Click(object sender, EventArgs e)
        {
            string userInput = TbProposition.Text.Trim();

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
                    var items = ParsingModule.elements.Except(ParsingModule.Connectives).Distinct().ToList();

                    GenerateGraphVizBinaryGraph(rootOfBinaryTree.GraphVizFormula, PbBinaryGraph);

                    items.ForEach(x => PropositionalVariables.Items.Add(x));
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
