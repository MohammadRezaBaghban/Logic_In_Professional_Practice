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

                if (compositeNode is Negation)
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

        public void Visit(BiImplication visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);

        }

        public void Visit(Implication visitable)
        {
            var nandRoot = new Disjunction();
            var negation = new Negation();

            //binaryTree.InsertNode(negation,v)
        }

        public void Visit(Disjunction visitable)
        {
            var nandRoot = new Nand();
            var nandLeft = new Nand();
            var nandRight = new Nand();

            NandInsertNode(nandLeft,visitable.LeftNode);
            NandInsertNode(nandRight, visitable.RightNode);

            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, nandRight);

            binaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(Conjunction visitable)
        {
            var nandRoot = new Negation();
            var nandLeft = new Nand();

            binaryTree.InsertNode(nandLeft, BinaryTree.CloneNode(visitable.LeftNode,binaryTree));
            binaryTree.InsertNode(nandLeft, BinaryTree.CloneNode(visitable.RightNode, binaryTree));
            binaryTree.InsertNode(nandRoot, nandLeft);


            Calculate(nandRoot);

            binaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Negation visitable)
        {
            var nandRoot = new Nand();

            NandInsertNode(nandRoot, visitable.LeftNode);

            binaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(Nand visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);
            visitable.Nand = visitable;
        }

        private void NandInsertNode(Component root, Component branch)
        {
            if (branch is CompositeComponent composite)
            {
                binaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, binaryTree));
                binaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, binaryTree));
            }
            else
            {
                binaryTree.InsertNode(root, BinaryTree.CloneNode(branch, binaryTree));
                binaryTree.InsertNode(root, BinaryTree.CloneNode(branch, binaryTree));
            }
        }
    }
}
