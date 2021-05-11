using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitBoxController : MonoBehaviour
{    
    private Dictionary<UnitController,GameObject> imagedict;
    public GameObject unitImage;
    private GridLayoutGroup gridLayoutGroup;
    private GameObject newunitImage;
    private Button unitselectButton;

    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
        imagedict = new Dictionary<UnitController, GameObject>();


    }

 
    public void onUnitSelected(UnitController unit, bool isSelected)
    {
        imagedict.TryGetValue(unit, out newunitImage);
        newunitImage.transform.GetChild(0).gameObject.SetActive(isSelected);

    }    
    void TaskOnClick(UnitController unit){
        if(unit.isSelected()==false)
        {
           playerManager.selectedUnits.Add(unit);
           unit.SetSelected(true);
        }else if(unit.isSelected()==true)
        {
            unit.SetSelected(false);
            playerManager.selectedUnits.Remove(unit);
        }
	}

     public void onUnitCreated(UnitController unit)
    {        
        newunitImage = Instantiate(unitImage, transform);
        TMP_Text text1 = newunitImage.transform.GetChild(2).GetComponent<TMP_Text>();
        text1.text = unit.unitName;
        unitselectButton = newunitImage.GetComponentInChildren<Button>();
        unitselectButton.onClick.AddListener(delegate { TaskOnClick(unit); });

        imagedict.Add(unit, newunitImage);
        if (imagedict.Count <= 8)
        {
            gridLayoutGroup.cellSize = new Vector2(100f, 100f);
            gridLayoutGroup.constraintCount = 8;
        }
        else if (imagedict.Count <= 12)
        {
            gridLayoutGroup.cellSize = new Vector2(80f, 80f);
            gridLayoutGroup.constraintCount = 12;
        }
        else if (imagedict.Count <= 16)
        {
            gridLayoutGroup.cellSize = new Vector2(60f, 60f);
            gridLayoutGroup.constraintCount = 16;
        }
        else if (imagedict.Count <= 20)
        {
            gridLayoutGroup.cellSize = new Vector2(45f, 45f);
            gridLayoutGroup.constraintCount = 20;
        }

    }

    public void beforeUnitsCreated()
    {
        foreach (var item in imagedict)
        {
            Destroy(item.Value);
        }
        imagedict.Clear();
    }

    public void onItemChange(UnitController unit, Texture2D unitTexture)
    {
        imagedict.TryGetValue(unit, out newunitImage);
        newunitImage.transform.GetChild(1).GetComponent<RawImage>().texture = unitTexture;
    }
}
