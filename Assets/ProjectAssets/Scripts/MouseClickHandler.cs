using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseClickHandler : MonoBehaviour
{
    public GameObject roadTypeMenu;
    void Start()
    {
        roadTypeMenu.SetActive(false);
    }

    void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            ShowContextMenu();
        }

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                HideContextMenu();
            }
        }    
    }

    private void HideContextMenu()
    {
        if (roadTypeMenu.activeSelf)
        {
            roadTypeMenu.SetActive(false);
        }
    }

    private void ShowContextMenu()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        roadTypeMenu.SetActive(true);
        roadTypeMenu.transform.position = mousePos;
    }
}
