using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Visitor_Pattern;

namespace LPP.Modules
{
    public class TruthTable
    {
        public readonly Row[] Rows;
        public List<Row> SimplifiedRows;

        public int NumberOfVariables { get; }
        public CompositeComponent RootOfBinaryTree { get; }
        public PropositionalVariable[] DistinctPropositionalVariables;

        public TruthTable(CompositeComponent rootOfBinaryTree, Calculator calculator)
        {
            this.SimplifiedRows = new List<Row>();
            this.RootOfBinaryTree = rootOfBinaryTree;
            DistinctPropositionalVariables = rootOfBinaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables();
            NumberOfVariables = DistinctPropositionalVariables.Length;
            
            Rows = new Row[(int)Math.Pow(2, NumberOfVariables)];
            for (var i = 0; i < Rows.Length; i++)
            {
                Rows[i] = new Row(NumberOfVariables);
            }

            FillRows();
            calculator.Visit(this);
            SimplifyRows();
        }

        private int binaryValue(bool? input) => (input != null && (bool) input) ? 1 : 0;

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

        private void SimplifyRows()
        {
            var simplifiedTruthTable = new Simplification(this);
            SimplifiedRows = simplifiedTruthTable.RecursiveSimplification();
        }

        public void SetValue_Of_Propositional_Variables(PropositionalVariable variable, bool value)
        {
            RootOfBinaryTree
                .PropositionalVariables
                .SetValue_Of_Propositional_Variables(symbol: variable.Symbol, value: value);
        }

        public override string ToString()
        {
            var headOfTruthTable = DistinctPropositionalVariables.Aggregate(" ", (current, variable) => current + $"{variable.Symbol}  ") + " v";
            var rowsOfTruthTable = Rows.Aggregate("", (current, row) => current + $"\n{row}");
            return headOfTruthTable + rowsOfTruthTable;
        }

        public string SimplifiedToString()
        {
            var headOfTruthTable = DistinctPropositionalVariables.Aggregate(" ", (current, variable) => current + $"{variable.Symbol}  ") + " v";
            var rowsOfTruthTable = SimplifiedRows.Aggregate("", (current, row) => current + $"\n{row}");
            return headOfTruthTable + rowsOfTruthTable;
        }

        public string GetHexadecimalHashCode() => Convert.ToInt64(GetHashCode().ToString(), 2).ToString("X");

        public override int GetHashCode() =>   Convert.ToInt32(Rows.Reverse()
            .Aggregate("", (current, next) => current + binaryValue(next.Result).ToString()));



    }
}
