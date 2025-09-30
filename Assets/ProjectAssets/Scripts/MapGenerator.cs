using Assets.ProjectAssets.ConstantValue;
using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int columns; // Oz
    public int rows; // Ox
    public int cellSize = 1;

    public GameObject dirtRoad;
    public GameObject asphaltRoad;
    public GameObject crowdedRoad;
    public GameObject barrier;
    public Transform parent;

    private int[,] map = null;
    public int[,] Map { get => map; }

    public HumanController humanPrefab;
    public HumanController humanInstance = null;

    void Start()
    {
        
    }

    void Update()
    {

    }

    internal void generate(int row, int col)
    {
        map = new int[row, col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                map[i, j] = (int)(ConstantValue.RoadType.AsphaltRoad);
                Vector3 position = new Vector3(-i * cellSize, 0, j * cellSize);
                GameObject gameObject = asphaltRoad;
                GameObject initializedObject = Instantiate(gameObject, parent);
                initializedObject.transform.localScale = new Vector3(cellSize, 0.25f * cellSize, cellSize);
                initializedObject.transform.position = position;
            }
        }
    }

    internal void destroyOldMap()
    {
        foreach(Transform i in parent)
        {
            Destroy(i.gameObject);
        }
    }

    internal void changeTo(GameObject cellSelected, ConstantValue.RoadType type)
    {
        Vector3 position = cellSelected.transform.position;
        Vector3 scale = cellSelected.transform.localScale;
        int i = -(int)position.x / cellSize;
        int j = (int)position.z / cellSize;    
        map[i, j] = (int)(type);
        GameObject initializedObject = null;
        if (type == ConstantValue.RoadType.DirtRoad)
        {
           
            initializedObject = Instantiate(dirtRoad, parent);
           
        }
        else if (type == ConstantValue.RoadType.AsphaltRoad)
        {
            initializedObject = Instantiate(asphaltRoad, parent);
        }
        else if (type == ConstantValue.RoadType.CrowdedRoad)
        {
            initializedObject = Instantiate(crowdedRoad, parent);
        }  
        else if(type == ConstantValue.RoadType.Barrier)
        {
            Destroy(cellSelected);
            initializedObject = Instantiate(barrier, parent);
        }

        initializedObject.transform.localScale = scale;
        initializedObject.transform.position = position;

        Destroy(cellSelected);
    }
}
