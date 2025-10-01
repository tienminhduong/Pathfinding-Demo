using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlgorithmHandler : MonoBehaviour
{
    #region Singleton
    public static AlgorithmHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
    }
    #endregion
    int[,] graph;
    int start;
    int goal;

    public List<int> ATPath;
    public List<int> AKTPath;
    public List<int> AStarPath;

    public static UnityAction OnATPressed;
    public static UnityAction OnAKTPressed;
    public static UnityAction OnAStarPressed;

    void AddNewEdge(Vertex v1, Vertex v2, int weight)
    {
        int v1Index = VerticesManager.Instance.Vertices.IndexOf(v1);
        int v2Index = VerticesManager.Instance.Vertices.IndexOf(v2);
        graph[v1Index, v2Index] = weight;
        graph[v2Index, v1Index] = weight;
    }

    public void InvokeAT() => OnATPressed?.Invoke();
    public void InvokeAKT() => OnAKTPressed?.Invoke();
    public void InvokeAStar() => OnAStarPressed?.Invoke();

    public void AddStartVertex(Vertex v)
    {
        start = VerticesManager.Instance.Vertices.IndexOf(v);
    }

    public void AddGoalVertex(Vertex v)
    {
        goal = VerticesManager.Instance.Vertices.IndexOf(v);
        Calculate();
    }

    private void OnEnable()
    {
        EdgeDrawer.OnEdgeCreated += AddNewEdge;
        Vertex.OnStartVertexSelected += AddStartVertex;
        Vertex.OnGoalVertexSelected += AddGoalVertex;
    }
    private void OnDisable()
    {
        EdgeDrawer.OnEdgeCreated -= AddNewEdge;
        Vertex.OnStartVertexSelected -= AddStartVertex;
        Vertex.OnGoalVertexSelected -= AddGoalVertex;
    }

    public void Init()
    {
        var numberVertices = VerticesManager.Instance.Vertices.Count;
        graph = new int[numberVertices, numberVertices];
    }

    public void Calculate()
    {
        ATPath = Algorithms.AT(graph, start, goal);
        AKTPath = Algorithms.AKT(graph, start, goal);
        AStarPath = Algorithms.Astar(graph, start, goal);
    }
}

