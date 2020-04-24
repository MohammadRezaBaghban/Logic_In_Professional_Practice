using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    items.ForEach(x => PropositionalVariables.Items.Add(x));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");

            }

        }
    }
}
