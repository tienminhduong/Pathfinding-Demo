using System;
using System.Collections.Generic;

class EntryAT : IComparable<EntryAT>
{
    public int cost;
    public int id;
    public int v;
    public int CompareTo(EntryAT other)
    {
        if (cost != other.cost) return cost.CompareTo(other.cost);
        return id.CompareTo(other.id);
    }
}

class EntryAKT : IComparable<EntryAKT>
{
    public int f;
    public int id;
    public int v;
    public int g;
    public int CompareTo(EntryAKT other)
    {
        if (f != other.f) return f.CompareTo(other.f);
        return id.CompareTo(other.id);
    }
}

class EntryAStar : IComparable<EntryAStar>
{
    public int f;
    public int id;
    public int v;
    public int g;
    public int parent;
    public int CompareTo(EntryAStar other)
    {
        if (f != other.f) return f.CompareTo(other.f);
        return id.CompareTo(other.id);
    }
}

class Algorithms
{
    public static int[,] To2DArray(List<List<int>> list)
    {
        int rows = list.Count;
        int cols = list[0].Count;
        int[,] array = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                array[i, j] = list[i][j];
            }
        }
        return array;
    }
    static int Heuristic(int u, int goal)
    {
        return Math.Abs(u - goal); //thay d?i th�nh t�nh kho?ng c�ch t? di?m u -> goal b?ng c�ng th?c t?a d? tr? nhau g� d�
    }

    public static List<int> AT(int[,] graph, int start, int goal)
    {
        int n = graph.GetLength(0);
        int[] dist = new int[n];
        int[] parent = new int[n];
        for (int i = 0; i < n; i++) { dist[i] = int.MaxValue; parent[i] = -1; }

        var open = new SortedSet<EntryAT>();
        int idCounter = 0;
        dist[start] = 0;
        open.Add(new EntryAT { cost = 0, id = idCounter++, v = start });

        while (open.Count > 0)
        {
            var top = open.Min;
            open.Remove(top);
            int d = top.cost;
            int u = top.v;
            if (d != dist[u]) continue;
            if (u == goal) break;

            for (int v = 0; v < n; v++)
            {
                if (graph[u, v] > 0)
                {
                    int nd = d + graph[u, v];
                    if (nd < dist[v])
                    {
                        dist[v] = nd;
                        parent[v] = u;
                        open.Add(new EntryAT { cost = nd, id = idCounter++, v = v });
                    }
                }
            }
        }

        Console.Write("AT path: ");
        if (dist[goal] == int.MaxValue) return new();
        return PrintPath(parent, goal);
        //Console.WriteLine("Cost = " + dist[goal]);
    }

    public static List<int> AKT(int[,] graph, int start, int goal)
    {
        int n = graph.GetLength(0);
        int[] g = new int[n];
        int[] parent = new int[n];
        int[] h = new int[n];
        for (int i = 0; i < n; i++)
        {
            g[i] = int.MaxValue;
            parent[i] = -1;
            h[i] = Heuristic(i, goal);
        }

        var open = new SortedSet<EntryAKT>();
        int idCounter = 0;
        g[start] = 0;
        open.Add(new EntryAKT { f = g[start] + h[start], id = idCounter++, v = start, g = 0 });

        var expandedOrder = new List<int>();

        while (open.Count > 0)
        {
            var top = open.Min;
            open.Remove(top);
            int curV = top.v;
            int curG = top.g;
            if (curG != g[curV]) continue;

            expandedOrder.Add(curV);
            if (curV == goal) break;

            for (int v = 0; v < n; v++)
            {
                if (graph[curV, v] > 0)
                {
                    int newg = curG + graph[curV, v];
                    if (newg < g[v])
                    {
                        g[v] = newg;
                        parent[v] = curV;
                        open.Add(new EntryAKT { f = newg + h[v], id = idCounter++, v = v, g = newg });
                    }
                }
            }
        }

        Console.Write("AKT expanded order: ");
        foreach (var x in expandedOrder) Console.Write(x + " ");
        Console.WriteLine();

        if (g[goal] == int.MaxValue) return new();
        return PrintPath(parent, goal);
    }

    public static List<int> Astar(int[,] graph, int start, int goal)
    {
        int n = graph.GetLength(0);
        int[] g = new int[n];
        int[] parent = new int[n];
        bool[] closed = new bool[n];
        for (int i = 0; i < n; i++) { g[i] = int.MaxValue; parent[i] = -1; closed[i] = false; }

        var open = new SortedSet<EntryAStar>();
        int idCounter = 0;
        g[start] = 0;
        open.Add(new EntryAStar { f = g[start] + Heuristic(start, goal), id = idCounter++, v = start, g = 0, parent = -1 });

        while (open.Count > 0)
        {
            var top = open.Min;
            open.Remove(top);
            int curV = top.v;
            int curG = top.g;
            int curParent = top.parent;
            if (curG != g[curV]) continue;
            if (closed[curV]) continue;
            closed[curV] = true;
            parent[curV] = curParent;
            if (curV == goal) break;

            for (int v = 0; v < n; v++)
            {
                if (graph[curV, v] > 0)
                {
                    int newg = curG + graph[curV, v];
                    if (newg < g[v])
                    {
                        g[v] = newg;
                        parent[v] = curV;
                        if (closed[v]) closed[v] = false;
                        int h = Heuristic(v, goal);
                        open.Add(new EntryAStar { f = newg + h, id = idCounter++, v = v, g = newg, parent = curV });
                    }
                }
            }
        }

        Console.Write("A* path: ");
        if (g[goal] == int.MaxValue) return new();
        return PrintPath(parent, goal);
    }

    static List<int> PrintPath(int[] parent, int goal)
    {
        List<int> path = new List<int>();
        for (int v = goal; v != -1; v = parent[v]) path.Add(v);
        path.Reverse();
        return path;
        //foreach (int x in path) Console.Write(x + " ");
        //Console.WriteLine();
    }

    //static void Main()
    //{
    //    Console.Write("Nhap so dinh: ");
    //    int n = int.Parse(Console.ReadLine() ?? "0");
    //    int[,] graph = new int[n, n];

    //    Console.WriteLine("Nhap ma tran ke (" + n + "x" + n + "):");
    //    for (int i = 0; i < n; i++)
    //    {
    //        string line = Console.ReadLine();
    //        string[] tokens = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
    //        for (int j = 0; j < n; j++) graph[i, j] = int.Parse(tokens[j]);
    //    }

    //    Console.Write("Nhap start va goal: ");
    //    string[] sg = Console.ReadLine().Split();
    //    int start = int.Parse(sg[0]);
    //    int goal = int.Parse(sg[1]);

    //    AT(graph, start, goal);
    //    AKT(graph, start, goal);
    //    Astar(graph, start, goal);
    //}
}

