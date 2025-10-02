using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField] List<int> path;
    [SerializeField] List<int> points;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.DOMove()
    }

    private void SelectATPAth()
    {
        path = AlgorithmHandler.Instance.ATPath;
        points = AlgorithmHandler.Instance.ATVisitedOrder;
        Move();
    }

    private void SelectAKTPath()
    {
        path = AlgorithmHandler.Instance.AKTPath;
        points = AlgorithmHandler.Instance.AKTVisitedOrder;
        Move();
    }

    private void SelectAStarPath()
    {
        path = AlgorithmHandler.Instance.AStarPath;
        points = AlgorithmHandler.Instance.AStarVisitedOrder;
        Move();
    }

    private void OnEnable()
    {
        AlgorithmHandler.OnATPressed += SelectATPAth;
        AlgorithmHandler.OnAKTPressed += SelectAKTPath;
        AlgorithmHandler.OnAStarPressed += SelectAStarPath;
    }

    private void OnDisable()
    {
        AlgorithmHandler.OnATPressed -= SelectATPAth;
        AlgorithmHandler.OnAKTPressed -= SelectAKTPath;
        AlgorithmHandler.OnAStarPressed -= SelectAStarPath;
    }


    public void Move()
    {
        Debug.Log("Run");
        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        // Wait for flashing to finish
        yield return StartCoroutine(Vertex.FlashVerticies(points));

        // Then start moving
        if (path.Count < 2) yield break;
        Vector3[] waypoints = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            waypoints[i] = VerticesManager.Instance.Vertices[path[i]].transform.position;
        }
        var seq = DG.Tweening.DOTween.Sequence();
        seq.Append(transform.DOPath(waypoints, waypoints.Length, PathType.Linear).SetEase(Ease.Linear));
    }
}
