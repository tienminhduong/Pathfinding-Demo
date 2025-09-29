using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelector : MonoBehaviour
{
    private GameObject selectedObject;
    public GameObject SelectedObject { get => selectedObject; }

    private Camera cam;
    private Material selectedMaterial;

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
                OnObjectSelected(hit.collider.gameObject);
            }
            else
            {
                OnObjectDeselected();
            }
        }
    }

    private void OnObjectDeselected()
    {
        if (selectedMaterial != null)
        {
            selectedMaterial.DisableKeyword("_EMISSION");

            selectedMaterial = null;
     
        }
        selectedObject = null;

    }
    
    private void OnObjectSelected(GameObject selectedObject)
    {
        OnObjectDeselected();
        this.selectedObject = selectedObject;
        Renderer renderer = selectedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            selectedMaterial = renderer.material;

            selectedMaterial.EnableKeyword("_EMISSION");
        }
    }


}
