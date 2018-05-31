using System;
using System.Collections;

namespace BST_Enumerable
{
    class Program
    {
        static BST initBST()
        {
            BST myBST = new BST();
            Random rand = new Random();
            Console.WriteLine("Init value: ");
            for (int i = 1; i <= 10; i++)
            {
                int val = rand.Next(1, 100);
                Console.Write(val + " ");
                myBST.Add(val);
            }
            Console.WriteLine();
            return myBST;
        }

        static void LNR(BST myBST)
        {
            Console.WriteLine("LNR order: ");
            myBST.ResetEnumerator(false);
            foreach (var node in myBST)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }

        static void RNL(BST myBST)
        {
            Console.WriteLine("RNL order: ");
            myBST.ResetEnumerator(true);
            foreach (var node in myBST)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            BST myBST = initBST();
            Console.WriteLine("------");
            LNR(myBST);
            Console.WriteLine("------");
            RNL(myBST);
            Console.ReadLine();
        }
    }

    class BST : IEnumerable
    {
        private Node Root;
        private BSTEnumerator MyEnumerator;

        public BST()
        {
            Root = null;
        }

        public bool Add(int value)
        {
            if (Root == null)
            {
                Root = new Node(value);
                MyEnumerator = new BSTEnumerator(Root, false);
                return true;
            }

            Node cur = Root;

            while (true)
            {
                if (cur.Value == value)
                    return false;
                else if (cur.Value > value)
                {
                    if (cur.Left != null)
                        cur = cur.Left;
                    else
                    {
                        cur.Left = new Node(value, cur);
                        return true;
                    }
                }
                else if (cur.Value < value)
                {
                    if (cur.Right != null)
                        cur = cur.Right;
                    else
                    {
                        cur.Right = new Node(value, cur);
                        return true;
                    }
                }
            }
        }

        public void ResetEnumerator(bool reverse)
        {
            MyEnumerator.Reset();
            MyEnumerator.Reverse = reverse;
        }

        public IEnumerator GetEnumerator()
        {
            return MyEnumerator;
        }

        class BSTEnumerator : IEnumerator
        {
            private Node Root { get; set; }
            private Node CurNode { get; set; }
            public bool Reverse { get; set; }

            public BSTEnumerator(Node root, bool reverse)
            {
                Root = root;
                CurNode = root;
                Reverse = reverse;
            }

            public object Current => CurNode.Value;

            public bool MoveNext()
            {
                if (!Reverse)
                    CurNode = Next(CurNode);
                else
                    CurNode = NextReverse(CurNode);

                if (CurNode != null)
                {
                    CurNode.Visited = true;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                ResetNode(Root);
                CurNode = Root;
            }

            private Node Next(Node node)
            {
                if (node.Left != null && !node.Left.Visited) return Next(node.Left);
                if (!node.Visited) return node;
                if (node.Right != null && !node.Right.Visited) return Next(node.Right);
                if (node.Parent != null) return Next(node.Parent);
                return null;
            }

            private Node NextReverse(Node node)
            {
                if (node.Right != null && !node.Right.Visited) return NextReverse(node.Right);
                if (!node.Visited) return node;
                if (node.Left != null && !node.Left.Visited) return NextReverse(node.Left);
                if (node.Parent != null) return NextReverse(node.Parent);
                return null;
            }

            private void ResetNode(Node node)
            {
                if (node == null) return;
                node.Visited = false;
                ResetNode(node.Left);
                ResetNode(node.Right);
            }
        }

        class Node
        {
            public Node(int value)
            {
                Value = value;
                Left = null;
                Right = null;
                Visited = false;
            }

            public Node(int value, Node parent)
            {
                Value = value;
                Parent = parent;
            }

            public Node Parent { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Value { get; set; }
            public bool Visited { get; set; }
        }
    }
}
