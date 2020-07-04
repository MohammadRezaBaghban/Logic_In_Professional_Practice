using System;
using System.Collections.Generic;
using LPP.Composite_Pattern.Components;
using LPP.Composite_Pattern.Connectives;
using LPP.Composite_Pattern.Variables;

namespace LPP
{
    /// <summary>
    /// A class who is responsible for creating BinaryTree from object structure.
    /// </summary>
    public class BinaryTree
    {
        public Component Root;
        public static BinaryTree Object { get; } = new BinaryTree();
        public readonly PropositionalVariables PropositionalVariables;

        private bool _nonModifiable;

        public BinaryTree() => PropositionalVariables = new PropositionalVariables();

        //Methods & Functions
        public Component InsertNode(Component root, Component node)
        {
            if (_nonModifiable ==false)
            {
                SingleComponent singleNode = node as SingleComponent;
                if (singleNode != null)
                {

                    if (singleNode is Variable variable)
                    {
                        PropositionalVariables.AddPropositionalVariable(singleNode as Variable);
                        if (Char.IsLower(variable.Symbol))
                        {
                            (root as IVariableContainer)?.ObjectVariables.Variables.Add(variable);
                            return Root;
                        }
                        return InsertSingleNode(root, singleNode);
                    }
                    return InsertSingleNode(root, singleNode);
                }
                var composite = node as CompositeComponent;
                return InsertCompositeNode(root, composite);
            }
            return Root;
        }

        public bool MakeIt_Non_Modifiable()
        {
            if (_nonModifiable) return true;
            _nonModifiable = true;
            return _nonModifiable;
        }

        private Component InsertCompositeNode(Component root, Component newNode)
        {
            if (root == null)
            {
                Root = newNode as CompositeComponent;
                return Root;
            }

            if (newNode is Negation || newNode is Universal || newNode is Existential)
            {
                //Try to put the function on the left side of tree
                if (root is Negation || root is Universal || root is Existential)
                {
                    if (root.LeftNode == null)
                    {
                        root.LeftNode = newNode;
                        root.LeftNode.Parent = root;
                    }
                    else
                    {
                        if (root.Parent != null)
                        {
                            return InsertNode(root.Parent, newNode);
                        }
                    }
                }
                else
                {
                    //Because of importance of Operator
                    if (root is Implication)
                    {
                        if (root.LeftNode == null)
                        {
                            root.LeftNode = newNode;
                            root.LeftNode.Parent = root;
                        }
                        else if (root.RightNode == null)
                        {
                            root.RightNode = newNode;
                            root.RightNode.Parent = root;
                        }
                        else
                        {
                            if (root.Parent != null)
                            {
                                return InsertNode(root.Parent, newNode);
                            }
                        }
                    }
                    else
                    {
                        //For the sake of having more balanced binary graph
                        if (root.LeftNode == null)
                        {
                            root.LeftNode = newNode;
                            root.LeftNode.Parent = root;
                        }
                        else if (root.RightNode == null)
                        {
                            root.RightNode = newNode;
                            root.RightNode.Parent = root;
                        }
                        else
                        {
                            if (root.Parent != null)
                            {
                                return InsertNode(root.Parent, newNode);
                            }
                            //Specifically be useful for DNF where tree be created from bottom to top
                            var newRoot = new Conjunction();
                            root.Parent = newRoot;
                            newRoot.LeftNode = root;
                            root = newRoot;
                            root.RightNode = newNode;
                            Root = root;
                            return newNode.Parent;
                        }
                    }
                }


                return newNode;
            }
            else
            {
                //Try to put the operation on the right side of tree
                if (root is Negation || root is Universal || root is Existential)
                {
                    if (root.LeftNode == null)
                    {
                        root.LeftNode = newNode;
                        root.LeftNode.Parent = root;
                    }
                    else
                    {
                        if (root.Parent != null)
                        {
                            return InsertNode(root.Parent, newNode);
                        }
                    }
                }
                else
                {

                    //Because of importance of Implication Operator
                    if (root is Implication )
                    {
                        if (root.LeftNode == null)
                        {
                            root.LeftNode = newNode;
                            root.LeftNode.Parent = root;
                        }
                        else if (root.RightNode == null)
                        {
                            root.RightNode = newNode;
                            root.RightNode.Parent = root;
                        }else
                        {
                            if (root.Parent != null)
                            {
                                return InsertNode(root.Parent, newNode);
                            }

                        }
                    }
                    else
                    {
                        if (root.LeftNode == null)
                        {
                            root.LeftNode = newNode;
                            root.LeftNode.Parent = root;
                        }
                        else if(root.RightNode == null)
                        {
                            root.RightNode = newNode;
                            root.RightNode.Parent = root;
                        }
                        else
                        {
                            if (root.Parent != null)
                            {
                                return InsertNode(root.Parent, newNode);
                            }
                        }
                    }

                }

                return newNode;
            }
        }
        private Component InsertSingleNode(Component root, SingleComponent singleNode)
        {
            if (root == null)
            {
                this.Root = singleNode;
                this.Root = Root as SingleComponent;
                return Root;
            }                         

            //Try to put the single node on the left side of tree as much as possible
            if (root is Negation || root is Predicate)
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = singleNode;
                    root.LeftNode.Parent = root;
                    return singleNode.Parent;
                }
                else
                {
                    //If both left and right node were full, insert it to the parent
                    if (root.Parent != null)
                    {
                        return InsertNode(root.Parent, singleNode);
                    }
                    else
                    {
                        return singleNode.Parent;
                    }
                }
            }
            else
            {
                if (root.LeftNode == null)
                {
                    root.LeftNode = singleNode;
                    root.LeftNode.Parent = root;
                    return singleNode.Parent;
                }
                else if (root.RightNode == null)
                {
                    root.RightNode = singleNode;
                    root.RightNode.Parent = root;
                    return singleNode.Parent;
                }
                else
                {
                    //If both left and right node were full, insert it to the parent
                    if (root.Parent != null)
                    {
                        return InsertNode(root.Parent, singleNode);
                    }
                    else
                    {
                        //Specifically be useful for DNF where tree be created from bottom to top
                        var newRoot = new Conjunction();
                        root.Parent = newRoot;
                        newRoot.LeftNode = root;
                        root = newRoot;
                        root.RightNode = singleNode;
                        Root = root;
                        return singleNode.Parent;
                    }
                }
            }
        }

        public static BinaryTree DnfBinaryTree(List<BinaryTree> nodes)
        {
            if (nodes.Count != 1)
            {
                var binaryTree = new BinaryTree();
                binaryTree.InsertNode(null, new Disjunction());
                var dnfRoot = binaryTree.Root;

                foreach (var node in nodes)
                {
                    binaryTree.PropositionalVariables.Variables.AddRange(node.PropositionalVariables.Variables);
                    var root = node.Root;
                    if (dnfRoot.LeftNode != null && dnfRoot.RightNode != null)
                    {
                        var newRoot = new Disjunction();
                        dnfRoot.Parent = newRoot;
                        newRoot.LeftNode = dnfRoot;
                        dnfRoot = newRoot;
                        dnfRoot.RightNode = root;
                        binaryTree.Root = newRoot;
                    }
                    else
                    {
                        if (dnfRoot.LeftNode == null)
                        {
                            dnfRoot.LeftNode = root;
                        }
                        else
                        {
                            dnfRoot.RightNode = root;
                        }
                    }
                }
                return binaryTree;
            }
            return nodes[0];
        }

        public static Component CloneNode(Component node, BinaryTree bt,char current ='!',char rename = '!')
        {
            Component newNode;

            if (node is BiImplication)
                newNode = new BiImplication();
            else if (node is Implication)
                newNode = new Implication();
            else if (node is Conjunction)
                newNode = new Conjunction();
            else if (node is Disjunction)
                newNode = new Disjunction();
            else if (node is Negation negation)
            {
                newNode = new Negation();
                ((Negation)newNode).GammaProcessed = negation.GammaProcessed;
            }
            else if (node is Nand)
                newNode = new Nand();
            else if (node is TrueFalse)
                newNode = new TrueFalse(((TrueFalse) node).Data);
            else if(node is Predicate predicate)
            {
                newNode = new Predicate(node.Symbol);
                predicate.ObjectVariables.Variables.ForEach(x=> ((IVariableContainer) newNode)
                    .ObjectVariables.AddPropositionalVariable(CloneVariableForPredicate(x,current,rename))
                );
            }
            else if (node is Universal universal)
            {
                newNode = new Universal();
                universal.ObjectVariables.Variables.ForEach(x => ((IVariableContainer)newNode)
                    .ObjectVariables.AddPropositionalVariable(CloneVariableForPredicate(x, current, rename))
                );
                ((Universal) newNode).GammaProcessed = universal.GammaProcessed;
            }
            else if (node is Existential existential)
            {
                newNode = new Existential();
                existential.ObjectVariables.Variables.ForEach(x => ((IVariableContainer)newNode)
                    .ObjectVariables.AddPropositionalVariable(CloneVariableForPredicate(x, current, rename))
                );
            }
            else
            {
                newNode = new Variable(((Variable) node).Symbol, ((Variable)node).BindVariable);
                bt.PropositionalVariables.AddPropositionalVariable(newNode as Variable);
            }

            newNode.Parent = node.Parent;
            if(node is CompositeComponent)
            {
                if(node is Negation)
                {
                    newNode.LeftNode = CloneNode(node.LeftNode, bt, current, rename);
                }
                else
                {
                    if (node.LeftNode != null)
                    {
                        newNode.LeftNode = CloneNode(node.LeftNode, bt,current,rename);
                    }
                    else
                    {
                        newNode.LeftNode = node.LeftNode;
                    }
                    newNode.RightNode = node.RightNode != null ? CloneNode(node.RightNode,bt, current, rename) : node.RightNode;
                }
            }
            newNode.NodeNumber++;
            return newNode;
        }

        private static Variable CloneVariableForPredicate(Variable v, char current,char rename)
        {
            var variable = (Variable)BinaryTree.CloneNode(v, BinaryTree.Object);
            if (variable.Symbol == current)
                variable.Symbol = rename;
            return variable;
        }
    }
}