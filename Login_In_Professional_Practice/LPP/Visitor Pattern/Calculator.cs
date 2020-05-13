using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.Modules;
using LPP.NodeComponents;

namespace LPP.Visitor_Pattern
{
    public class Calculator : IVisitor
    {
        public void Calculate(Component visitable)
        {
            if (!(visitable is SingleComponent))
            {
                CompositeComponent compositeNode = visitable as CompositeComponent;

                if (compositeNode is NegationConnective)
                {
                    Calculate(compositeNode.LeftNode);
                    compositeNode.Evaluate(this);
                }
                else if (compositeNode != null)
                {
                    Calculate(compositeNode.RightNode);
                    Calculate(compositeNode.LeftNode);
                    compositeNode.Evaluate(this);
                }
            }
        }

        public void Visit(Bi_ImplicationConnective visitable) => visitable.Data =
            (visitable.LeftNode.Data && visitable.RightNode.Data) ||
            (!visitable.LeftNode.Data && !visitable.RightNode.Data);


        public void Visit(ImplicationConnective visitable) =>
            visitable.Data = !(visitable.LeftNode.Data && !visitable.RightNode.Data);

        public void Visit(DisjunctionConnective visitable) =>
            visitable.Data = visitable.LeftNode.Data || visitable.RightNode.Data;

        public void Visit(ConjuctionConnective visitable) =>
            visitable.Data = visitable.LeftNode.Data && visitable.RightNode.Data;

        public void Visit(NegationConnective visitable) => visitable.Data = !visitable.LeftNode.Data;

        public void Visit(TruthTable truthTable)
        {
            //Traverse Each Row
            foreach (var currentRow in truthTable.Rows)
            {
                for (int j = 0; j < currentRow.PropositionValues.Length; j++)
                {
                    truthTable.Variables[j].Data = currentRow.PropositionValues[j];
                }
                this.Calculate(truthTable.RootOfBinaryTree);
                currentRow.SetValue(truthTable.RootOfBinaryTree.Data);
            }
        }
    }
}
