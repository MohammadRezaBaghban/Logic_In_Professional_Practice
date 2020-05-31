using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Modules;
using LPP.Visitor_Pattern;

namespace LPP.Truth_Table
{
    public static class DNF
    {
        private static List<Row> _rows;
        private static char[] _variables;

        public static List<Component> ProcessDNF(List<Row> truthTableRows, char[] variables)
        {
            _rows = truthTableRows.Where(x => x.Result == true).ToList();
            DNF._variables = variables;

            List<Component> components = new List<Component>();
            _rows.ForEach( row => components.Add(RowProcessing(row)));
            return components;
        }

        private static Component RowProcessing(Row row)
        {
            var bt = new BinaryTree();
            var root = bt._root;

            if (row.PropositionValues.Count(x => x != null) > 1)
            {
                root = bt.InsertNode(root, new ConjunctionConnective());
                for (var i = 0; i < row.PropositionValues.Length; i++)
                {
                    var character = _variables[i];
                    if (row.PropositionValues[i] == null) continue;
                    if (row.PropositionValues[i].Value)
                    {
                        bt.InsertNode(root, new PropositionalVariable(character));
                    }
                    else
                    {
                        var negation = new NegationConnective { LeftNode = new PropositionalVariable(character) };
                        bt.InsertNode(root, negation);
                    }
                }
                return bt._root;
            }
            else
            {
                var index = Array.IndexOf(row.PropositionValues, row.PropositionValues.First(x=>x != null && x.Value!=null));
                var character = _variables[index];
                return new PropositionalVariable(character);
            }
        }

        public static string DNFFormula(List<Component> components)
        {
            InfixFormulaGenerator formulaGenerator = new InfixFormulaGenerator();
            List<string> normalDNF = new List<string>();
            components.ForEach(x => {
                formulaGenerator.Calculate(x);
                normalDNF.Add(x.InFixFormula);
            });

            return string.Join(" ⋁ ", normalDNF);
        }
    }
}
