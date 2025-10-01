
using System.Collections.Generic;
using UnityEngine;

public class EdgeDrawer : MonoBehaviour
{
    List<Vertex> connectedVertices = new();
    [SerializeField] Line linePrefab;
    List<Vertex> vertexStack = new();

    private void Start()
    {
    }

    private void OnEnable()
    {
        Vertex.OnVertexSelected += AddVertex;
        Vertex.OnVertexDeselected += RemoveVertex;
    }

    private void OnDisable()
    {
        Vertex.OnVertexSelected -= AddVertex;
        Vertex.OnVertexDeselected -= RemoveVertex;
    }

    private void AddEdge(Vertex v1, Vertex v2)
    {
        var line = Instantiate(linePrefab);
        line.InitLine(v1.transform.position, v2.transform.position);

        connectedVertices.Add(v1);
        connectedVertices.Add(v2);
    }

    private void AddVertex(Vertex v)
    {
        vertexStack.Add(v);
        if (vertexStack.Count == 2)
        {
            var v1 = vertexStack[0];
            var v2 = vertexStack[1];
            vertexStack.Clear();
            AddEdge(v1, v2);
            v1.Unselect();
            v2.Unselect();
        }
    }

    private void RemoveVertex(Vertex v)
    {
        if (vertexStack.Contains(v))
            vertexStack.Remove(v);
    }

}