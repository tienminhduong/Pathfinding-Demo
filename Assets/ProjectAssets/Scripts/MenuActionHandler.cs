using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuActionHandler : MonoBehaviour
{
    public MapGenerator mapGenerator = null;
    public TMP_InputField inpRow = null;
    public TMP_InputField inpCol = null;
    public Button btn_Start = null;
    public Button btn_GenerateHuman = null;

    GameObject selectedCell = null;

    public GameObject humanPrefab;

    private void OnEnable()
    {
        CellSelector.OnCellSelected += SelectCell;
        CellSelector.OnCellDeselected += DeselectCell;
    }

    private void OnDisable()
    {
        CellSelector.OnCellSelected -= SelectCell;
        CellSelector.OnCellDeselected -= DeselectCell;
    }

    void Start()
    {
        inpCol.onValueChanged.AddListener(onInputChange);
        inpRow.onValueChanged.AddListener(onInputChange);

        btn_GenerateHuman.interactable = false;
    }

    private void onInputChange(string arg0)
    {
        string strRow = inpRow.text;
        string strCol = inpCol.text;
        try
        {
            int row = int.Parse(strRow);
            int col = int.Parse(strCol);
            mapGenerator.destroyOldMap();
            mapGenerator.generate(row, col);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void generateAfterClick()
    {
        string strRow = inpRow.text;
        string strCol = inpCol.text;
        try
        {
            int row = int.Parse(strRow);
            int col = int.Parse(strCol);
            mapGenerator.destroyOldMap();
            mapGenerator.generate(row, col);
        }
        catch(Exception e) 
        {
            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectCell(GameObject cell)
    {
        selectedCell = cell;
        btn_GenerateHuman.interactable = true;
    }

    void DeselectCell()
    {
        selectedCell = null;
        btn_GenerateHuman.interactable = false;
    }

    public void GenerateHuman()
    {
        Debug.Log("Function called");
        if (selectedCell == null || humanPrefab == null)
        {
            Debug.LogError("No cell selected or humanPrefab is null");
            return;
        }
        Debug.Log("Human Generated");
        Vector3 position = selectedCell.transform.position;
        Instantiate(humanPrefab, position, Quaternion.identity);
    }
}
