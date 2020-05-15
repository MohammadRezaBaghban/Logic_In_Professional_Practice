using System;
using System.Linq;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;

namespace LPP.Modules
{
    public class TruthTable
    {
        public Row[] Rows;
        public int NumberOfVariables { get; }
        public PropositionalVariable[] PropositionalVariables;
        public CompositeComponent RootOfBinaryTree { get; }

        public TruthTable(CompositeComponent rootOfBinaryTree)
        {
            this.RootOfBinaryTree = rootOfBinaryTree;
            PropositionalVariables = rootOfBinaryTree.PropositionalVariables.GetPropositionalVariables();
            NumberOfVariables = PropositionalVariables.Length;
            
            Rows = new Row[(int)Math.Pow(2, NumberOfVariables)];
            for (var i = 0; i < Rows.Length; i++)
            {
                Rows[i] = new Row(NumberOfVariables);
            }

            FillRows();
        }

        private void FillRows()
        {
            var columnIndex = 0;
            for (var i = NumberOfVariables; i > 0; i--)
            {
                var consequential01S = Math.Pow(2, i - 1);

                var OneOrZero = false;
                for (var j = 0; j < Rows.Length; j++)
                {
                    Rows[j].PropositionValues[columnIndex] = OneOrZero;
                    if ((j + 1) % consequential01S == 0)
                    {
                        OneOrZero = !OneOrZero;
                    }
                }
                columnIndex++;
            }
        }

        

    }
}
