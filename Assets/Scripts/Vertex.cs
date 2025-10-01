using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Vertex : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    private bool isDragging = false;
    private Vector3 offset; // Offset between mouse position and object's pivot
    private Sprite normalSprite;
    [SerializeField] Sprite highLightSprite;
    private bool isSelected = false;
    private SpriteRenderer spr = null;

    public static UnityAction<Vertex> OnVertexSelected;
    public static UnityAction<Vertex> OnVertexDeselected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spr = gameObject.GetComponent<SpriteRenderer>();
        normalSprite = spr.sprite;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mouseScreen = Mouse.current.position.ReadValue(); // new API
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(mouseScreen.x, mouseScreen.y, Camera.main.WorldToScreenPoint(transform.position).z)
            );
    
            transform.position = mouseWorld + offset;
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (VerticesManager.Instance.CurrentInputState == InputState.CreateEdge)
            return;

        isDragging = true;
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreen.x, mouseScreen.y, Camera.main.WorldToScreenPoint(transform.position).z)
        );
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (VerticesManager.Instance.CurrentInputState == InputState.CreateEdge)
        {
            isSelected = !isSelected;

            if (isSelected)
            {
                spr.sprite = highLightSprite;
                OnVertexSelected?.Invoke(this);
            }
            else
            {
                spr.sprite = normalSprite;
                OnVertexDeselected?.Invoke(this);
            }
        }
            
    }

    public void Unselect()
    {
        isSelected = false;
        OnVertexDeselected?.Invoke(this);
        spr.sprite = normalSprite;
    }    
}
