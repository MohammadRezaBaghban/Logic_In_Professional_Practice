using System.Collections.Generic;
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
        public bool? Branched { get; private set; }
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
        public TableauxNode(List<Component> components, TableauxNode parent)
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
        }
        public TableauxNode(Component node, Component processed, TableauxNode parent)
        {
            this.Components = new List<Component>();
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
                else
                {
                    this.LeftNode.IsClosed();
                }

                IsClosed();
            }
        }

        public void IsClosed()
        {
            this.Components.ForEach(x => InfixFormulaGenerator.Calculator.Calculate(x));

            if (this.LeftNode == null && this.RightNode==null)
            {// Is not being Processed
                if (Components.Count > 1)
                {
                    for (int i = 0; i < Components.Count && this.Closed != true; i++)
                    {
                        for (int j = i + 1; j < Components.Count; j++)
                        {
                            if (Components[i] is SingleComponent)
                            {
                                var node1Formula = Components[i].InFixFormula;
                                var node2Formula = Components[j].InFixFormula;
                                if ($"¬{node1Formula}" == node2Formula)
                                {
                                    this.Closed = true;
                                    break;
                                }
                            }
                            else
                            {
                                var node1Formula = Components[i].InFixFormula;
                                var node2Formula = Components[j].InFixFormula;
                                if ($"¬({node1Formula})" == node2Formula)
                                {
                                    this.Closed = true;
                                    break;
                                }
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
                else
                {
                    this.Evaluate();
                }
            }
            else
            {//Already Processed and simplified
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

        public string Label()
        {
            string label = "";
            Components.ForEach(x =>
            {
                InfixFormulaGenerator.Calculator.Calculate(x);
                label += x.InFixFormula + ", ";
            });

            if (this.Closed == true)
                label += "\n\n CLOSED";
            else if (this.Closed == false)
                label += "\n\n OPENED";

            return label;
        }
        public string GraphVizFormula()
        {
            string temp ="";
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
