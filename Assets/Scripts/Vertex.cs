using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Vertex : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private bool isDragging = false;
    private Vector3 offset; // Offset between mouse position and object's pivot


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
        Debug.Log("hehe");
        isDragging = true;
        Vector2 mouseScreen = Mouse.current.position.ReadValue();
        offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreen.x, mouseScreen.y, Camera.main.WorldToScreenPoint(transform.position).z)
        );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("huhu");
        isDragging = false;
    }
}
