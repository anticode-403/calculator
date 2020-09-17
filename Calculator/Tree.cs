using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using static Calculator.Operations;

namespace Calculator
{
  public class TreeNode : IEnumerable<TreeNode>
  {
    public readonly Dictionary<string, TreeNode> children = new Dictionary<string, TreeNode>();
    public readonly string id;
    public OperableEnum operation = OperableEnum.Solved;
    public TreeNode parent { get; private set; }

    public int count
    {
      get { return this.children.Count; }
    }

    public TreeNode(string _id, OperableEnum _operation)
    {
      id = _id;
      operation = _operation;
    }

    public TreeNode GetChild(string id)
    {
      return this.children[id];
    }

    public void Add(TreeNode node)
    {
      if (node.parent != null) node.parent.children.Remove(node.id);
      node.parent = this;
      children.Add(node.id, node);
    }

    public IEnumerator<TreeNode> GetEnumerator()
    {
      return children.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public static TreeNode BuildTree(string tree)
    {
      tree = tree.Trim();
      TreeNode parent = null;
      string text = null;
      TreeNode root = null;
      for (int i = 0; i < tree.Length; i++)
      {
        if (tree[i] == '(' && (ParseOperable(tree[i + 1]) == OperableEnum.Solved || tree[i + 2] != ' ')) throw new Exception("Opened a new tree branch without providing an operable.");
        else if (tree[i] == '(') continue;
        if (ParseOperable(tree[i]) != OperableEnum.Solved && (tree[i + 1] == ' ' && tree[i + 1] != ')'))
        {
          if (root == null)
          {
            root = new TreeNode(tree[i].ToString(), ParseOperable(tree[i]));
            parent = root;
          } else
          {
            TreeNode newNode = new TreeNode(tree[i].ToString(), ParseOperable(tree[i]));
            parent.Add(newNode);
            parent = newNode;
          }
        } else if (tree[i] == ' ')
        {
          if (text == null) continue;
          else
          {
            while (parent.children.ContainsKey(text)) text = "_" + text;
            parent.Add(new TreeNode(text, OperableEnum.Solved));
            text = null;
          }
        } else if (tree[i] == ')')
        {
          if (text == null) continue;
          else
          {
            while (parent.children.ContainsKey(text)) text = "_" + text;
            parent.Add(new TreeNode(text, OperableEnum.Solved));
            text = null;
            parent = parent.parent;
          }
        } else
        {
          if (text == null) text = "";
          text += tree[i];
        }
      }
      return root;
    }
  }
}
