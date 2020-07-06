using System;
using System.Collections.Generic;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;
using static LPP.Composite_Pattern.Components.TableauxNode;

namespace LPP.Visitor_Pattern
{

    public class Tableaux : IVisitor
    {
        private readonly BinaryTree _binaryTree;
        private readonly char[] _variables = {'a','b', 'c', 'd', 'e', 'f', 'g', 'h',
        'i', 'g', 'k', 'l', 'm', 'n', 'o', 'p',
        'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        ,'1','2','3','4','5','6','7','8','9'};//Sometimes I need more variable than alphabet

        public static int VarIndex;
        public static Tableaux Object { get; } = new Tableaux();

        public Tableaux() => _binaryTree = new BinaryTree();

        public void Calculate(Component visitable)
        {
            if (visitable is SingleComponent) return;
            CompositeComponent compositeNode = visitable as CompositeComponent;
            compositeNode?.Evaluate(this);
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
            var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot, RuleType.RuleBeta);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot, RuleType.RuleBeta);
        }

        public void Visit(Disjunction visitable)
        {// β-rule for | 
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var nodeLeft = new TableauxNode(p, visitable, tableauxRoot, RuleType.RuleBeta);
            var nodeRight = new TableauxNode(q, visitable, tableauxRoot, RuleType.RuleBeta);
        }

        public void Visit(Conjunction visitable)
        {// α-rule for &
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var newTableauxNode = new TableauxNode(new List<Component> { p, q }, visitable, tableauxRoot,RuleType.RuleAlpha);
        }

        public void Visit(Negation visitable)
        {
            var mainConnective = visitable.LeftNode;
            var tableauxRoot = visitable.Belongs;

            if (mainConnective is Negation doubleNegation)
            {// Double Negation | Omega

                var p = BinaryTree.CloneNode(doubleNegation.LeftNode, _binaryTree);
                var nodeLeft = new TableauxNode(p, visitable, tableauxRoot, RuleType.RuleOmega);
            }
            else if (mainConnective is Disjunction disjunction)
            {// α-rule for ~(|) | Done

                var _p = new Negation();
                var _q = new Negation();
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(disjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(disjunction.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> { _p, _q }, visitable, tableauxRoot, RuleType.RuleAlpha);
            }
            else if (mainConnective is Implication implication)
            {// α-rule for ~(>) , elements | Done
                var _q = new Negation();
                var p = BinaryTree.CloneNode(implication.LeftNode, _binaryTree);
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(implication.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> { p, _q }, visitable, tableauxRoot, RuleType.RuleAlpha);
            }
            else if (mainConnective is Conjunction conjunction)
            {// β-rule for ~(&)
                var _p = new Negation();
                var _q = new Negation();
                var nodeLeft = new TableauxNode(_p, visitable, tableauxRoot, RuleType.RuleBeta);
                var nodeRight = new TableauxNode(_q, visitable, tableauxRoot, RuleType.RuleBeta);
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(conjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(conjunction.RightNode, _binaryTree));
            }
            else if (mainConnective is Universal universal)
            {// δ(Delta)-rule for @
                var predicate = new Negation();
                var introducedVariable = _variables[VarIndex++];
                var leftNode = BinaryTree.CloneNode(universal.LeftNode, _binaryTree,
                    current: universal.ObjectVariables.Variables[0].Symbol, rename: introducedVariable);
                _binaryTree.InsertNode(predicate, leftNode);
                var newTableauxNode = new TableauxNode(predicate, visitable, tableauxRoot, introducedVariable);
            }
            else if(mainConnective is Existential existential)
            {
                //γ(Gama)-rule for !
                if (tableauxRoot.ActiveVariables != null && tableauxRoot.ActiveVariables.Count > 0 && visitable.GammaProcessed == false)
                {
                    List<Component> components = new List<Component>();
                    Negation itself = (Negation)BinaryTree.CloneNode(visitable, BinaryTree.Object);
                    components.Add(itself);
                    tableauxRoot.ActiveVariables.ForEach(x =>
                    {
                        var negation = new Negation();
                        _binaryTree.InsertNode(negation, BinaryTree.CloneNode(existential.LeftNode, _binaryTree,
                            current: existential.ObjectVariables.Variables[0].Symbol, rename: x));
                        components.Add(negation);
                    });
                    itself.GammaProcessed = true;
                    var newTableauxNode = new TableauxNode(components, visitable, tableauxRoot, RuleType.RuleGamma);
                }
            }
            else if (mainConnective is BiImplication biImplication)
            {
                var converted = ConvertBiImplicationToDisjunction(biImplication);
                converted.Belongs = visitable.Belongs;
                converted.Parent = visitable;
                visitable.LeftNode = converted;
                Calculate(visitable);
            }
            else if (mainConnective is Nand nand)
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
            //γ(Gama)-rule for @
            var tableauxRoot = visitable.Belongs;
            if (tableauxRoot.ActiveVariables != null && tableauxRoot.ActiveVariables.Count > 0 && visitable.GammaProcessed == false)
            {
                List<Component> components = new List<Component>();
                tableauxRoot.ActiveVariables.ForEach(x =>
                {
                    components.Add(BinaryTree.CloneNode(visitable.LeftNode, _binaryTree,
                        current: visitable.ObjectVariables.Variables[0].Symbol, rename: x));
                });
                Universal itself = (Universal) BinaryTree.CloneNode(visitable, BinaryTree.Object);
                itself.GammaProcessed = true;
                components.Add(itself);
                var newTableauxNode = new TableauxNode(components, visitable, tableauxRoot,RuleType.RuleGamma);
            }
        }
        public void Visit(Existential visitable)
        {// δ(Delta)-rule for ∃
            var tableauxRoot = visitable.Belongs;
            var introducedVariable = _variables[VarIndex++];
            var predicate = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree,
                current: visitable.ObjectVariables.Variables[0].Symbol, rename: introducedVariable);
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
