using System;
using System.Collections;

namespace BinarySortTreeEnumerable
{
    class BinarySortTree
    {
        static BinarySortTreeComponent createBinaryTree()
        {
            // Khoi tao cay nhi phan
            BinarySortTreeComponent binarytree = new BinarySortTreeComponent();

            // Thuc hien random 10 so bat ki vao cay nhi phan vua tao
            Random rand = new Random();
            Console.WriteLine("Init value: ");
            for (int i = 1; i <= 10; i++)
            {
                int value = rand.Next(1, 100);
                Console.Write(value + " ");
                binarytree.Add(value);
            }
            Console.WriteLine();
            return binarytree;
        }

        // sap xep tu thap den cao theo LEFT NODE RIGHT
        static void LeftNodeRight(BinarySortTreeComponent myBST)
        {
            Console.WriteLine("LeftNodeRight order: ");
            myBST.ResetEnumerator(false);
            foreach (var node in myBST)
            {
                Console.Write(node + " ");
            }
            Console.WriteLine();
        }

        //sap xep theo tu cao xuong thap RIGHT NODE LEFT
        static void RightNodeLeft(BinarySortTreeComponent myBST)
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
            BinarySortTreeComponent binarytree = createBinaryTree();

            Console.WriteLine();
            LeftNodeRight(binarytree);

            Console.WriteLine();
            RightNodeLeft(binarytree);
            Console.ReadLine();
        }
    }

    class BinarySortTreeComponent : IEnumerable
    {

        private Node Root;
        private BinarySortTreeEnumerator Enumerator;

        public BinarySortTreeComponent()
        {
            Root = null;
        }

        public bool Add(int value)
        {
            if (Root == null)
            {
                Root = new Node(value);
                Enumerator = new BinarySortTreeEnumerator(Root, false);
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
            Enumerator.Reset();
            Enumerator.Reverse = reverse;
        }

        public IEnumerator GetEnumerator()
        {
            return Enumerator;
        }

        class BinarySortTreeEnumerator : IEnumerator
        {
            private Node Root { get; set; }
            private Node CurNode { get; set; }
            public bool Reverse { get; set; }

            public BinarySortTreeEnumerator(Node root, bool reverse)
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
