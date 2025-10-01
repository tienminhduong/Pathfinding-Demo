using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField] List<int> path;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.DOMove()
    }


    public void Move()
    {
        if (path.Count < 2) return;
        Vector3[] waypoints = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            waypoints[i] = VerticesManager.Instance.Vertices[path[i]].transform.position;
        }
        var seq = DOTween.Sequence();
        seq.Append(transform.DOPath(waypoints, waypoints.Length, PathType.Linear).SetEase(Ease.Linear));
    }
}
