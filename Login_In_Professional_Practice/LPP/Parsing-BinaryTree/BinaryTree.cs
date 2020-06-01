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
        public Component _root;
       
        //Methods & Functions
        public Component InsertNode(Component root, Component node)
        {
            SingleComponent singleNode = node as SingleComponent;
            if (singleNode != null)
            {

                return InsertSingleNode(root, singleNode);
            }
            else
            {
                CompositeComponent composite = node as CompositeComponent;
                return InsertCompositeNode(root, composite);
            }
        }
        private Component InsertCompositeNode(Component root, Component newNode)
        {
            if (root == null)
            {
                this._root = newNode as CompositeComponent;
                ((CompositeComponent)this._root).PropositionalVariables = new PropositionalVariables();
                return _root;
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
                                _root = root;
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
                this._root = singleNode;
                this._root = _root as SingleComponent;
                return _root;
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
                        _root = root;
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