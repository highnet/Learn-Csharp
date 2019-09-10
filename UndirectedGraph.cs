using System;
using System.Collections.Generic;

namespace Undirected_Graph
{
    public class UndirectedGraph
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
            if (a != null && b != null)
            {
                a.edges.Add(b);
            }
            Console.WriteLine(a.edges.Count);
        }

        public int Count()
        {
            return nodes.Count;
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
                foreach(Node edge in node.edges)
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
