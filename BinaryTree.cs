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

    public TreeNode<T> Next(TreeNode<T> currentNode)
    {
        if (currentNode == null) return null;

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
        if (currentNode == null) return null;

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
            if (node == null) yield break;

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

    private TreeNode<T> FindLeftmostNode(TreeNode<T> currentNode)
    {
        if (currentNode == null) return null;

        while (currentNode.Left != null)
        {
            currentNode = currentNode.Left;
        }

        return currentNode;
    }
}