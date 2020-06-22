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
            var nandRoot = new Disjunction();
            var nandLeft = new Conjunction();
            var nandRight = new Conjunction();
            var nandLeftLeft = new Negation();
            var nandLeftRight = new Negation();

            InsertNodeSingle(nandLeftLeft, visitable.LeftNode);
            InsertNodeSingle(nandLeftRight, visitable.RightNode);
            
            binaryTree.InsertNode(nandRight, visitable.LeftNode);
            binaryTree.InsertNode(nandRight, visitable.RightNode);
            binaryTree.InsertNode(nandLeft, nandLeftLeft);
            binaryTree.InsertNode(nandLeft, nandLeftRight);
            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, nandRight);

            Calculate(nandRoot);

            binaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Implication visitable)
        {
            var nandRoot = new Disjunction();
            var nandLeft = new Negation();

            InsertNodeSingle(nandLeft, visitable.LeftNode);
            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, visitable.RightNode);

            Calculate(nandRoot);

            binaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;

        }

        public void Visit(Disjunction visitable)
        {
            var nandRoot = new Nand();
            var nandLeft = new Nand();
            var nandRight = new Nand();

            InsertNodeDouble(nandLeft,visitable.LeftNode);
            InsertNodeDouble(nandRight, visitable.RightNode);

            binaryTree.InsertNode(nandRoot, nandLeft);
            binaryTree.InsertNode(nandRoot, nandRight);

            binaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(Conjunction visitable)
        {
            var nandRoot = new Negation();
            var nandLeft = new Nand();

            InsertNodeSingle(nandLeft, visitable.LeftNode);
            InsertNodeSingle(nandLeft, visitable.RightNode);
            binaryTree.InsertNode(nandRoot, nandLeft);

            Calculate(nandRoot);

            binaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Negation visitable)
        {
            var nandRoot = new Nand();

            InsertNodeDouble(nandRoot, visitable.LeftNode);

            binaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(Nand visitable)
        {
            Calculate(visitable.LeftNode);
            Calculate(visitable.RightNode);
            visitable.Nand = visitable;
        }

        public void Visit(Universal visitable) => throw new System.NotImplementedException();

        public void Visit(Existential visitable) => throw new System.NotImplementedException();

        private void InsertNodeSingle(Component root, Component branch)
        {
            if (branch is CompositeComponent composite)
            {
                binaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, binaryTree));
            }
            else
            {
                binaryTree.InsertNode(root, BinaryTree.CloneNode(branch, binaryTree));
            }
        }

        private void InsertNodeDouble(Component root, Component branch)
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

        public void Visit(TableauxNode visitable) => throw new System.NotImplementedException();
    }
}
