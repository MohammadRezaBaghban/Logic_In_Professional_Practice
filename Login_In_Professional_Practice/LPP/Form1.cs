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

            if (userInput.Length < 4)
            {
                try
                {
                    throw new LPPException("Format of input is not correct");
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                    TbProposition.Text = "";
                }
            }
            else
            {
                listBox1.Items.Clear();
                ParsingModule.ParseInput(userInput);
                var items = ParsingModule.elements;
                listBox1.Items.AddRange(items.ToArray());
            }
        }
    }
}
