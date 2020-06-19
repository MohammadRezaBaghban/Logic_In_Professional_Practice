using System;
using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
{

    public class TableauxCalculator : IVisitor
    {
        private BinaryTree _binaryTree;
        public static TableauxCalculator Object { get; } = new TableauxCalculator();

        public TableauxCalculator() => _binaryTree = new BinaryTree();

        public void Calculate(Component visitable)
        {
            if (!(visitable is SingleComponent))
            {
                CompositeComponent compositeNode = visitable as CompositeComponent;
                compositeNode.Evaluate(this);
            }
        }

        public void Visit(BiImplication visitable)
        {
            var root = new Disjunction();
            var left = new Conjunction();
            var right = new Conjunction();
            var rightLeft = new Negation();
            var rightRight = new Negation();

            _binaryTree.InsertNode(left, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));
            _binaryTree.InsertNode(left, BinaryTree.CloneNode(visitable.RightNode, _binaryTree));
            _binaryTree.InsertNode(rightLeft, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));
            _binaryTree.InsertNode(rightRight, BinaryTree.CloneNode(visitable.RightNode, _binaryTree));
            _binaryTree.InsertNode(right, rightLeft);
            _binaryTree.InsertNode(right, rightRight);
            _binaryTree.InsertNode(root, left);
            _binaryTree.InsertNode(root, right);

            root.Parent = visitable.Parent;
        }

        public void Visit(Implication visitable)
        {//β-rule rule for >

            var tableauxRoot = visitable.Belongs;
            var _p = new Negation();
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            _binaryTree.InsertNode(_p, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));
            var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot);
        }

        public void Visit(Disjunction visitable)
        {// β-rule for | 
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var nodeLeft = new TableauxNode(p, visitable, tableauxRoot);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot);
        }

        public void Visit(Conjunction visitable)
        {// α-rule for &
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var newTableauxNode = new TableauxNode(new List<Component> { p, q }, visitable, tableauxRoot);
        }

        public void Visit(Negation visitable)
        {
            var mainConnective = visitable.LeftNode;
            var tableauxRoot = visitable.Belongs;

            if (mainConnective is Negation doubleNegation)
            {// Double Negation | Done

                var p = BinaryTree.CloneNode(doubleNegation.LeftNode, _binaryTree);
                var newTableauxNode = new TableauxNode(p, tableauxRoot);
            }
            else if (mainConnective is Disjunction disjunction)
            {// α-rule for ~(|) | Done

                var _p = new Negation();
                var _q = new Negation();
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(disjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(disjunction.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> { _p, _q },visitable, tableauxRoot);
            }
            else if (mainConnective is Implication implication)
            {// α-rule for ~(>) , elements | Done

                var _q = new Negation();
                var p = BinaryTree.CloneNode(implication.LeftNode, _binaryTree);
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(implication.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> { p, _q }, visitable, tableauxRoot);
            }
            else if (mainConnective is Conjunction conjunction)
            {// β-rule for ~(&)

                var _p = new Negation();
                var _q = new Negation();
                var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot);
                var nodeRight = new TableauxNode(_q, visitable, tableauxRoot);
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(conjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(conjunction.RightNode, _binaryTree));
            }
        }

        public void Visit(Nand visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(TableauxNode visitable)
        {
            if (visitable.Branched == null || !((bool)visitable.Branched))
            {
                int index = 0;
                do
                {
                    this.Calculate(visitable.Components[index++]);
                }
                while ((visitable.LeftNode == null && visitable.RightNode == null) &&
                       (index < visitable.Components.Count));

                if (visitable.LeftNode == null)
                {
                    visitable.Branched = false;
                    visitable.Closed = false;
                }
            }
        }
    }
}
