using System.Xml;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
{
    public class Nandify:IVisitor
    {
        public static BinaryTree binaryTree;

        public Nandify()
        {
            binaryTree = new BinaryTree();
        }

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
                    Calculate(compositeNode.LeftNode);
                    Calculate(compositeNode.RightNode);
                    compositeNode.Evaluate(this);
                }
            }
            else
            {
                if(visitable is Variable)
                    binaryTree.PropositionalVariables.Variables.Add(visitable as Variable);
            }
        }

        public void Visit(Bi_ImplicationConnective visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);

        }

        public void Visit(ImplicationConnective visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);
        }

        public void Visit(DisjunctionConnective visitable)
        {
            var nandRoot = new NANDConnective();
            var nandLeft = new NANDConnective();
            var nandRight = new NANDConnective();

            binaryTree.InsertNode(nandLeft, BinaryTree.CloneNode(visitable.LeftNode));
            binaryTree.InsertNode(nandLeft, BinaryTree.CloneNode(visitable.LeftNode));
            binaryTree.InsertNode(nandRight, BinaryTree.CloneNode(visitable.RightNode));
            binaryTree.InsertNode(nandRight, BinaryTree.CloneNode(visitable.RightNode));
            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, nandRight);

            binaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(ConjunctionConnective visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);
        }

        public void Visit(NegationConnective visitable)
        {
            Calculate(visitable.LeftNode);
        }

        public void Visit(NANDConnective visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);

            visitable.LeftNode = ((CompositeComponent) visitable.LeftNode).Nand;
            visitable.RightNode = ((CompositeComponent)visitable.RightNode).Nand;

        }
    }
}
