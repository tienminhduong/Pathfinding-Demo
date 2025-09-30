using Assets.ProjectAssets.ConstantValue;
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
           
            if (cellSelected != null)
            {
                mapGenerator.changeTo(cellSelected, ConstantValue.RoadType.DirtRoad);
            }
        });

        btnAsphaltRoad.onClick.AddListener(() =>
        {
            GameObject cellSelected = cellSelector.SelectedObject;

            if (cellSelected != null)
            {
                mapGenerator.changeTo(cellSelected, ConstantValue.RoadType.AsphaltRoad);
            }
        });

        btnCrowdedRoad.onClick.AddListener(() =>
        {
            GameObject cellSelected = cellSelector.SelectedObject;

            if (cellSelected != null)
            {
                mapGenerator.changeTo(cellSelected, ConstantValue.RoadType.CrowdedRoad);
            }
        });

        btnBarrier.onClick.AddListener(() =>
        {
            GameObject cellSelected = cellSelector.SelectedObject;

            if (cellSelected != null)
            {
                mapGenerator.changeTo(cellSelected, ConstantValue.RoadType.Barrier);
            }
        });


    }

    void Update()
    {
        
    }
}
