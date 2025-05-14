using System;
using System.Collections.Generic;

namespace Tree
{
    public class Tree
    {
        public List<Node> nodes = new List<Node>();

        public void Connect(int id_a, int id_b)
        {
            Node a = null;
            Node b = null;
            foreach (Node node in this.nodes)
            {
                if (node.id == id_a)
                {
                    a = node;
                }
                if (node.id == id_b)
                {
                    b = node;
                }
            }
            if (a != null && b != null && id_a != id_b && !AreConnected(id_a, id_b))
            {
                a.edges.Add(b);
                b.edges.Add(a);
            }
        }

        public bool AreConnected(int id_a, int id_b)
        {
            Node a = null;
            Node b = null;
            foreach (Node node in this.nodes)
            {
                if (a != null && b != null)
                {
                    break;
                }

                if (node.id == id_a)
                {
                    a = node;
                }

                if (node.id == id_b)
                {
                    b = node;
                }
            }

            return a != null && b != null && id_a != id_b && a.edges.Contains(b) && b.edges.Contains(a);
        }

        public int Order() // number of vertices in the graph
        {
            return nodes.Count;
        }

        public int Weight(int id) // number of edges coming to-and-from the node id
        {
            Node n = null;

            foreach(Node node in this.nodes)
            {
                if (node.id == id)
                {
                    n = node;
                    break;
                }
            }
            return n.edges.Count;

        }

        public int Size() // number of edges in the graph
        {
            int doubledSum = 0; 
            foreach (Node node in this.nodes)
            {
                    doubledSum += node.edges.Count;
            }
            return doubledSum / 2;
        }

        public void Add(int _id, int _value)
        {
            nodes.Add(new Node(_id, _value));
        }

        public override string ToString()
        {
            string str = "";
            foreach (Node node in this.nodes)
            {

                str += "[id: " + node.id + " value: " + node.value + " edges: { ";
                foreach (Node edge in node.edges)
                {
                    if (edge != null)
                    {
                        str += edge.id + ", ";
                    }
                }
                str += "} ] \n";
            }
            return str;
        }

        public class Node
        {
            public int id;
            public int value;
            public List<Node> edges = new List<Node>();

            public Node(int _id, int _value)
            {
                id = _id;
                value = _value;
            }
        }
    }

}
