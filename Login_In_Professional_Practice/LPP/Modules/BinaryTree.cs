using LPP.Composite_Pattern;
using LPP.Composite_Pattern.Node;

namespace LPP
{
    /// <summary>
    /// A class who is responsible for creating BinaryTree from object structure.
    /// </summary>
    public class BinaryTree
    {
        public CompositeComponent _root;

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
                this._root = (CompositeComponent)newNode;
                this._root.PropositionalVariables = new PropositionalVariables();
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
                        return singleNode.Parent;
                    }
                }
            }
        }
    }
}