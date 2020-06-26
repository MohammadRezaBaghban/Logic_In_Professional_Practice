using System;
using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
{

    public class Tableaux : IVisitor
    {
        private BinaryTree _binaryTree;
        private char[] _variables = new[]
        {'a','b', 'c', 'd', 'e', 'f', 'g', 'h',
        'i', 'g', 'k', 'l', 'm', 'n', 'o', 'p',
        'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        int _varIndex = 0; 
        public static Tableaux Object { get; } = new Tableaux();

        public Tableaux() => _binaryTree = new BinaryTree();

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
            var root = ConvertBiImplicationToDisjunction(visitable);
            root.Belongs = visitable.Belongs;
            visitable.Belongs.Components.Add(root);
            visitable.Belongs.Components.Remove(visitable);
            Calculate(root);
        }

        public void Visit(Implication visitable)
        {//β-rule rule for >

            var tableauxRoot = visitable.Belongs;
            var _p = new Negation();
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            _binaryTree.InsertNode(_p, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));
            var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot,true);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot, true);
        }

        public void Visit(Disjunction visitable)
        {// β-rule for | 
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var nodeLeft = new TableauxNode(p, visitable, tableauxRoot, true);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot, true);
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
                var nodeLeft = new TableauxNode(p, visitable, tableauxRoot,false);
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
                var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot, true);
                var nodeRight = new TableauxNode(_q, visitable, tableauxRoot, true);
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(conjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(conjunction.RightNode, _binaryTree));
            } 
            else if(mainConnective is Universal universal)
            {// δ(Delta)-rule for ∀
                var predicate = new Negation();
                var introducedVariable = _variables[_varIndex++];
                var leftNode = BinaryTree.CloneNode(universal.LeftNode, _binaryTree,
                    universal.ObjectVariables.Variables[0].Symbol, introducedVariable);
                _binaryTree.InsertNode(predicate, leftNode);
                var newTableauxNode = new TableauxNode(predicate, visitable, tableauxRoot, introducedVariable);
            }
            else if (mainConnective is BiImplication biImplication)
            {
                var converted = ConvertBiImplicationToDisjunction(biImplication);
                converted.Belongs = visitable.Belongs;
                converted.Parent = visitable;
                visitable.LeftNode = converted;
                Calculate(visitable);
            }else if (mainConnective is Nand nand)
            {
                var converted = ConvertNandToNegation(nand);
                converted.Belongs = visitable.Belongs;
                converted.Parent = visitable;
                visitable.LeftNode = converted;
                Calculate(visitable);
            }
        }

        public void Visit(Nand visitable)
        {
            var root = ConvertNandToNegation(visitable);
            root.Belongs = visitable.Belongs;
            visitable.Belongs.Components.Add(root);
            visitable.Belongs.Components.Remove(visitable);
            Calculate(root);
        }

        public void Visit(Universal visitable)
        { 
            
        }
        public void Visit(Existential visitable)
        {// δ(Delta)-rule for ∃
            var tableauxRoot = visitable.Belongs;
            var introducedVariable = _variables[_varIndex++];
            var predicate = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree,
                visitable.ObjectVariables.Variables[0].Symbol, introducedVariable);
            var newTableauxNode = new TableauxNode(predicate, visitable, tableauxRoot, introducedVariable);
        }
        public void Visit(Predicate visitable) => throw new NotImplementedException();

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
                    visitable.LeafIsClosed = false;
                }
            }
        }

        private Component ConvertBiImplicationToDisjunction(BiImplication visitable)
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

            return root;
        }

        private Component ConvertNandToNegation(Nand visitable)
        {
            var root = new Negation();
            var conjunction = new Conjunction();

            _binaryTree.InsertNode(conjunction, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));
            _binaryTree.InsertNode(conjunction, BinaryTree.CloneNode(visitable.RightNode, _binaryTree));
            _binaryTree.InsertNode(root, conjunction);

            return root;
        }
    }
}
