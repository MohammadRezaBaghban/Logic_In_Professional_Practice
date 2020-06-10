using System.Xml;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;

namespace LPP.Visitor_Pattern
{
    public class Nandify:IVisitor
    {
        private BinaryTree binaryTree;

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
                    Calculate(compositeNode.RightNode);
                    Calculate(compositeNode.LeftNode);
                    compositeNode.Evaluate(this);
                }
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
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);

            var nandRoot = new NANDConnective();
            var nandLeft = new NANDConnective();
            var nandRight = new NANDConnective();

            binaryTree.InsertNode(nandLeft, visitable.LeftNode);
            binaryTree.InsertNode(nandLeft, visitable.LeftNode);
            binaryTree.InsertNode(nandRight, visitable.RightNode);
            binaryTree.InsertNode(nandRight, visitable.RightNode);
            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, nandRight);

            visitable.Nand = nandRoot;
            //BinaryTree.ChangeNode(visitable,nandRoot);
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
        }
    }
}
