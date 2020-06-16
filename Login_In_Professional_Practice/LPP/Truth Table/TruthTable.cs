using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Variables;
using LPP.Truth_Table;
using LPP.Visitor_Pattern;

namespace LPP.Modules
{
    public class TruthTable
    {
        public Row[] NormalRows;
        public TruthTable DnftTruthTable;
        public List<Row> SimplifiedRows;
        public List<BinaryTree> DNF_Normal_Components;
        public List<BinaryTree> DNF_Simplified_Components;
        public BinaryTree DNF_Normal_BinaryTree;
        public BinaryTree DNF_Simplified_BinaryTree;

        public BinaryTree binaryTree;
        public int NumberOfVariables { get; }
        public CompositeComponent RootOfBinaryTree { get; }
        public Variable[] DistinctPropositionalVariables;

        public TruthTable(BinaryTree binaryTree)
        {
            this.binaryTree = binaryTree;
            this.SimplifiedRows = new List<Row>();
            this.RootOfBinaryTree = binaryTree.Root as CompositeComponent;
            DistinctPropositionalVariables = binaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables();
            NumberOfVariables = DistinctPropositionalVariables.Length;
            FillAndCalculateRows();
            SimplifyRows();
        }

        private int binaryValue(bool? input) => (input != null && (bool)input) ? 1 : 0;

        private void FillAndCalculateRows()
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
            var calculator = new Calculator();
            calculator.Visit(this);
        }

        public void SimplifyRows()
        {
            var simplifiedTruthTable = new Simplification(this);
            SimplifiedRows = simplifiedTruthTable.RecursiveSimplification();
        }

        public void SetValue_Of_Propositional_Variables(Variable variable, bool value)
        {
            binaryTree.PropositionalVariables.SetValue_Of_Propositional_Variables(symbol: variable.Symbol, value: value);
        }

        public void ProcessDNF()
        {
            var variables = binaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables_Chars();
            DNF_Normal_Components = DNF.ProcessDNF(NormalRows.ToList(), variables);
            DNF_Simplified_Components = DNF.ProcessDNF(SimplifiedRows, variables);
            if (DNF_Normal_Components.Count != 0)
            {
                DNF_Normal_BinaryTree = BinaryTree.DNFBinaryTree(DNF_Normal_Components);
                DNF_Simplified_BinaryTree = BinaryTree.DNFBinaryTree(DNF_Simplified_Components);
                DnftTruthTable = new TruthTable(DNF_Normal_BinaryTree);
            }
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

        public string GetHexadecimalSimplifiedHashCode() => Convert.ToInt64(GetHashCodeSimplified().ToString(), 2).ToString("X");

        public string GetHashCode() => NormalRows.Reverse().Aggregate("", (current, next) => current + binaryValue(next.Result).ToString());

        public string GetHashCodeSimplified() => SimplifiedRows.ToArray().Reverse().Aggregate("", (current, next) => current + binaryValue(next.Result).ToString());
    }
}
