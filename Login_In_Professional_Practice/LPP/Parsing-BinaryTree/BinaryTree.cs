using System.Collections.Generic;
using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;

namespace LPP
{
    /// <summary>
    /// A class who is responsible for creating BinaryTree from object structure.
    /// </summary>
    public class BinaryTree
    {
        public Component Root;
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
                    if(singleNode is PropositionalVariable) 
                        PropositionalVariables.AddPropositionalVariable(singleNode as PropositionalVariable);
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
                return Root;
            }

            if (newNode is NegationConnective)
            {
                //Try to put the function on the left side of tree
                if (root is NegationConnective)
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
                    if (root is ImplicationConnective)
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
                                var newRoot = new ConjunctionConnective();
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
                if (root is NegationConnective)
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
                    if (root is ImplicationConnective )
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
            if (root is NegationConnective)
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
                        var newRoot = new ConjunctionConnective();
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
        
        public static Component DNFBinaryTree(List<Component> nodes)
        {
            Component _dnfRoot = null;

            if (nodes.Count == 1)
            {
                _dnfRoot = nodes[0];
                return _dnfRoot;
            }
            else
            {
                if (_dnfRoot == null)
                {
                    _dnfRoot = new DisjunctionConnective();
                }

                foreach (var node in nodes)
                {
                    if (_dnfRoot.LeftNode != null && _dnfRoot.RightNode != null)
                    {
                        var newRoot = new DisjunctionConnective();
                        _dnfRoot.Parent = newRoot;
                        newRoot.LeftNode = _dnfRoot;
                        _dnfRoot = newRoot;
                        _dnfRoot.RightNode = node;
                    }
                    else
                    {
                        if (_dnfRoot.LeftNode == null)
                        {
                            _dnfRoot.LeftNode = node;
                        }
                        else
                        {
                            _dnfRoot.RightNode = node;
                        }
                    }
                }
                return _dnfRoot;
            }
        }
    }
}