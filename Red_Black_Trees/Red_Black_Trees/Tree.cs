using System;
using System.Collections.Generic;
using System.Text;

namespace Red_Black_Trees
{
    public class DuplicateValueException : ArgumentException
    {
        public DuplicateValueException(string message) : base(message) { }
        public DuplicateValueException(string message, string paramName) : base(message, paramName) { }
    }

    public class Tree<T> where T : IComparable<T>
    {
        class Node
        {
            public T Value;
            public Node Left;
            public Node Right;
            public bool IsBlack;


            //constructor
            public Node(T value)
            {
                this.Value = value;
                IsBlack = false;
            }
        }

        Node root;

        //constructor
        //Recursive Add Function

        public void Add(T value)
        {
            root = Add(root, value);
        }

        /// <summary>
        /// Adds value below the given node and returns the new root of the operation
        /// </summary>
        /// <param name="curr">the node to add the value below</param>
        /// <param name="value">the value to add</param>
        /// <returns>the new root of the operation</returns>
        private Node Add(Node curr, T value)
        {
            if (curr == null)
            {
                return new Node(value);
            }

            //before recursion: as we travel DOWN the tree

            //break up 4 nodes
            if (IsRed(curr.Left) && IsRed(curr.Right))
            {
                FlipColor(curr);
            }

            if (value.CompareTo(curr.Value) > 0)
            {
                curr.Right = Add(curr.Right, value);
            }
            else if (value.CompareTo(curr.Value) < 0)
            {
                curr.Left = Add(curr.Left, value);
            }
            else if (value.CompareTo(curr.Value) == 0)
            {
                throw new DuplicateValueException("duplicate values are not allowed", "value");
            }

            //post recursion: as we travel UP the tree
            curr = Fixup(curr);

            return curr;
        }

        private bool IsRed(Node node)
        {
            if (node == null || node.IsBlack)
            {
                return false;
            }
            return true;
        }

        private void FlipColor(Node node)
        {
            node.IsBlack = !node.IsBlack;
            if (node.Left != null) node.Left.IsBlack = !node.Left.IsBlack;
            if (node.Right != null) node.Right.IsBlack = !node.Right.IsBlack;
        }


        //takes in node, rotates, returns new root of rotation
        private Node RotateLeft(Node node)
        {
            var child = node.Right;
            node.Right = child.Left;
            child.Left = node;

            //child takes nodes color
            //node becomes red
            child.IsBlack = node.IsBlack;
            node.IsBlack = false;


            return child;
        }


        private Node RotateRight(Node node)
        {
            var child = node.Left;
            node.Left = child.Right;
            child.Right = node;

            child.IsBlack = node.IsBlack;
            node.IsBlack = false;

            return child;
        }

        private Node Fixup(Node node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Right) && IsRed(node.Left))
            {
                FlipColor(node);
            }

            return node;
        }

        public void PreOrder()
        {
            Function(root);
            void Function(Node n)
            {
                Console.WriteLine(n.Value);
                if (n.Left != null) Function(n.Left);
                if (n.Right != null) Function(n.Right);
            }
        }

        public void Remove(T value)
        {
            root = Remove(root, value);
        }

        private Node Remove(Node curr, T value)
        {
            if (value.CompareTo(curr.Value) < 0)
            {
                if (!IsRed(curr.Left) && !IsRed(curr.Left.Left))
                {
                    curr = MoveRedLeft(curr);
                }

                curr.Left = Remove(curr.Left, value);
            }
            else
            {
                if (IsRed(curr.Left))
                {
                    curr = RotateRight(curr);
                }
                //if the value is equal to the current value, or there is nothing to the left or right of current, return a null;
                if (value.CompareTo(curr.Value) == 0 && (curr.Left == null && curr.Right == null))
                {
                    return null;
                }
                if (curr.Right != null)
                {
                    //if the right of current is not red or the right of the left of the current is not red, move down the right sub tree
                    if (!IsRed(curr.Right) && !IsRed(curr.Right.Left))
                    {
                        curr = MoveRedRight(curr);
                    }
                    if (value.CompareTo(curr.Value) == 0)
                    {
                        Node min = GetMinimum(curr.Right);
                        curr.Value = min.Value;
                        curr.Right = Remove(curr.Right, min.Value);
                    }
                    else
                    {
                        curr.Right = Remove(curr.Right, value);
                    }
                }
            }


            curr = Fixup(curr);
            return curr;
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Right) && IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColor(node);
            }

            return node;
        }
        private Node MoveRedRight(Node node)
        {
            FlipColor(node);

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColor(node);
            }

            return node;
        }

        private Node GetMinimum(Node node)
        {
            Node tempnode = node;
            while (tempnode.Left != null)
            {
                tempnode = tempnode.Left;
            }
            return tempnode;
        }
        //remove
        //MoveRedLeft
        //MoveRedRight





    }
}
