using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Variables;
using LPP.Modules;
using LPP.Visitor_Pattern;

namespace LPP.Truth_Table
{
    public class TruthTable
    {
        public Row[] NormalRows;
        public TruthTable DnfTruthTable;
        public List<Row> SimplifiedRows;
        public List<BinaryTree> DnfNormalComponents;
        public List<BinaryTree> DnfSimplifiedComponents;
        public BinaryTree DnfNormalBinaryTree;
        public BinaryTree DnfSimplifiedBinaryTree;

        public readonly BinaryTree BinaryTree;
        public int NumberOfVariables { get; }
        public CompositeComponent RootOfBinaryTree { get; }
        public readonly Variable[] DistinctPropositionalVariables;

        public TruthTable(BinaryTree binaryTree)
        {
            this.BinaryTree = binaryTree;
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

        private void SimplifyRows()
        {
            var simplifiedTruthTable = new Simplification(this);
            SimplifiedRows = simplifiedTruthTable.RecursiveSimplification();
        }

        public void SetValue_Of_Propositional_Variables(Variable variable, bool value)
        {
            BinaryTree.PropositionalVariables.SetValue_Of_Propositional_Variables(symbol: variable.Symbol, value: value);
        }

        public void ProcessDnf()
        {
            var variables = BinaryTree.PropositionalVariables.Get_Distinct_PropositionalVariables_Chars();
            DnfNormalComponents = Dnf.ProcessDnf(NormalRows.ToList(), variables);
            DnfSimplifiedComponents = Dnf.ProcessDnf(SimplifiedRows, variables);
            if (DnfNormalComponents.Count != 0)
            {
                DnfNormalBinaryTree = BinaryTree.DnfBinaryTree(DnfNormalComponents);
                DnfSimplifiedBinaryTree = BinaryTree.DnfBinaryTree(DnfSimplifiedComponents);
                DnfTruthTable = new TruthTable(DnfNormalBinaryTree);
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

        public string GetHexadecimalHashCode() => Convert.ToInt64(GetHashCode(), 2).ToString("X");

        public string GetHexadecimalSimplifiedHashCode() => Convert.ToInt64(GetHashCodeSimplified(), 2).ToString("X");

        public string GetHashCode() => NormalRows.Reverse().Aggregate("", (current, next) => current + binaryValue(next.Result).ToString());

        public string GetHashCodeSimplified() => SimplifiedRows.ToArray().Reverse().Aggregate("", (current, next) => current + binaryValue(next.Result).ToString());
    }
}
