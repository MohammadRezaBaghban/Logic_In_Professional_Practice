using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;

namespace LPP.Visitor_Pattern
{
    public class Calculator : IVisitor
    {
        public void Calculate(Component visitable)
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

        public void Visit(SingleComponent visitable) => visitable.Data = visitable.Data;
    }
}
