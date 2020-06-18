using System.Collections.Generic;
using System.Runtime.InteropServices;
using LPP.Composite_Pattern;
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
        public PropositionalVariables PropositionalVariables = null;

        private bool _nonModifiable = false;

        public BinaryTree() => PropositionalVariables = new PropositionalVariables();

        //Methods & Functions
        public Component InsertNode(Component root, Component node)
        {
            if (_nonModifiable ==false)
            {
                SingleComponent singleNode = node as SingleComponent;
                if (singleNode != null)
                {
                    if(singleNode is Variable) 
                        PropositionalVariables.AddPropositionalVariable(singleNode as Variable);
                    return InsertSingleNode(root, singleNode);
                }
                else
                {
                    CompositeComponent composite = node as CompositeComponent;
                    return InsertCompositeNode(root, composite);
                }
            }
            else
            {
                return Root;
            }
            
        }

        public bool MakeIt_Non_Modifiable()
        {
            if (!_nonModifiable)
            {
                
                _nonModifiable = true;
                return _nonModifiable;
            }
            return true;
        }

        private Component InsertCompositeNode(Component root, Component newNode)
        {
            if (root == null)
            {
                this.Root = newNode as CompositeComponent;
                root = Root;
                return Root;
            }

            if (newNode is Negation)
            {
                //Try to put the function on the left side of tree
                if (root is Negation)
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
                            else
                            {
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
                }


                return newNode;
            }
            else
            {
                //Try to put the operation on the right side of tree
                if (root is Negation)
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
            if (root is Negation)
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

        public static BinaryTree DNFBinaryTree(List<BinaryTree> nodes)
        {
            BinaryTree binaryTree = null;
            Component _dnfRoot = null;
            if (nodes.Count == 1)
            {
                return nodes[0];
            }
            else
            {
                if (binaryTree == null)
                {
                    binaryTree = new BinaryTree();
                    binaryTree.InsertNode(_dnfRoot, new Disjunction());
                    _dnfRoot = binaryTree.Root;
                }

                foreach (var node in nodes)
                {
                    binaryTree.PropositionalVariables.Variables.AddRange(node.PropositionalVariables.Variables);
                    var root = node.Root;
                    if (_dnfRoot.LeftNode != null && _dnfRoot.RightNode != null)
                    {
                        var newRoot = new Disjunction();
                        _dnfRoot.Parent = newRoot;
                        newRoot.LeftNode = _dnfRoot;
                        _dnfRoot = newRoot;
                        _dnfRoot.RightNode = root;
                        binaryTree.Root = newRoot;
                    }
                    else
                    {
                        if (_dnfRoot.LeftNode == null)
                        {
                            _dnfRoot.LeftNode = root;
                        }
                        else
                        {
                            _dnfRoot.RightNode = root;
                        }
                    }
                }
                return binaryTree;
            }
        }

        public static Component CloneNode(Component node, BinaryTree bt)
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
            else if (node is Negation)
                newNode = new Negation();
            else if (node is Nand)
                newNode = new Nand();
            else if (node is TrueFalse)
                newNode = new TrueFalse(((TrueFalse) node).Data);
            else
            {
                newNode = new Variable(((Variable) node).Symbol);
                bt.PropositionalVariables.AddPropositionalVariable(newNode as Variable);
            }

            newNode.Parent = node.Parent;
            if(node is CompositeComponent)
            {
                if(node is Negation)
                {
                    newNode.LeftNode = node.LeftNode;
                }
                else
                {
                    if (node.LeftNode != null)
                    {
                        newNode.LeftNode = CloneNode(node.LeftNode,bt);
                    }
                    else
                    {
                        newNode.LeftNode = node.LeftNode;
                    }

                    if (node.RightNode != null)
                    {
                        newNode.RightNode = CloneNode(node.RightNode,bt);
                    }
                    else
                    {
                        newNode.RightNode = node.RightNode;
                    }
                }
            }

            newNode.NodeNumber++;
            return newNode;
        }
    }
}