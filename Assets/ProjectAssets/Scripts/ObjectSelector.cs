using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelector : MonoBehaviour
{
    private List<GameObject> selectedObjects = new List<GameObject>();
    public List<GameObject> SelectedObjects { get => selectedObjects; }
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObj = hit.collider.gameObject;
                bool isCtrlPressed = Keyboard.current.ctrlKey.isPressed;

                if (!isCtrlPressed)
                { 
                    DeselectAll();
                }

                if (!selectedObjects.Contains(clickedObj))
                {
                    OnObjectSelected(clickedObj);
                }
                else if (isCtrlPressed)
                {
                    OnObjectDeselected(clickedObj);
                }
            }
            else
            {
                DeselectAll();
            }
        }
    }

    private void DeselectAll()
    {
        Renderer renderer = null;
        foreach (GameObject obj in selectedObjects)
        {
            renderer = obj.GetComponent<Renderer>();
            if(renderer != null)
            {
                renderer.material.DisableKeyword("_EMISSION");
            }    
        }
        selectedObjects.Clear();
    }

    private void OnObjectDeselected(GameObject obj)
    {
        selectedObjects.Remove(obj);
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }    
    

    }
    
    private void OnObjectSelected(GameObject selectedObject)
    {
        this.selectedObjects.Add(selectedObject);
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }
    }

}
