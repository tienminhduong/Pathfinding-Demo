using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CellSelector : MonoBehaviour
{
    private GameObject selectedObject;
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
                OnObjectSelected(clickedObj);
            }
            else
            {
                OnObjectDeselected();
            }
        }
    }

    private void OnObjectSelected(GameObject obj)
    {
        OnObjectDeselected();
        selectedObject = obj;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.EnableKeyword("_EMISSION");
        }    
    

    }
    
    private void OnObjectDeselected()
    {
        if(selectedObject == null)
        {
            return;
        }
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.DisableKeyword("_EMISSION");
        }
        selectedObject = null;
    }

}
