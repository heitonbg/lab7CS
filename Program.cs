using System;
using System.Collections.Generic;

class Program
{
  static void Main(string[] args)
  {
    BinaryTree<int> tree = new BinaryTree<int>();
    tree.Add(5);
    tree.Add(3);
    tree.Add(8);
    tree.Add(9);
    tree.Add(10);
    tree.Add(11);

    Func<BinaryTree<int>, IEnumerable<int>> inorder = trees => trees.InOrderTraversal();
    
    Console.WriteLine("Симметричный обход:");
    foreach (var number in inorder(tree))
    {
      Console.WriteLine(number);
    }

    Console.WriteLine("\nПрямой обход:");
    foreach (var number in tree.PreOrderTraversal())
    {
      Console.WriteLine(number);
    }

    Console.WriteLine("\nОбратный обход:");
    foreach (var number in tree.PostOrderTraversal())
    {
      Console.WriteLine(number);
    }

    Console.WriteLine("\nОбход с использованием Next/Current:");
    var node = tree.FindLeftmostNode(tree.GetRoot());
    while (node != null)
    {
      Console.WriteLine(tree.Current(node));
      node = tree.Next(node);
    }
  }
}