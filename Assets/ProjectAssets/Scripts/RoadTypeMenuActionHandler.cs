using UnityEngine;
using UnityEngine.UI;

public class RoadTypeMenuActionHandler : MonoBehaviour
{
    public Button btnDirtRoad;
    public Button btnAsphaltRoad;
    public Button btnCrowdedRoad;
    public Button btnBarrier;
    public MapGenerator mapGenerator;
    public CellSelector cellSelector;
    void Start()
    {
        btnDirtRoad.onClick.AddListener(() =>
        {
            GameObject cellSelected = cellSelector.SelectedObject;
            Debug.Log(cellSelected == null ? "Null" : cellSelected.transform.position.ToString());
            if (cellSelected != null)
            {
                mapGenerator.changeTo(cellSelected, "Dirt");
            }
        });
    }

    void Update()
    {
        
    }
}
