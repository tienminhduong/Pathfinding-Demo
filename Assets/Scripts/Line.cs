using TMPro;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weightText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLine(Vector3 start, Vector3 end)
    {
        var lr = gameObject.GetComponent<LineRenderer>();
        transform.GetChild(0).transform.position
            = (start + end) / 2f;
        lr.positionCount = 2;
        lr.widthMultiplier = 0.3f;
        lr.useWorldSpace = true;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        var distance = Vector3.Distance(start, end);
        weightText.text = distance.ToString("F0");
    }
}
