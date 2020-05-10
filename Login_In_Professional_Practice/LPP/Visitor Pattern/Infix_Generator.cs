using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;
using IVisitor = LPP.Composite_Pattern.IVisitor;

namespace LPP.Visitor_Pattern
{
    public class Infix_Generator:IVisitor
    {
        public void Calculate(Component visitable)
        {
            CompositeComponent compositeNode = visitable as CompositeComponent;
            
            if (compositeNode is NegationConnective)
            {
                Calculate(compositeNode.LeftNode);
                compositeNode.Evaluate(this);
            }
            else
            {
                Calculate(compositeNode.RightNode);
                Calculate(compositeNode.LeftNode);
                compositeNode.Evaluate(this);
            }
        }

        public void Visit(NegationConnective visitable)
        {
            if(visitable.LeftNode is CompositeComponent)
            {
                visitable.InFixFormula = $"¬({visitable.LeftNode.InFixFormula})";
            }
            if(visitable.LeftNode is SingleComponent)
            {
                visitable.InFixFormula = $"¬{visitable.LeftNode.InFixFormula}";
            }

        }

        public void Visit(ImplicationConnective visitable)
        {
            
        }

        public void Visit(Bi_ImplicationConnective visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(ConjuctionConnective visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(DisjunctionConnective visitable)
        {
            throw new NotImplementedException();
        }
    }
}
