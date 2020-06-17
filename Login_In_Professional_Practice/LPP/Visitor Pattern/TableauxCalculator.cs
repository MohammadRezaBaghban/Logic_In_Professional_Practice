﻿using System;
using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;
using LPP.Parsing_BinaryTree;

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
        {//Beta rule for >
            //var leftNode = new Negation();
            //var rightNode = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            //_binaryTree.InsertNode(leftNode, BinaryTree.CloneNode(visitable.LeftNode, _binaryTree));

            //rightNode.ParentFormula = visitable;
            //leftNode.ParentFormula = visitable;

            //visitable.LeftFormula = leftNode;
            //visitable.Rightformula = rightNode;
        }

        public void Visit(Disjunction visitable)
        {//Beta rule for | 
            //var rightNode = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            //var leftNode = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);

            //leftNode.ParentFormula = visitable;
            //rightNode.ParentFormula = visitable;

            //visitable.LeftFormula = leftNode;
            //visitable.Rightformula = rightNode;
        }

        public void Visit(Conjunction visitable)
        {//Alpha rule for &
            var tableauxRoot = visitable.Belongs;
            var p = BinaryTree.CloneNode(visitable.LeftNode, _binaryTree);
            var q = BinaryTree.CloneNode(visitable.RightNode, _binaryTree);
            var newTableauxNode = new TableauxNode(new List<Component> { p, q }, tableauxRoot);

            visitable.Belongs.SetBranchStatus(false);
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
            {// Alpha Rules for ~(|) | Done

                var _p = new Negation();
                var _q = new Negation();
                _binaryTree.InsertNode(_p, BinaryTree.CloneNode(disjunction.LeftNode, _binaryTree));
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(disjunction.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> { _p, _q }, tableauxRoot);

                visitable.Belongs.SetBranchStatus(false);
            }
            else if (mainConnective is Implication implication)
            {// Alpha Rules for ~(>) , elements | Done

                var _q = new Negation();
                var p = BinaryTree.CloneNode(implication.LeftNode, _binaryTree);
                _binaryTree.InsertNode(_q, BinaryTree.CloneNode(implication.RightNode, _binaryTree));
                var newTableauxNode = new TableauxNode(new List<Component> {p,_q}, tableauxRoot);

                tableauxRoot.SetBranchStatus(false);
            }
            else if (mainConnective is Conjunction conjunction)
            {//Beta rule for ~(&)

                //var rightNode = new Negation();
                //var leftNode = new Negation();
                //_binaryTree.InsertNode(leftNode, BinaryTree.CloneNode(conjunction.LeftNode, _binaryTree));
                //_binaryTree.InsertNode(rightNode, BinaryTree.CloneNode(conjunction.RightNode, _binaryTree));

                //rightNode.ParentFormula = visitable;
                //leftNode.ParentFormula = visitable;

                //visitable.LeftFormula = leftNode;
                //visitable.Rightformula = rightNode;
            }
        }

        public void Visit(Nand visitable)
        {
            throw new NotImplementedException();
        }

        public void Visit(TableauxNode visitable)
        {
            if (visitable.branched == null || !((bool) visitable.branched))
            {
                bool formulaFound = false;
                int index = 0;
                do
                { 
                    this.Calculate(visitable.components[index++]);
                }
                while (visitable.LeftNode!=null && visitable.RightNode!=null);
            }
            else
            {

            }
        }
    }
}