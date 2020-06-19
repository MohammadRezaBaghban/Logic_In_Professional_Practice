﻿using System.Collections.Generic;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Components
{
    public class TableauxNode
    {
        //Fields
        public List<Component> Components;
        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;
        public bool? Closed;

        //Properties
        public bool? Branched { get; set; }
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;


        //Constructors
        public TableauxNode(Component root, TableauxNode parent = null)
        {
            Components = new List<Component>();
            this.ParentNode = parent;
            this.Branched = false;
            root.Belongs = this;
            Components.Add(root);
            if (parent != null)
                parent.LeftNode ??= this;
        }
        public TableauxNode(List<Component> components, Component processed, TableauxNode parent)
        {
            this.Components = new List<Component>();
            this.ParentNode = parent;
            parent.Branched = false;
            parent.LeftNode = this;
            components.ForEach(x =>
            {
                var newNode = BinaryTree.CloneNode(x, BinaryTree.Object);
                newNode.Belongs = this;
                this.Components.Add(newNode);
            });
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    var newNode = BinaryTree.CloneNode(component, BinaryTree.Object);
                    newNode.Belongs = this;
                    this.Components.Add(newNode);
                }
            });
        }
        public TableauxNode(Component node, Component processed, TableauxNode parent)
        {
            this.Components = new List<Component>();
            node.Belongs = this;
            Components.Add(node);
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    var newNode = BinaryTree.CloneNode(component, BinaryTree.Object);
                    newNode.Belongs = this;
                    this.Components.Add(newNode);
                }
            });
            parent.Branched = true;
            this.ParentNode = parent;
            if (parent.LeftNode == null) parent.LeftNode = this;
            else if (parent.RightNode == null) parent.RightNode = this;

        }

        private void Evaluate()
        {
            if (Closed == null)
            {
                TableauxCalculator.Object.Visit(this);
                if (Branched == true)
                {
                    this.LeftNode.IsClosed();
                    this.RightNode.IsClosed();
                }
                else if (Branched == false && this.LeftNode != null)
                {
                    this.LeftNode.IsClosed();
                }
                IsClosed();
            }
        }

        public void IsClosed()
        {
            if (Closed != true)
            {
                this.Components.ForEach(x => InfixFormulaGenerator.Calculator.Calculate(x));
                if (this.LeftNode == null && this.RightNode == null)
                {// Node has not being Processed
                    if (Components.Count > 1)
                    {
                        if (Branched == false && LeftNode == null)
                        {
                            this.Closed = false;
                        }
                        else
                        {
                            for (var i = 0; i < Components.Count && this.Closed != true; i++)
                            {
                                for (var j = i + 1; j < Components.Count; j++)
                                {
                                    var node1Formula = Components[i].InFixFormula;
                                    var node2Formula = Components[j].InFixFormula;

                                    if (Components[j] is SingleComponent)
                                    {
                                        node2Formula = Components[j] is SingleComponent ? $"¬{node2Formula}" : $"¬({node2Formula})";
                                        if (node1Formula != node2Formula) continue;
                                        this.Closed = true;
                                        break;
                                    } else if (Components[i] is SingleComponent)
                                    {
                                        node1Formula = Components[i] is SingleComponent ? $"¬{node1Formula}" : $"¬({node1Formula})";
                                        if (node1Formula != node2Formula) continue;
                                        this.Closed = true;
                                        break;
                                    }
                                    
                                }
                            }
                            if (this.Closed != true)
                            {
                                if (!Components.Exists(x => x is CompositeComponent))
                                {
                                    this.Closed = false;
                                }
                                else
                                {
                                    this.Evaluate();
                                }
                            }
                        }
                    }
                    else
                    {//Root of Tableaux Node
                        this.Evaluate();
                    }
                }
                else
                {//Node Has already Processed and simplified
                    if (Branched == false)
                    {
                        if (this.LeftNode.Closed == true)
                            this.Closed = true;
                        else if (this.LeftNode.Closed == false)
                            this.Closed = false;
                    }
                    else
                    {
                        if (this.LeftNode.Closed == true && this.RightNode.Closed == true)
                            this.Closed = true;
                        else if (this.LeftNode.Closed == false || this.RightNode.Closed == false)
                            this.Closed = false;
                        else
                        {
                            this.LeftNode.Evaluate();
                            this.RightNode.Evaluate();
                        }
                    }
                }
            }
            
        }

        public string Label()
        {
            var label = "";
            Components.ForEach(x => { InfixFormulaGenerator.Calculator.Calculate(x); label += x.InFixFormula + ", "; });

            if (this.Closed == true)
                label += "\n\n CLOSED";
            else if (this.Closed == false)
                label += "\n\n OPENED";

            return label;
        }
        public string GraphVizFormula()
        {
            string temp = "";
            temp += $"node{NodeNumber} [ label = \"{this.Label()}\" ]";

            if (LeftNode != null)
            {
                temp += $"\nnode{NodeNumber} -- node{LeftNode.NodeNumber}\n";
                temp += LeftNode.GraphVizFormula();
            }
            if (RightNode != null)
            {
                temp += $"\nnode{NodeNumber} -- node{RightNode.NodeNumber}\n";
                temp += RightNode.GraphVizFormula();
            }
            return temp;
        }
    }
}
