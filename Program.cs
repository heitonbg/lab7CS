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

        Func<BinaryTree<int>, IEnumerable<int>> inorder = t => t.InOrderTraversal();

        Console.WriteLine("In-order traversal:");
        foreach (var number in inorder(tree))
        {
            Console.WriteLine(number);
        }
    }
}