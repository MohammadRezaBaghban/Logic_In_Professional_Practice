using System;
using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP.Visitor_Pattern
{

    public class TableauxCalculator:IVisitor
    {
        private BinaryTree _binaryTree;

        public TableauxCalculator() => _binaryTree = new BinaryTree();

        public void Calculate(Component visitable)
        {
            if (!(visitable is SingleComponent))
            {
                CompositeComponent compositeNode = visitable as CompositeComponent;

                if (compositeNode is Negation)
                {
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
                if (visitable is Variable)
                    _binaryTree.PropositionalVariables.Variables.Add(visitable as Variable);
            }
        }

        public void Visit(BiImplication visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(Implication visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(Disjunction visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(Conjunction visitable)
        {//Alpha rule for &
            var rightNode = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var leftNode = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);

            leftNode.ParentFormula = visitable;
            rightNode.ParentFormula = visitable;

            visitable.nextStep = new List<Component>();
            visitable.nextStep.Add(leftNode);
            visitable.nextStep.Add(rightNode);
        }

        public void Visit(Negation visitable)
        {
            var mainConnective = visitable.LeftNode;

            if (mainConnective is Negation doubleNegation)
            {// Double Negation

                var leftNode = BinaryTree.CloneNode(doubleNegation.LeftNode, _binaryTree);
                leftNode.ParentFormula = visitable;
                visitable.nextStep = new List<Component>();
                visitable.nextStep.Add(leftNode);
            }
            else if (mainConnective is Disjunction disjunction)
            {// Alpha Rules for ~(|)

                var rightNode = new Negation();
                var leftNode = new Negation();
                _binaryTree.InsertNode(leftNode, BinaryTree.CloneNode(disjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(rightNode, BinaryTree.CloneNode(disjunction.RightNode, _binaryTree));

                rightNode.ParentFormula = visitable;
                leftNode.ParentFormula = visitable;

                visitable.nextStep = new List<Component>();
                visitable.nextStep.Add(leftNode);
                visitable.nextStep.Add(rightNode);

            }
            else if (mainConnective is Implication implication)
            {// Alpha Rules for ~(>)

                var rightNode = new Negation();
                var leftNode = BinaryTree.CloneNode(implication.LeftNode, _binaryTree);
                _binaryTree.InsertNode(rightNode, BinaryTree.CloneNode(implication.RightNode, _binaryTree));

                rightNode.ParentFormula = visitable;
                leftNode.ParentFormula = visitable;

                visitable.nextStep = new List<Component>();
                visitable.nextStep.Add(leftNode);
                visitable.nextStep.Add(rightNode);
            }
            else if (mainConnective is Conjunction conjunction)
            {

            }
            

        }

        public void Visit(Nand visitable)
        {
            throw new NotImplementedException();
        }
    }
}
