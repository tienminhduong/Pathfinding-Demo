using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    [SerializeField] GameObject vertexPrefab;
    [SerializeField] Text changeModeButtonText;
    [SerializeField] Toggle tgMode;
    [SerializeField] InputState state;


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
        Instantiate(vertexPrefab);
    }

    public void ChangeInputState()
    {
        if (state == InputState.CreateEdge)
        {
            state = InputState.MoveVertex;
            changeModeButtonText.text = "Chế độ Dời khối";
        }
        else
        {
            state = InputState.CreateEdge;
            changeModeButtonText.text = "Chế độ Tạo đường";
        }
    }
}

public enum InputState
{
    CreateEdge,
    MoveVertex
}
