using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
{
    public class Nandify:IVisitor
    {
        public static BinaryTree BinaryTree;
        public Nandify()
        {
            BinaryTree = new BinaryTree();
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
                    BinaryTree.PropositionalVariables.Variables.Add(visitable as Variable);
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
            
            BinaryTree.InsertNode(nandRight, visitable.LeftNode);
            BinaryTree.InsertNode(nandRight, visitable.RightNode);
            BinaryTree.InsertNode(nandLeft, nandLeftLeft);
            BinaryTree.InsertNode(nandLeft, nandLeftRight);
            BinaryTree.InsertNode(nandRoot, nandLeft);
            BinaryTree.InsertNode(nandRoot, nandRight);

            Calculate(nandRoot);

            BinaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Implication visitable)
        {
            var nandRoot = new Disjunction();
            var nandLeft = new Negation();

            InsertNodeSingle(nandLeft, visitable.LeftNode);
            BinaryTree.InsertNode(nandRoot, nandLeft);
            BinaryTree.InsertNode(nandRoot, visitable.RightNode);

            Calculate(nandRoot);

            BinaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Disjunction visitable)
        {
            var nandRoot = new Nand();
            var nandLeft = new Nand();
            var nandRight = new Nand();

            InsertNodeDouble(nandLeft,visitable.LeftNode);
            InsertNodeDouble(nandRight, visitable.RightNode);

            BinaryTree.InsertNode(nandRoot, nandLeft);
            BinaryTree.InsertNode(nandRoot, nandRight);

            BinaryTree.Root = nandRoot;
            visitable.Nand = nandRoot;
        }

        public void Visit(Conjunction visitable)
        {
            var nandRoot = new Negation();
            var nandLeft = new Nand();

            InsertNodeSingle(nandLeft, visitable.LeftNode);
            InsertNodeSingle(nandLeft, visitable.RightNode);
            BinaryTree.InsertNode(nandRoot, nandLeft);

            Calculate(nandRoot);

            BinaryTree.Root = nandRoot.Nand;
            visitable.Nand = nandRoot.Nand;
        }

        public void Visit(Negation visitable)
        {
            var nandRoot = new Nand();
            InsertNodeDouble(nandRoot, visitable.LeftNode);

            BinaryTree.Root = nandRoot;
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
        public void Visit(Predicate visitable) => throw new System.NotImplementedException();

        private void InsertNodeSingle(Component root, Component branch)
        {
            if (branch is CompositeComponent composite)
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, BinaryTree));
            else
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(branch, BinaryTree));
        }

        private void InsertNodeDouble(Component root, Component branch)
        {
            if (branch is CompositeComponent composite)
            {
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, BinaryTree));
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(composite.Nand, BinaryTree));
            }
            else
            {
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(branch, BinaryTree));
                BinaryTree.InsertNode(root, BinaryTree.CloneNode(branch, BinaryTree));
            }
        }
    }
}
