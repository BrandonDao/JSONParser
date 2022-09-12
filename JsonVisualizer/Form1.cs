using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonVisualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Node node = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5, new Node(6, null))))));
            TreeNode root = new TreeNode("Json:");
            Visualize(node, root);
            visualizer.Nodes.Add(root);
        }

        void Visualize(Node node, TreeNode treeNode)
        {
            if (node == null) return;
            treeNode.Nodes.Add($"{node.Value}");
            Visualize(node.Next, treeNode.Nodes[0]);
        }
        class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }

            public Node(int value, Node next)
            {
                Value = value;
                Next = next;
            }
        }


    }
}
