using UnityEngine;

public class EdgeDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component if not assigned in Inspector
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        // Example: Drawing a simple two-point line
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(-2, 0, 0)); // Start point
        lineRenderer.SetPosition(1, new Vector3(2, 0, 0));  // End point
        lineRenderer.widthMultiplier = 0.15f;
    }

    // Example: Updating line points dynamically
    public void UpdateLine(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
    }
}

/*
 * public class MultiLineSeparate : MonoBehaviour
{
    public Transform[] startPoints;
    public Transform[] endPoints;
    public GameObject linePrefab; // prefab with LineRenderer

    void Start()
    {
        for (int i = 0; i < Mathf.Min(startPoints.Length, endPoints.Length); i++)
        {
            var line = Instantiate(linePrefab);
            var lr = line.GetComponent<LineRenderer>();

            lr.positionCount = 2;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.useWorldSpace = true;

            lr.SetPosition(0, startPoints[i].position);
            lr.SetPosition(1, endPoints[i].position);
        }
    }
}

*/