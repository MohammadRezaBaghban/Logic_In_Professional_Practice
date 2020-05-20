using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;
using LPP.Visitor_Pattern;

namespace LPP.Modules
{
    public class TruthTable
    {
        public Row[] Rows;
        public int NumberOfVariables { get; }
        public PropositionalVariable[] PropositionalVariables;
        public CompositeComponent RootOfBinaryTree { get; }

        public TruthTable(CompositeComponent rootOfBinaryTree, Calculator calculator)
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
            calculator.Visit(this);
        }

        private void FillRows()
        {
            var columnIndex = 0;
            for (var i = NumberOfVariables; i > 0; i--)
            {
                var consequential01S = Math.Pow(2, i - 1);

                var oneOrZero = false;
                for (var j = 0; j < Rows.Length; j++)
                {
                    Rows[j].PropositionValues[columnIndex] = oneOrZero;
                    if ((j + 1) % consequential01S == 0)
                    {
                        oneOrZero = !oneOrZero;
                    }
                }
                columnIndex++;
            }
        }

        public override string ToString()
        {
            var headOfTruthTable = PropositionalVariables.Aggregate(" ", (current, variable) => current + $"{variable.Symbol}  ") + " v";
            var rowsOfTruthTable = Rows.Aggregate("", (current, row) => current + $"\n{row.ToString()}");
            return headOfTruthTable + rowsOfTruthTable;
        }

        public string GetHexadecimalHashCode() => Convert.ToInt64(GetHashCode().ToString(), 2).ToString("X");

        public override int GetHashCode()
            =>   Convert.ToInt32(Rows
                .Reverse()
                .Aggregate("", (current, next) => current + binaryValue(next.Result).ToString())
                );
                
                
        
        private int binaryValue(bool? input) => (input != null && (bool) input) ? 1 : 0;
    }
}
