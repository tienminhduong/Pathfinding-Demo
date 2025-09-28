using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int columns; // Oz
    public int rows; // Ox
    public float cellSize = 1f;

    public GameObject dirtRoad;
    public GameObject asphaltRoad;
    public Transform parent;

    void Start()
    {
        
    }

    private void GenerateRandomMap()
    {
        System.Random rd = new System.Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(i * cellSize, 0, -j * cellSize);
                int type = rd.Next(0, 2);
                GameObject gameObject = type == 0 ? dirtRoad : asphaltRoad;
                GameObject initializedObject = Instantiate(gameObject);
                initializedObject.transform.localScale = new Vector3(cellSize, 0.25f * cellSize, cellSize);
                initializedObject.transform.position = position;

            }
        }

    }

    void Update()
    {

    }

    internal void generate(int row, int col)
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Vector3 position = new Vector3(i * cellSize, 0, j * cellSize);
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
}
