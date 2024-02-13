using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var heap = new FibonacciHeap();
        Console.WriteLine("Enter the 1st number");
        var numOne = Console.ReadLine();
        heap.Add(Int32.Parse(numOne));
        Console.WriteLine("Enter the 2nd number");
        var numTwo = Console.ReadLine();
        heap.Add(Int32.Parse(numTwo));
        Console.WriteLine("Enter the 3rd number");
        var numThree = Console.ReadLine();
        heap.Add(Int32.Parse(numThree));
        var min = heap.Pop();
        Console.WriteLine($"The minimum = {min}");
    }
}

//public class Node
//{
    //public Point Position { get; set; }
    //public int Cost { get; set; }
//}
public class Tree
{
    public int Value { get; set; }
    public List<Tree> Children { get; set; }
    public int Order { get; set; }

    public Tree (int value)
    {
        Value = value;
        Children = new List<Tree>();
        Order = 0;
    }

    public void AddToEnd(Tree tree)
    {
        Children.Add(tree);
        Order++;
    }
}

public class FibonacciHeap
{
    private List<Tree> trees;
    private Tree least;
    public int count { get; private set; }

    public FibonacciHeap()
    {
        trees = new List<Tree>();
        least = null;
        count = 0;
    }

    public void Add(int value)
    {
        var newTree = new Tree(value);
        trees.Add(newTree);
        if (least == null || value < least.Value)
        {
            least = newTree;
        }

        count++;
    }

    public int? Peek()
    {
        if (least == null)
        {
            return null;
        }

        return least.Value;
    }

    public int? Pop()
    {
        var smallest = least;
        if (smallest != null)
        {
            foreach (Tree child in smallest.Children)
            {
                trees.Add(child);
            }

            trees.Remove(smallest);

            if (trees.Count == 0)
            {
                least = null;
            }
            else
            {
                least = trees[0];
                Consolidate();
            }

            count--;
            return smallest.Value;
        }

        return null;
    }

    public void Consolidate()
    {
        int arraySize = FloorLog(count) + 1;
        Tree[] aux = new Tree[arraySize];
        while (trees.Count > 0)
        {
            Tree x = trees[0];
            int order = x.Order;
            trees.RemoveAt(0);

            while (aux[order] != null)
            {
                Tree y = aux[order];
                if (x.Value > y.Value)
                {
                    (x, y) = (y, x);
                }

                x.AddToEnd(y);
                aux[order] = null;
                order++;
            }

            aux[order] = x;
        }

        least = null;
        for (int i = 0; i < aux.Length; i++)
        {
            if (aux[i] != null)
            {
                trees.Add(aux[i]);
                if (least == null || aux[i].Value < least.Value)
                {
                    least = aux[i];
                }
            }
        }
    }

    private static int FloorLog(int x)
    {
        return (int)Math.Floor(Math.Log(x, 2));
    }
}
