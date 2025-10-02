using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public static UnityAction<Vertex> OnStartVertexSelected;
    public static UnityAction<Vertex> OnGoalVertexSelected;

    public static IEnumerator FlashVerticies(List<int> verticies)
    {
        foreach (var v in verticies)
        {
            var vertex = VerticesManager.Instance.Vertices[v];
            var spr = vertex.GetComponent<SpriteRenderer>();
            Color originalColor = spr.color;
            Debug.Log($"Visited vertex: {v}");
            spr.color = Color.red; // Flash to yellow
            yield return new WaitForSeconds(1f);
            spr.color = originalColor; // Restore color
            yield return new WaitForSeconds(0.2f);
        }
    }

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
        else if (VerticesManager.Instance.CurrentInputState == InputState.SelectStartingPoint)
        {
            VerticesManager.Instance.CreateBubbles(transform.position);
            OnStartVertexSelected?.Invoke(this);
        }
        else if (VerticesManager.Instance.CurrentInputState == InputState.SelectEndingPoint)
        {
            OnGoalVertexSelected?.Invoke(this);
            spr.sprite = highLightSprite;
        }
    }

    public void Unselect()
    {
        isSelected = false;
        OnVertexDeselected?.Invoke(this);
        spr.sprite = normalSprite;
    }    
}
