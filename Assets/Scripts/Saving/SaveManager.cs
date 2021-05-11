using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private PlayerUnitController[] units;
    private Interactable[] interacts;
    private GameObject camRig;
    private GameObject cam;

    public List<GameData> gameDataList;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
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


        camRig = GameObject.Find("Camera Rig");
        cam = GameObject.Find("Main Camera");
    }

    public void Save(string gameName)
    {

        units = GameObject.Find("UnitCollector").GetComponentsInChildren<PlayerUnitController>();
        interacts = GameObject.Find("InteractableCollector").GetComponentsInChildren<Interactable>();
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
        PlayerManager pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        pm.DeselectUnits();
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

        units = GameObject.Find("UnitCollector").GetComponentsInChildren<PlayerUnitController>();
        interacts = GameObject.Find("InteractableCollector").GetComponentsInChildren<Interactable>();
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + "QuickSave.buner", FileMode.Create);

            SaveData data = new SaveData();

            SaveUnit(data);

            bf.Serialize(file, data);

            file.Close();
            bool autosavevar = false;
            //autosave i game data ya saveliyoruz
            for (int i = 0; i < gameDataList.Count; i++)
            {
                if (gameDataList[i].gameName == "QuickSave")
                {
                    autosavevar = true;
                    gameDataList[i].gameTime = DateTime.Now.ToString();
                }
            }
            if (!autosavevar)
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
        camRig.transform.position.x,
        camRig.transform.position.y,
        camRig.transform.position.z,
        camRig.transform.eulerAngles.y,
        cam.transform.localPosition.y,
        cam.transform.localPosition.z
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
            interacts[i].CurrentAmount,
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
        PlayerManager pm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        pm.DeselectUnits();
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
            LoadGameSaveData(gameData);

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
    private void LoadGameSaveData(SaveGameData gameData)
    {
        gameDataList = gameData.myGameDataList;
    }


    private void LoadUnit(SaveData data)
    {
        CameraController camcontroller = camRig.GetComponent<CameraController>();
        //camera and controller load
        Time.timeScale = data.myControllerData.pause;
        camcontroller.loadPosition = new Vector3(data.myControllerData.positionx, data.myControllerData.positiony, data.myControllerData.positionz);
        camcontroller.loadRotation = new Vector3(0, data.myControllerData.rigRotationy, 0);
        camcontroller.loadZoom = new Vector3(0, data.myControllerData.camPositiony, data.myControllerData.camPositionz);

        ClickMarker[] clicks = GameObject.Find("clickMakerTransform").GetComponentsInChildren<ClickMarker>();
        foreach (ClickMarker click in clicks)
        {
            Destroy(click.gameObject);
        }

        interacts = GameObject.Find("InteractableCollector").GetComponentsInChildren<Interactable>();
        for (int i = 0; i < interacts.Length; i++)
        {
            Destroy(GameObject.Find("CanvasInteractable").transform.GetChild(i).gameObject);
        }

        foreach (InteractableData interact in data.myInteractsData)
        {
            Destroy(GameObject.Find(interact.interactableName));
            InteractableBasics loadingInteract = new InteractableBasics(interact.interactableName, interact.interactableType, new Vector3(interact.positionx, interact.positiony, interact.positionz), new Vector3(interact.rotationx, interact.rotationy, interact.rotationz), interact.currentAmount, interact.spawnTimer);
            FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().interactableList.Add(loadingInteract);
        }
        foreach (UnitData unit in data.myUnitsData)
        {
            Destroy(GameObject.Find(unit.unitName));
            UnitBasics loadingUnit = new UnitBasics(unit.unitName, unit.unitType, new Vector3(unit.positionx, unit.positiony, unit.positionz), new Vector3(unit.rotationx, unit.rotationy, unit.rotationz), false, new Vector3(unit.destinationx, unit.destinationy, unit.destinationz), unit.interactName);
            FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().unitList.Add(loadingUnit);
        }

        foreach (InventoryData inventory in data.myInventoryData)
        {
            InventoryBasics loadingInventory = new InventoryBasics(inventory.inventoryName, inventory.unitName);
            FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().inventoryList.Add(loadingInventory);
        }
        foreach (ItemData item in data.myItemData)
        {
            ItemBasics loadingItem = new ItemBasics(item.itemId, item.itemName, item.itemtype, item.itemAmount, item.inventoryName, item.slotId);
            FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().itemList.Add(loadingItem);
        }
        foreach (ItemAttributeData itemAttribute in data.myItemAttributeData)
        {
            ItemAttributeBasics loadingItemAttribute = new ItemAttributeBasics(itemAttribute.itemId, itemAttribute.attributeName, itemAttribute.attributeValue, itemAttribute.attributeMin, itemAttribute.attributeMax);
            FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().itemAttributeList.Add(loadingItemAttribute);
        }
        FindObjectOfType<UnitCreateManager>().GetComponent<UnitCreateManager>().CreatorFunc();
    }

}
