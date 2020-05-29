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
        private DisjunctionConnective normaldnfRoot;
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

        private CompositeComponent RowProcessing(Row row)
        {
            BinaryTree bt = new BinaryTree();
            CompositeComponent root = bt._root;

            bt.InsertNode(root, new ConjunctionConnective());

            for(int i = 0; i < row.PropositionValues.Length; i++)
            {
                var character = variables[i];
                if (row.PropositionValues[i] != null)
                {
                    if (row.PropositionValues[i].Value)
                    {
                        bt.InsertNode(root, new PropositionalVariable(character));
                    }
                    else
                    {
                        var negation = new NegationConnective();
                        negation.LeftNode = new PropositionalVariable(character);
                        bt.InsertNode(root, negation);
                    }
                }
            }
            return bt._root;
        }
    }
}
