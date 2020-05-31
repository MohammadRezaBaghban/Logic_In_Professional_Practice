using System;
using System.Collections.Generic;
using System.Linq;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Modules;

namespace LPP.Truth_Table
{
    public static class DNF
    {
        private static Component _dnfRoot;
        private static List<Row> _rows;
        private static char[] _variables;

        public static Component ProcessDNF(List<Row> truthTableRows, char[] variables)
        {
            _rows = truthTableRows.Where(x => x.Result == true).ToList();
            DNF._variables = variables;

            List<Component> components = new List<Component>();
            _rows.ForEach( row => components.Add(RowProcessing(row)));
            _dnfRoot = TruthTableDnfProcessing(components);
            return _dnfRoot;
        }

        private static Component RowProcessing(Row row)
        {
            var bt = new BinaryTree();
            var root = bt._root;

            if (row.PropositionValues.Count(x => x != null) > 1)
            {
                bt.InsertNode(root, new ConjunctionConnective());
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
                var index = Array.IndexOf(row.PropositionValues, null);
                var character = _variables[index];
                return new PropositionalVariable(character);
            }
        }

        private static Component TruthTableDnfProcessing(List<Component> nodes)
        {

            if (nodes.Count == 1)
            { 
                _dnfRoot = nodes[0];
                return _dnfRoot;
            }
            else
            {
                if (_dnfRoot == null)
                {
                    _dnfRoot = new DisjunctionConnective();
                }

                foreach (var node in nodes)
                {
                    if (_dnfRoot.LeftNode == null && _dnfRoot.RightNode == null)
                    {
                        var newRoot = new DisjunctionConnective();
                        newRoot.LeftNode = _dnfRoot;
                        _dnfRoot = newRoot;
                        _dnfRoot.RightNode = node;
                    }
                    else
                    {
                        if (_dnfRoot.RightNode == null)
                        {
                            _dnfRoot.RightNode = node;
                        }
                        else
                        {
                            _dnfRoot.LeftNode = node;
                        }
                    }
                }
                return _dnfRoot;
            }
        }
    }
}
