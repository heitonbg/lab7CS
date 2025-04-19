using System;
using System.Collections;
using System.Collections.Generic;

internal class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
{
  private TreeNode<T> _root;

  public void Add(T value)
  {
    if (_root == null)
    {
      _root = new TreeNode<T>(value);
    }
    else
    {
      AddChild(_root, value);
    }
  }

  private void AddChild(TreeNode<T> parentNode, T value)
  {
    if (value.CompareTo(parentNode.Data) < 0)
    {
      if (parentNode.Left == null)
      {
        parentNode.Left = new TreeNode<T>(value) { Parent = parentNode };
      }
      else
      {
        AddChild(parentNode.Left, value);
      }
    }
    else
    {
      if (parentNode.Right == null)
      {
        parentNode.Right = new TreeNode<T>(value) { Parent = parentNode };
      }
      else
      {
        AddChild(parentNode.Right, value);
      }
    }
  }

  public IEnumerable<T> PreOrderTraversal()
  {
    if (_root == null) yield break;

    var stack = new Stack<TreeNode<T>>();
    stack.Push(_root);

    while (stack.Count > 0)
    {
      var node = stack.Pop();
      yield return node.Data;

      if (node.Right != null) 
      {
        stack.Push(node.Right);
      }
      if (node.Left != null) 
      {
        stack.Push(node.Left);
      }
    }
  }

  public IEnumerable<T> PostOrderTraversal()
  {
    if (_root == null) yield break;

    var stack = new Stack<TreeNode<T>>();
    var result = new Stack<T>();
    stack.Push(_root);

    while (stack.Count > 0)
    {
      var node = stack.Pop();
      result.Push(node.Data);

      if (node.Left != null) 
      {
        stack.Push(node.Left);
      }
      if (node.Right != null) 
      {
        stack.Push(node.Right);
      }
    }

    while (result.Count > 0)
    {
      yield return result.Pop();
    }
  }

  public TreeNode<T> Next(TreeNode<T> currentNode)
  {
    if (currentNode == null) 
    {
      return null;
    }

    if (currentNode.Right != null)
    {
      currentNode = currentNode.Right;
      while (currentNode.Left != null)
      {
        currentNode = currentNode.Left;
      }
      return currentNode;
    }
    else
    {
      while (currentNode.Parent != null && currentNode == currentNode.Parent.Right)
      {
        currentNode = currentNode.Parent;
      }
      return currentNode.Parent;
    }
  }

  public TreeNode<T> Previous(TreeNode<T> currentNode)
  {
    if (currentNode == null)
    {
      return null;
    }

    if (currentNode.Left != null)
    {
      currentNode = currentNode.Left;
      while (currentNode.Right != null)
      {
        currentNode = currentNode.Right;
      }
      return currentNode;
    }
    else
    {
      while (currentNode.Parent != null && currentNode == currentNode.Parent.Left)
      {
        currentNode = currentNode.Parent;
      }
      return currentNode.Parent;
    }
  }

  public T Current(TreeNode<T> node) => node != null ? node.Data : throw new InvalidOperationException("Node пуст");

    public TreeNode<T> GetRoot()
    {
        return _root;
    }

  public IEnumerator<T> GetEnumerator()
  {
    TreeNode<T> currentNode = FindLeftmostNode(_root);

    while (currentNode != null)
    {
      yield return currentNode.Data;
      currentNode = Next(currentNode);
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  public IEnumerable<T> InOrderTraversal()
  {
    return Traverse(_root);

    IEnumerable<T> Traverse(TreeNode<T> node)
    {
      if (node == null) 
      {
        yield break;
      }

      foreach (var leftNode in Traverse(node.Left))
      {
        yield return leftNode;
      }

      yield return node.Data;

      foreach (var rightNode in Traverse(node.Right))
      {
        yield return rightNode;
      }
    }
  }

  public TreeNode<T> FindLeftmostNode(TreeNode<T> currentNode)
  {
    if (currentNode == null) 
    {
      return null;
    }

    while (currentNode.Left != null)
    {
      currentNode = currentNode.Left;
    }

    return currentNode;
  }
}