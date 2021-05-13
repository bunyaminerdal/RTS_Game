using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitBoxController : MonoBehaviour
{    
    private Dictionary<PlayerUnitController,GameObject> imagedict;
    public GameObject unitImage;
    private GridLayoutGroup gridLayoutGroup;
    private GameObject newunitImage;
    private Button unitselectButton;
    private PlayerManager playerManager;


    // Start is called before the first frame update
    void Awake()
    {
        playerManager = PlayerManager.Instance;
        gridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
        imagedict = new Dictionary<PlayerUnitController, GameObject>();


    }

 
    public void onUnitSelected(PlayerUnitController unit, bool isSelected)
    {
        imagedict.TryGetValue(unit, out newunitImage);
        newunitImage.transform.GetChild(0).gameObject.SetActive(isSelected);

    }    
    void TaskOnClick(PlayerUnitController unit){

        //TODO: bu kısımda event ya da action kullanılabilir.
        if (playerManager == null) return;
        if(unit.isSelected()==false)
        {
            
           playerManager.SelectUnit(unit);
           //unit.SetSelected(true);
        }else if(unit.isSelected()==true)
        {
            unit.SetSelected(false);
            playerManager.DeselectUnit(unit);
        }
	}

     public void onUnitCreated(PlayerUnitController unit)
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

    public void onItemChange(PlayerUnitController unit, Texture2D unitTexture)
    {
        imagedict.TryGetValue(unit, out newunitImage);
        newunitImage.transform.GetChild(1).GetComponent<RawImage>().texture = unitTexture;
    }
}
