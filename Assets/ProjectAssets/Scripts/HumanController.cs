using UnityEngine;

public class HumanController : MonoBehaviour
{
    public Transform footwearPivot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
