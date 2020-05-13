using System;
using System.Collections.Generic;
using LPP.Composite_Pattern.Node;
using LPP.NodeComponents;

namespace LPP.Modules
{
    public class TruthTable
    {
        public Row[] Rows;
        public int NumberOfVariables { get; }
        public PropositionalVariable[] Variables;
        public CompositeComponent RootOfBinaryTree { get; }

        public TruthTable(CompositeComponent rootOfBinaryTree)
        {
            Variables = rootOfBinaryTree.PropositionalVariables.GetPropositionalVariables();
            this.RootOfBinaryTree = rootOfBinaryTree;
            NumberOfVariables = Variables.Length;

            Rows = new Row[(int)Math.Pow(2, NumberOfVariables)];
        }
    }
}
