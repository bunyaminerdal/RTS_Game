using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private PlayerUnitController[] units;
    private Interactable[] interacts;
    private Vector3 playerManagerTransform;
    private float playerManagerRotationY;
    private float virtualCamOffset;

    public List<GameData> gameDataList { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    private void OnEnable()
    {
        MenuEventHandler.QuickSaveClicked.AddListener(QuickSave);
        MenuEventHandler.QuickLoadClicked.AddListener(QuickLoad);
        SaveLoadHandlers.PlayerManagerTransform.AddListener(PlayerManagerTransform);
        SaveLoadHandlers.PlayerManagerRotationY.AddListener(PlayerManagerRotationY);
        SaveLoadHandlers.VirtualCamOffset.AddListener(VirtualCamOffset);
    }


    private void OnDisable()
    {
        MenuEventHandler.QuickSaveClicked.RemoveListener(QuickSave);
        MenuEventHandler.QuickLoadClicked.RemoveListener(QuickLoad);
        SaveLoadHandlers.PlayerManagerTransform.RemoveListener(PlayerManagerTransform);
        SaveLoadHandlers.PlayerManagerRotationY.RemoveListener(PlayerManagerRotationY);
        SaveLoadHandlers.VirtualCamOffset.RemoveListener(VirtualCamOffset);
    }
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        gameDataList = new List<GameData>();
        if (File.Exists(Application.persistentDataPath + "/" + "SaveGamedata.buner"))
        {
            LoadGameData();
            //TODO: loadgame data yaptıktan sonra file exists mi diye bakmak lazım.
        }
        else
        {
            SaveGameData();
        }

    }

    public void Save(string gameName)
    {
        units = PlayerUnitController.AllPlayerUnits.ToArray();
        interacts = Interactable.AllInteractables.ToArray();
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + gameName + ".buner", FileMode.Create);

            SaveData data = new SaveData();

            SaveUnit(data);

            bf.Serialize(file, data);

            file.Close();
            bool autosavevar = false;
            //autosave i game data ya saveliyoruz
            for (int i = 0; i < gameDataList.Count; i++)
            {
                if (gameDataList[i].gameName == gameName)
                {
                    autosavevar = true;
                    gameDataList[i].gameTime = DateTime.Now.ToString();
                }
            }
            if (!autosavevar)
            {
                gameDataList.Add(new GameData(gameName, DateTime.Now.ToString()));
            }
            SaveGameData();
        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }

    }

    public void Load(string gameName)
    {

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + gameName + ".buner", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();
            LoadUnit(data);

        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }
    }

    public void QuickSave()
    {
        units = PlayerUnitController.AllPlayerUnits.ToArray();
        interacts = Interactable.AllInteractables.ToArray();
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "QuickSave.buner", FileMode.Create);

            SaveData data = new SaveData();

            SaveUnit(data);

            bf.Serialize(file, data);

            file.Close();
            bool haveAutoSaveFile = false;
            //autosave i game data ya saveliyoruz
            for (int i = 0; i < gameDataList.Count; i++)
            {
                if (gameDataList[i].gameName == "QuickSave")
                {
                    haveAutoSaveFile = true;
                    gameDataList[i].gameTime = DateTime.Now.ToString();
                }
            }
            if (!haveAutoSaveFile)
            {
                gameDataList.Add(new GameData("QuickSave", DateTime.Now.ToString()));
            }
            SaveGameData();
        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }

    }

    private void SaveUnit(SaveData data)
    {
        data.myControllerData = new ControllerData(Time.timeScale,
        playerManagerTransform.x,
        playerManagerTransform.y,
        playerManagerTransform.z,
        playerManagerRotationY,
        virtualCamOffset
        );
        for (int i = 0; i < interacts.Length; i++)
        {

            GameObject realInteractobject = interacts[i].gameObject;

            data.myInteractsData.Add(new InteractableData(interacts[i].interactName, interacts[i].interactType,
            realInteractobject.transform.position.x,
            realInteractobject.transform.position.y,
            realInteractobject.transform.position.z,
            realInteractobject.transform.eulerAngles.x,
            realInteractobject.transform.eulerAngles.y,
            realInteractobject.transform.eulerAngles.z,
            interacts[i].attributes[3].value.ModifiedValue,
            interacts[i].respawnTime
            ));
        }

        for (int i = 0; i < units.Length; i++)
        {
            string interactName = "No interact";
            GameObject realUnitobject = units[i].gameObject;
            if (units[i].interact != null)
            {
                Interactable interact = units[i].interact;
                interactName = interact.interactName;
            }

            data.myInventoryData.Add(new InventoryData("inventoryof_" + units[i].unitName, units[i].unitName));
            data.myInventoryData.Add(new InventoryData("equipmentInvof_" + units[i].unitName, units[i].unitName));
            UnitInventory inventory = units[i].getUnitInventory();
            UnitInventory equipments = units[i].getUnitEqInventory();
            for (int j = 0; j < inventory.GetSlots.Length; j++)
            {
                if (inventory.GetSlots[j].item != null)
                {
                    data.myItemData.Add(new ItemData(inventory.GetSlots[j].item.itemId, inventory.GetSlots[j].item.itemName, inventory.GetSlots[j].item.itemtype, inventory.GetSlots[j].amount, ("inventoryof_" + units[i].unitName), inventory.GetSlots[j].ID));
                    for (int k = 0; k < inventory.GetSlots[j].item.buffs.Length; k++)
                    {
                        data.myItemAttributeData.Add(new ItemAttributeData(inventory.GetSlots[j].item.itemId, inventory.GetSlots[j].item.buffs[k].attribute.ToString(),
                        inventory.GetSlots[j].item.buffs[k].value, inventory.GetSlots[j].item.buffs[k].min, inventory.GetSlots[j].item.buffs[k].max));
                    }
                }

            }
            for (int j = 0; j < equipments.GetSlots.Length; j++)
            {
                if (equipments.GetSlots[j].item != null)
                {
                    data.myItemData.Add(new ItemData(equipments.GetSlots[j].item.itemId, equipments.GetSlots[j].item.itemName, equipments.GetSlots[j].item.itemtype, equipments.GetSlots[j].amount, ("equipmentInvof_" + units[i].unitName), equipments.GetSlots[j].ID));
                    for (int k = 0; k < equipments.GetSlots[j].item.buffs.Length; k++)
                    {
                        data.myItemAttributeData.Add(new ItemAttributeData(equipments.GetSlots[j].item.itemId, equipments.GetSlots[j].item.buffs[k].attribute.ToString(),
                        equipments.GetSlots[j].item.buffs[k].value, equipments.GetSlots[j].item.buffs[k].min, equipments.GetSlots[j].item.buffs[k].max));
                    }
                }

            }

            data.myUnitsData.Add(new UnitData(units[i].unitName, units[i].unitType,
            realUnitobject.transform.position.x,
            realUnitobject.transform.position.y,
            realUnitobject.transform.position.z,
            realUnitobject.transform.eulerAngles.x,
            realUnitobject.transform.eulerAngles.y,
            realUnitobject.transform.eulerAngles.z,
            units[i].navAgent.destination.x,
            units[i].navAgent.destination.y,
            units[i].navAgent.destination.z,
            interactName
            ));
        }
    }

    public void QuickLoad()
    {
        //PlayerManager.Instance.DeselectUnits();
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "QuickSave.buner", FileMode.Open);

            SaveData data = (SaveData)bf.Deserialize(file);

            file.Close();
            LoadUnit(data);

        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }
    }

    public void SaveGameData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveGamedata.buner", FileMode.Create);

            SaveGameData gameData = new SaveGameData();

            SaveGameSaveData(gameData);

            bf.Serialize(file, gameData);

            file.Close();

        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }
    }
    public void LoadGameData()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "SaveGamedata.buner", FileMode.Open);

            SaveGameData gameData = (SaveGameData)bf.Deserialize(file);

            file.Close();
            gameDataList = gameData.myGameDataList;

        }
        catch (System.Exception)
        {
            //this is for handling errors
            throw;
        }
    }

    private void SaveGameSaveData(SaveGameData gameData)
    {
        for (int i = 0; i < gameDataList.Count; i++)
        {
            gameData.myGameDataList.Add(new GameData(gameDataList[i].gameName, gameDataList[i].gameTime));
        }

    }
    private void LoadUnit(SaveData data)
    {
        PlayerEventHandler.DeSelectUnits?.Invoke();
        InteractableMenuEventHandler.ClearInteractMenus?.Invoke();

        SaveLoadHandlers.ClearClickMarkers?.Invoke();

        List<InteractableBasics> interactableBasicsList = new List<InteractableBasics>();
        foreach (InteractableData interact in data.myInteractsData)
        {
            Destroy(GameObject.Find(interact.interactableName));
            InteractableBasics loadingInteract = new InteractableBasics(interact.interactableName, interact.interactableType, new Vector3(interact.positionx, interact.positiony, interact.positionz), new Vector3(interact.rotationx, interact.rotationy, interact.rotationz), interact.currentAmount, interact.spawnTimer);
            interactableBasicsList.Add(loadingInteract);
        }
        SaveLoadHandlers.SetInteractableBasicsLoadingForCreate?.Invoke(interactableBasicsList.ToArray());
        interactableBasicsList.Clear();

        List<UnitBasics> unitBasicsList = new List<UnitBasics>();
        foreach (UnitData unit in data.myUnitsData)
        {
            Destroy(GameObject.Find(unit.unitName));
            UnitBasics loadingUnit = new UnitBasics(unit.unitName, unit.unitType, new Vector3(unit.positionx, unit.positiony, unit.positionz), new Vector3(unit.rotationx, unit.rotationy, unit.rotationz), false, new Vector3(unit.destinationx, unit.destinationy, unit.destinationz), unit.interactName);
            unitBasicsList.Add(loadingUnit);
        }
        SaveLoadHandlers.SetUnitBasicsLoadingForCreate?.Invoke(unitBasicsList.ToArray());
        unitBasicsList.Clear();

        List<InventoryBasics> inventoryBasicsList = new List<InventoryBasics>();
        foreach (InventoryData inventory in data.myInventoryData)
        {
            InventoryBasics loadingInventory = new InventoryBasics(inventory.inventoryName, inventory.unitName);
            inventoryBasicsList.Add(loadingInventory);
        }
        SaveLoadHandlers.SetInventoryBasicsLoadingForCreate?.Invoke(inventoryBasicsList.ToArray());
        inventoryBasicsList.Clear();

        List<ItemBasics> itemBasicsList = new List<ItemBasics>();
        foreach (ItemData item in data.myItemData)
        {
            ItemBasics loadingItem = new ItemBasics(item.itemId, item.itemName, item.itemtype, item.itemAmount, item.inventoryName, item.slotId);
            itemBasicsList.Add(loadingItem);
        }
        SaveLoadHandlers.SetItemBasicsLoadingForCreate?.Invoke(itemBasicsList.ToArray());
        itemBasicsList.Clear();

        List<ItemAttributeBasics> itemAttBasicsList = new List<ItemAttributeBasics>();
        foreach (ItemAttributeData itemAttribute in data.myItemAttributeData)
        {
            ItemAttributeBasics loadingItemAttribute = new ItemAttributeBasics(itemAttribute.itemId, itemAttribute.attributeName, itemAttribute.attributeValue, itemAttribute.attributeMin, itemAttribute.attributeMax);
            itemAttBasicsList.Add(loadingItemAttribute);
        }
        SaveLoadHandlers.SetItemAttBasicsLoadingForCreate?.Invoke(itemAttBasicsList.ToArray());
        itemAttBasicsList.Clear();

        SaveLoadHandlers.CreatorFuncEvent?.Invoke();

        //camera and controller load
        Time.timeScale = data.myControllerData.pause;
        SaveLoadHandlers.PlayerManagerTransformLoad?.Invoke(data.myControllerData.positionx, data.myControllerData.positiony, data.myControllerData.positionz);
        SaveLoadHandlers.PlayerManagerRotationYLoad?.Invoke(data.myControllerData.rotationy);
        SaveLoadHandlers.VirtualCamOffsetLoad?.Invoke(data.myControllerData.virtualCamOffsetZ);
    }

    private void PlayerManagerTransform(float arg0, float arg1, float arg2)
    {
        playerManagerTransform = new Vector3(arg0, arg1, arg2);
    }

    private void PlayerManagerRotationY(float arg0)
    {
        playerManagerRotationY = arg0;
    }
    private void VirtualCamOffset(float arg0)
    {
        virtualCamOffset = arg0;
    }

}
