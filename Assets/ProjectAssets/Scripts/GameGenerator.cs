using UnityEngine;

public class GameGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int columns;
    public int rows;
    public float cellSize = 1f;

    public GameObject dirtRoad;
    public GameObject asphaltRoad;

    void Start()
    {
        GenerateRandomMap();
    }

    private void GenerateRandomMap()
    {
        System.Random rd = new System.Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(i * cellSize, 0, j * cellSize);
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
}
