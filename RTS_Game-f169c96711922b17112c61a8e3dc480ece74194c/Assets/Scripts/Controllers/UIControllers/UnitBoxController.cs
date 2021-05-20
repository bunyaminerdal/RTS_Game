using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitBoxController : MonoBehaviour
{
    private Dictionary<PlayerUnitController, GameObject> imagedict;
    public GameObject unitImage;
    private GridLayoutGroup gridLayoutGroup;
    private GameObject newunitImage;
    private Button unitselectButton;



    // Start is called before the first frame update
    void Awake()
    {
        gridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
        imagedict = new Dictionary<PlayerUnitController, GameObject>();


    }
    private void OnEnable()
    {
        SaveLoadHandlers.UnitFrameClearBeforeUnitCreated.AddListener(beforeUnitsCreated);
        SaveLoadHandlers.UnitFrameClearAfterUnitCreated.AddListener(onUnitCreated);
    }
    private void OnDisable()
    {
        SaveLoadHandlers.UnitFrameClearBeforeUnitCreated.RemoveListener(beforeUnitsCreated);
        SaveLoadHandlers.UnitFrameClearAfterUnitCreated.RemoveListener(onUnitCreated);
    }

    public void onUnitSelected(PlayerUnitController unit, bool isSelected)
    {
        imagedict.TryGetValue(unit, out newunitImage);
        newunitImage.transform.GetChild(0).gameObject.SetActive(isSelected);

    }


    public void onUnitCreated(PlayerUnitController unit)
    {
        newunitImage = Instantiate(unitImage, transform);
        TMP_Text text1 = newunitImage.transform.GetChild(2).GetComponent<TMP_Text>();
        text1.text = unit.unitName;
        unitselectButton = newunitImage.GetComponentInChildren<Button>();
        //unitselectButton.onClick.AddListener(delegate { TaskOnClick(unit); });
        unitselectButton.onClick.AddListener(() =>
        {
            UnitFrameEventHandler.UnitFrameClicked?.Invoke(unit);
        });

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

    private void beforeUnitsCreated()
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
