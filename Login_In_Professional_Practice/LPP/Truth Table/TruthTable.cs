using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;

namespace LPP.Modules
{
    public class TruthTable
    {
        public Row[] NormalRows;
        public List<Row> SimplifiedRows;
        public Component DNF_Normal;
        public Component DNF_Simplified;

        public int NumberOfVariables { get; }
        public CompositeComponent RootOfBinaryTree { get; }
        public PropositionalVariable[] DistinctPropositionalVariables;

        public TruthTable(CompositeComponent rootOfBinaryTree, Calculator calculator)
        {
            this.SimplifiedRows = new List<Row>();
            this.RootOfBinaryTree = rootOfBinaryTree;
            DistinctPropositionalVariables = rootOfBinaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables();
            NumberOfVariables = DistinctPropositionalVariables.Length;

            FillRows();
            SimplifyRows();
            ProcessDNF();
            calculator.Visit(this);
        }
        private int binaryValue(bool? input) => (input != null && (bool)input) ? 1 : 0;

        private void FillRows()
        {
            NormalRows = new Row[(int)Math.Pow(2, NumberOfVariables)];
            for (var i = 0; i < NormalRows.Length; i++)
            {
                NormalRows[i] = new Row(NumberOfVariables);
            }

            var columnIndex = 0;
            for (var i = NumberOfVariables; i > 0; i--)
            {
                var consequential01S = Math.Pow(2, i - 1);

                var oneOrZero = false;
                for (var j = 0; j < NormalRows.Length; j++)
                {
                    NormalRows[j].PropositionValues[columnIndex] = oneOrZero;
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

        public void ProcessDNF()
        {
            var variables = RootOfBinaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables_Chars();
            DNF_Normal = DNF.ProcessDNF(NormalRows.ToList(), variables);
            DNF_Simplified = DNF.ProcessDNF(SimplifiedRows, variables);
        }

        public override string ToString()
        {
            var headOfTruthTable = DistinctPropositionalVariables.Aggregate(" ", (current, variable) => current + $"{variable.Symbol}  ") + " v";
            var rowsOfTruthTable = NormalRows.Aggregate("", (current, row) => current + $"\n{row}");
            return headOfTruthTable + rowsOfTruthTable;
        }

        public string SimplifiedToString()
        {
            var headOfTruthTable = DistinctPropositionalVariables.Aggregate(" ", (current, variable) => current + $"{variable.Symbol}  ") + " v";
            var rowsOfTruthTable = SimplifiedRows.Aggregate("", (current, row) => current + $"\n{row}");
            return headOfTruthTable + rowsOfTruthTable;
        }

        public string GetHexadecimalHashCode() => Convert.ToInt64(GetHashCode().ToString(), 2).ToString("X");

        public override int GetHashCode() => Convert.ToInt32(NormalRows.Reverse().Aggregate("", (current, next) => current + binaryValue(next.Result).ToString()));
    }
}
