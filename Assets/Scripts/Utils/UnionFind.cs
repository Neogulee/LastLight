using System.Collections.Generic;
using System.Linq;


public class UnionFind
{
    private List<int> parents;
    public UnionFind(int size)
    {
        parents = Enumerable.Range(0, size).ToList();
    }

    public void increase()
    {
        parents.Add(parents.Count);
    }

    public int find(int node)
    {
        int parent = parents[node];
        if (parent == node)
            return node;
        return parents[node] = find(parent);
    }

    public bool merge(int a, int b)
    {
        int root_a = find(a);
        int root_b = find(b);
        if (root_a == root_b)
            return false;
        parents[root_a] = root_b;
        return true;
    }
}