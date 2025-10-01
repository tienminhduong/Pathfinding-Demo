using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;


public class VerticesManager : MonoBehaviour
{
    #region Singleton
    public static VerticesManager Instance { get; private set; }

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
    [SerializeField] Vertex vertexPrefab;
    [SerializeField] Text changeModeButtonText;
    [SerializeField] Toggle tgMode;
    [SerializeField] InputState state;

    [SerializeField] Bubbles bubblesPrefab;

    public List<Vertex> Vertices = new();


    public InputState CurrentInputState => state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = InputState.MoveVertex;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateVertex()
    {
        tgMode.isOn = true;
        Vertices.Add(Instantiate(vertexPrefab));
    }

    public void ChangeInputState()
    {
        if (state == InputState.CreateEdge)
        {
            state = InputState.MoveVertex;
            changeModeButtonText.text = "Chế độ Dời khối";
        }
        else if (state == InputState.MoveVertex)
        {
            state = InputState.CreateEdge;
            changeModeButtonText.text = "Chế độ Tạo đường";
        }
    }

    public void EnableCreateBubbles()
    {
        state = InputState.SelectStartingPoint;
    }

    public void CreateBubbles(Vector3 position)
    {
        Instantiate(bubblesPrefab, position, Quaternion.identity);
        state = InputState.MoveVertex;
    }
}

public enum InputState
{
    CreateEdge,
    MoveVertex,
    SelectStartingPoint,
    SelectEndingPoint
}
