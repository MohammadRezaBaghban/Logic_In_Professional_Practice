using System.Collections.Generic;
using System.Linq;
using LPP.Visitor_Pattern;

namespace LPP.Composite_Pattern.Components
{
    public class TableauxNode
    {
        //Fields
        public enum RuleType { RuleAlpha, RuleBeta, RuleDelta, RuleGamma, RuleOmega, RuleDefault };
        public readonly List<Component> Components;
        public readonly List<char> ActiveVariables;
        public readonly RuleType Rule_Type;
        public TableauxNode ParentNode;
        public TableauxNode LeftNode;
        public TableauxNode RightNode;
        public bool? LeafIsClosed;

        //Properties
        public bool? Branched { get; set; }
        public int NodeNumber { get; set; } = ++ParsingModule.NodeCounter;


        //Constructors
        public TableauxNode(Component root, TableauxNode parent = null)
        {
            Components = new List<Component>();
            this.Rule_Type = RuleType.RuleDefault;
            this.ParentNode = parent;
            this.Branched = false;
            root.Belongs = this;
            Components.Add(root);
            if (parent != null)
                parent.LeftNode ??= this;
        }

        /// <summary>
        /// Used for non-branched a-rules & gamma-rule
        /// </summary>
        /// <param name="components" />
        /// <param name="processed" />
        /// <param name="parent" />
        /// <param name="ruleType">Alpha or Gamma</param> 
        public TableauxNode(List<Component> components, Component processed,
                TableauxNode parent, RuleType ruleType)
        {
            this.Components = new List<Component>();
            this.ActiveVariables = new List<char>();
            this.Rule_Type = ruleType;
            this.ParentNode = parent;
            parent.Branched = false;
            parent.LeftNode = this;
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    component.Belongs = this;
                    this.Components.Add(component);
                }
            });
            components.ForEach(x =>
            {
                x.Belongs = this;
                this.Components.Add(x);
            });
            if (parent.ActiveVariables?.Count != 0 && parent.ActiveVariables != null)
            {
                parent.ActiveVariables?.ForEach(x => this.ActiveVariables?.Add(x));
            }
            Components.Sort(new ComponentComparer());
        }

        /// <summary>
        /// // Used for δ(Delta)-rule
        /// </summary>
        /// <param name="component"></param>
        /// <param name="processed"></param>
        /// <param name="parent"></param>
        public TableauxNode(Component component, Component processed, TableauxNode parent, char variable)
        {
            this.Components = new List<Component>();
            this.ActiveVariables = new List<char>() { variable };
            this.Rule_Type = RuleType.RuleDelta;
            this.ParentNode = parent;
            parent.Branched = false;
            parent.LeftNode = this;
            component.Belongs = this;
            if (parent.ActiveVariables?.Count != 0)
            {
                parent.ActiveVariables?.ForEach(x => this.ActiveVariables.Add(x));
            }
            parent.Components.ForEach(node =>
            {
                if (node != processed)
                {
                    var newNode = BinaryTree.CloneNode(node, BinaryTree.Object);
                    newNode.Belongs = this;
                    Components.Add(newNode);
                }
            });
            Components.Add(component);
            Components.Sort(new ComponentComparer());
        }

        /// <summary>
        /// // Used for branched B-rules
        /// </summary>
        /// <param name="node"></param>
        /// <param name="processed"></param>
        /// <param name="parent"></param>
        public TableauxNode(Component node, Component processed, TableauxNode parent, RuleType ruleType)
        {
            this.Components = new List<Component>();
            this.ActiveVariables = new List<char>();
            this.Rule_Type = ruleType;
            node.Belongs = this;
            parent.Components.ForEach(component =>
            {
                if (component != processed)
                {
                    var newNode = BinaryTree.CloneNode(component, BinaryTree.Object);
                    newNode.Belongs = this;
                    Components.Add(newNode);
                }
            });
            Components.Add(node);
            parent.Branched = (ruleType == RuleType.RuleBeta);
            this.ParentNode = parent;
            if (parent.LeftNode == null) parent.LeftNode = this;
            else if (parent.RightNode == null && ruleType == RuleType.RuleBeta) parent.RightNode = this;

            if (parent.ActiveVariables?.Count != 0)
            {
                parent.ActiveVariables?.ForEach(x => this.ActiveVariables?.Add(x));
            }
            Components.Sort(new ComponentComparer());
        }

        public void IsClosed()
        {
            if (LeafIsClosed != true)
            {
                Components.ForEach(x => InfixFormulaGenerator.Calculator.Calculate(x));
                if (LeftNode == null && this.RightNode == null)
                {// Node has not being Processed
                    if (Components.Count > 1)
                    {
                        FindContradiction();
                        if (Branched == false && LeftNode == null)
                        {
                            LeafIsClosed = false;
                        }
                        else
                        {
                            if (LeafIsClosed != true)
                            {
                                Evaluate();
                            }
                        }
                    }
                    else
                    {
                        //Root of Tableaux Node
                        Evaluate();
                    }
                }
                else
                {
                    //Node Has already Processed and simplified
                    if (LeftNode != null)
                        this.LeafIsClosed = Branched == false
                            ? LeftNode.LeafIsClosed
                            : LeftNode.LeafIsClosed == true && this.RightNode.LeafIsClosed == true;
                }
            }
        }
        private void Evaluate()
        {
            if (LeafIsClosed != null) return;
            Tableaux.Object.Visit(this);
            if (Branched == true)
            {
                this.LeftNode.IsClosed();
                this.RightNode.IsClosed();
            }
            else if (Branched == false)
            {
                LeftNode?.IsClosed();
            }
            IsClosed();
        }
        private void FindContradiction()
        {
            for (var i = 0; i < Components.Count && this.LeafIsClosed != true; i++)
            {
                for (var j = i + 1; j < Components.Count; j++)
                {
                    var node1Formula = Components[i].InFixFormula;
                    var node2Formula = Components[j].InFixFormula;

                    if (Components[j] is SingleComponent)
                    {
                        node2Formula = Components[j] is SingleComponent ? $"¬{node2Formula}" : $"¬({node2Formula})";
                        if (node1Formula != node2Formula) continue;
                        this.LeafIsClosed = true;
                        break;
                    }
                    if (Components[i] is SingleComponent)
                    {
                        node1Formula = Components[i] is SingleComponent ? $"¬{node1Formula}" : $"¬({node1Formula})";
                        if (node1Formula != node2Formula) continue;
                        this.LeafIsClosed = true;
                        break;
                    }
                }
            }
        }

        private string Label()
        {
            var label = "";
            Components.ForEach(x => { InfixFormulaGenerator.Calculator.Calculate(x); label += x.InFixFormula + ",\n"; });

            if (this.ActiveVariables?.Count != 0 && this.ActiveVariables != null)
            {
                var vars = this.ActiveVariables.Aggregate("", (current, next) => current + $"{next},");
                label += $"\n\n vars[{vars.Remove(vars.Length - 1)}]";
            }
            if (this.LeafIsClosed == true)
                label += "\n\n CLOSED";
            else if (this.LeafIsClosed == false)
                label += "\n\n OPENED";

            return label;
        }
        public string GraphVizFormula()
        {
            string temp = "";
            temp += $"node{NodeNumber} [ label = \"{this.Label()}\" shape=rectangle style=filled" +
                    " color=" + (LeafIsClosed == true ? "red" : "black") +
                    " fillcolor=" + (Rule_Type == RuleType.RuleAlpha ? "yellow" :
                        Rule_Type == RuleType.RuleBeta ? "palegreen" : Rule_Type == RuleType.RuleDelta ? "skyblue" :
                        Rule_Type == RuleType.RuleGamma ? "brown1" : Rule_Type == RuleType.RuleOmega ? "darkorange" :
                        "gray88") + "]";

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
