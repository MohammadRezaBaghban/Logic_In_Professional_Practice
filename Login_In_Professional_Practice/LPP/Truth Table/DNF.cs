﻿using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;
using LPP.Modules;
using LPP.Visitor_Pattern;

namespace LPP.Truth_Table
{
    public static class Dnf
    {
        private static List<Row> _rows;
        private static char[] _variables;

        public static List<BinaryTree> ProcessDnf(List<Row> truthTableRows, char[] variables)
        {
            _rows = truthTableRows.Where(x => x.Result == true).ToList();
            _variables = variables;

            var components = new List<BinaryTree>();
            _rows.ForEach( row => components.Add(RowProcessing(row)));
            return components;
        }

        private static BinaryTree RowProcessing(Row row)
        {
            var bt = new BinaryTree();
            var root = bt.Root;

            if (row.PropositionValues.Count(x => x != null) > 1)
            {
                root = bt.InsertNode(root, new Conjunction());
                for (var i = 0; i < row.PropositionValues.Length; i++)
                {
                    var character = _variables[i];
                    if (row.PropositionValues[i] == null) continue;
                    if (row.PropositionValues[i].Value)
                    {
                        var variable = new Variable(character);
                        bt.PropositionalVariables.AddPropositionalVariable(variable);
                        bt.InsertNode(root, variable);
                    }
                    else
                    {
                        var variable = new Variable(character);
                        bt.PropositionalVariables.AddPropositionalVariable(variable);
                        var negation = new Negation { LeftNode = variable };
                        bt.InsertNode(root, negation);
                    }
                }
                return bt;
            }
            else
            {
                var index = Array.IndexOf(row.PropositionValues, row.PropositionValues.FirstOrDefault(x=>x != null && x.Value!=null));
                if (row.PropositionValues[index] != null)
                {
                    var character = _variables[index];
                    var variable = new Variable(character);
                    bt.PropositionalVariables.AddPropositionalVariable(variable);
                    bt.InsertNode(root, variable);
                    return bt;
                }
                return null;

            }
        }

        public static string DnfFormula(List<BinaryTree> components)
        {
            
            List<string> normalDNF = new List<string>();
            components.ForEach(x => {
                if (x == null) return;
                InfixFormulaGenerator.Calculator.Calculate(x.Root);
                normalDNF.Add(x.Root.InFixFormula);
            });
            return string.Join(" ⋁ ", normalDNF);
        }
    }
}
