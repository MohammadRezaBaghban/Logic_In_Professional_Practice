using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Modules;

namespace LPP.Truth_Table
{
    public class DNF
    {
        private Component dnfRoot;
        private DisjunctionConnective simplifiedDnfRoot;

        private List<Row> normalRows;
        private List<Row> simplifiedRows;
        private char[] variables;

        public DNF(TruthTable truthTable)
        {
            normalRows = truthTable.Rows.Where(x => x.Result == true).ToList();
            simplifiedRows = truthTable.SimplifiedRows.Where(x => x.Result == true).ToList();
            variables = truthTable.RootOfBinaryTree.PropositionalVariables
                        .Get_Distinct_PropositionalVariables().OrderBy(x=>x.Symbol)
                        .Select(x => x.Symbol).ToArray();
        }

        private Component RowProcessing(Row row)
        {
            var bt = new BinaryTree();
            var root = bt._root;

            if (row.PropositionValues.Count(x => x != null) > 1)
            {
                bt.InsertNode(root, new ConjunctionConnective());
                for (var i = 0; i < row.PropositionValues.Length; i++)
                {
                    var character = variables[i];
                    if (row.PropositionValues[i] == null) continue;
                    if (row.PropositionValues[i].Value)
                    {
                        bt.InsertNode(root, new PropositionalVariable(character));
                    }
                    else
                    {
                        var negation = new NegationConnective {LeftNode = new PropositionalVariable(character)};
                        bt.InsertNode(root, negation);
                    }
                }
                return bt._root;
            }
            else
            {
                var index = Array.IndexOf(row.PropositionValues, null);
                var character = variables[index];
                return new PropositionalVariable(character);
            }
        }

        private Component TruthTableDNFProcessing(List<CompositeComponent> nodes)
        {
            if (nodes.Count == 1)
            {
                dnfRoot = nodes[0];
                return dnfRoot;
            }
            else
            {
                foreach (var node in nodes)
                {
                    InsertForDNF(node);
                }
                return dnfRoot;
            }
        }

        private void InsertForDNF(CompositeComponent node)
        {
            if (dnfRoot == null)
            {
                dnfRoot = new DisjunctionConnective();
            }

            if(dnfRoot.LeftNode ==null && dnfRoot.RightNode == null)
            {
                var newRoot = new DisjunctionConnective();
                newRoot.LeftNode = dnfRoot;
                dnfRoot = newRoot;
                InsertForDNF(node);
            }
            else
            {
                if (dnfRoot.RightNode == null)
                {
                    dnfRoot.RightNode = node;
                }
                else
                {
                    dnfRoot.LeftNode = node;
                }
            }
        }
    }
}
