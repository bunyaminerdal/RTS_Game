using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreateManager : MonoBehaviour
{
    public GameObject GarbagePrefab;
    public GameObject TrashbinPrefab;
    public GameObject interactableCollector;

    public List<ItemObject> itemObjectList;

    private GameObject createdInteractable;
    private GameObject interactPrefab;
    public List<InteractableBasics> interactableList;

    public Transform clickMarkerTransform;
    public GameObject manPrefab;
    public GameObject manPrefab1;
    public GameObject manPrefab2;
    public GameObject womanPrefab;
    public GameObject womanPrefab1;
    public GameObject womanPrefab2;
    public GameObject policePrefab;

    public GameObject unitCollector;

    public UnitStats unitStats;

    public List<UnitBasics> unitList;
    public List<InventoryBasics> inventoryList;
    public List<ItemBasics> itemList;
    public List<ItemAttributeBasics> itemAttributeList;

    private GameObject createdUnit;
    private GameObject unitPrefab;
    private Interactable interactableObject;

    private PlayerUnitController[] units;

    // Start is called before the first frame update
    void Awake()
    {
        interactableList = new List<InteractableBasics>();
        InteractableBasics interact1 = new InteractableBasics("cop1", "Garbage", new Vector3(10f, 0f, 0f), new Vector3(0f, 0f, 0f), 4f, 0f);
        InteractableBasics interact2 = new InteractableBasics("cop2", "TrashBin", new Vector3(20f, 0f, 0f), new Vector3(0f, 0f, 0f), 0f, 10f);
        InteractableBasics interact3 = new InteractableBasics("cop3", "Garbage", new Vector3(30f, 0f, 0f), new Vector3(0f, 0f, 0f), 6f, 0f);
        InteractableBasics interact4 = new InteractableBasics("cop4", "TrashBin", new Vector3(40f, 0f, 0f), new Vector3(0f, 0f, 0f), 10f, 0f);
        interactableList.Add(interact1);
        interactableList.Add(interact2);
        interactableList.Add(interact3);
        interactableList.Add(interact4);

        unitList = new List<UnitBasics>();
        UnitBasics unit1 = new UnitBasics("Ali", "man", new Vector3(0f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(0f, 0.0833f, 0f), "cop1");
        UnitBasics unit2 = new UnitBasics("Cafer", "man1", new Vector3(2f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(2f, 0.0833f, 0f), "No interact");
        UnitBasics unit3 = new UnitBasics("Kamil", "man2", new Vector3(-2f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(2f, 0.0833f, 0f), "cop4");
        UnitBasics unit4 = new UnitBasics("polisamca", "police", new Vector3(4f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(4f, 0.0833f, 0f), "No interact");
        UnitBasics unit5 = new UnitBasics("Zilli", "woman", new Vector3(6f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(6f, 0.0833f, 0f), "No interact");
        UnitBasics unit6 = new UnitBasics("Zilloş", "woman1", new Vector3(8f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(8f, 0.0833f, 0f), "No interact");
        UnitBasics unit7 = new UnitBasics("Zeliha", "woman2", new Vector3(10f, 0.0833f, 0f), new Vector3(0f, 0f, 0f), false, new Vector3(10f, 0.0833f, 0f), "No interact");


        unitList.Add(unit1);
        unitList.Add(unit2);
        unitList.Add(unit3);
        unitList.Add(unit4);
        unitList.Add(unit5);
        unitList.Add(unit6);
        unitList.Add(unit7);
        inventoryList = new List<InventoryBasics>();
        itemList = new List<ItemBasics>();
        itemAttributeList = new List<ItemAttributeBasics>();
    }

    // Update is called once per frame
    void Start()
    {
        CreatorFunc();

    }

    public void CreatorFunc()
    {
        CreateInteractable();
        if (interactableList.Count > 0)
        {
            interactableList.Clear();
        }
        UnitCreate();

        if (unitList.Count > 0)
        {
            unitList.Clear();
        }

        CreateInventory();

        if (itemAttributeList.Count > 0)
        {
            itemAttributeList.Clear();
        }

        if (itemList.Count > 0)
        {
            itemList.Clear();
        }

        if (inventoryList.Count > 0)
        {
            inventoryList.Clear();
        }
    }

    void UnitCreate()
    {
        UnitBoxController unitBoxController = FindObjectOfType<UnitBoxController>();
        if (unitList.Count > 0)
        {
            unitBoxController.beforeUnitsCreated();

            for (int i = 0; i < unitList.Count; i++)
            {
                switch (unitList[i].unitType)
                {
                    case "man":
                        unitPrefab = manPrefab;
                        break;
                    case "man1":
                        unitPrefab = manPrefab1;
                        break;
                    case "man2":
                        unitPrefab = manPrefab2;
                        break;
                    case "woman":
                        unitPrefab = womanPrefab;
                        break;
                    case "woman1":
                        unitPrefab = womanPrefab1;
                        break;
                    case "woman2":
                        unitPrefab = womanPrefab2;
                        break;
                    case "police":
                        unitPrefab = policePrefab;
                        break;
                    default:
                        unitPrefab = manPrefab;
                        break;
                }

                createdUnit = Instantiate(unitPrefab, unitCollector.transform);
                createdUnit.name = unitList[i].unitName;
                createdUnit.transform.position = unitList[i].position;
                createdUnit.transform.eulerAngles = unitList[i].rotation;

                UnitController unitcontroller = createdUnit.GetComponent<UnitController>();
                unitcontroller.unitName = unitList[i].unitName;
                unitcontroller.unitType = unitList[i].unitType;
                unitcontroller.clickMarkerTransform = clickMarkerTransform;
                unitcontroller.unitDestination = unitList[i].destination;
                if (unitList[i].interactName != "No interact")
                {
                    Interactable[] interacts = GameObject.Find("InteractableCollector").GetComponentsInChildren<Interactable>();
                    foreach (Interactable interact in interacts)
                    {
                        if (unitList[i].interactName == interact.interactName)
                        {
                            interactableObject = interact;
                        }
                    }
                    unitcontroller.unitInteract = interactableObject;
                }

                unitBoxController.onUnitCreated(unitcontroller);
            }

        }
    }

    void CreateInteractable()
    {
        if (interactableList.Count > 0)
        {
            for (int i = 0; i < interactableList.Count; i++)
            {
                switch (interactableList[i].interactableType)
                {
                    case "Garbage":
                        interactPrefab = GarbagePrefab;
                        break;
                    case "TrashBin":
                        interactPrefab = TrashbinPrefab;
                        break;
                    default:
                        interactPrefab = GarbagePrefab;
                        break;
                }
                createdInteractable = Instantiate(interactPrefab, interactableCollector.transform);
                createdInteractable.name = interactableList[i].interactableName;
                createdInteractable.transform.position = interactableList[i].position;
                createdInteractable.transform.eulerAngles = interactableList[i].rotation;
                Interactable interactableProperties = createdInteractable.GetComponent<Interactable>();
                interactableProperties.CurrentAmount = interactableList[i].currentAmount;
                interactableProperties.interactName = interactableList[i].interactableName;
                interactableProperties.interactType = interactableList[i].interactableType;
                interactableProperties.respawnTime = interactableList[i].spawnTimer;
            }

        }
    }
    void CreateInventory()
    {
        units = GameObject.Find("UnitCollector").GetComponentsInChildren<PlayerUnitController>();
        foreach (UnitController unit in units)
        {

            foreach (InventoryBasics inventory in inventoryList)
            {
                if (unit.unitName == inventory.unitName)
                {
                    if (inventory.inventoryName == "inventoryof_" + unit.unitName)
                    {
                        //yeni inventory oluşturuyoruz
                        UnitInventory createdInventory = new UnitInventory(6);

                        for (int i = 0; i < createdInventory.GetSlots.Length; i++)
                        {
                            createdInventory.GetSlots[i] = new InventorySlot(i, null, 0);
                        }
                        foreach (ItemBasics item in itemList)
                        {
                            if (item.inventoryName == inventory.inventoryName)
                            {
                                foreach (ItemObject itemobjprefab in itemObjectList)
                                {
                                    if (itemobjprefab.itemName == item.itemName)
                                    {
                                        ItemObject createdItemprefab = itemobjprefab;
                                        Item createdItem = new Item(createdItemprefab);
                                        createdItem.itemId = item.itemId;
                                        createdItem.slotId = item.slotId;
                                        foreach (ItemAttributeBasics itemattr in itemAttributeList)
                                        {
                                            if (item.itemId == itemattr.itemId)
                                            {
                                                for (int i = 0; i < createdItem.buffs.Length; i++)
                                                {
                                                    if (createdItem.buffs[i].attribute.ToString() == itemattr.attributeName)
                                                    {
                                                        createdItem.buffs[i].value = itemattr.attributeValue;
                                                        createdItem.buffs[i].min = itemattr.attributeMin;
                                                        createdItem.buffs[i].max = itemattr.attributeMax;
                                                    }
                                                }
                                            }
                                        }
                                        createdInventory.AddItem(createdItem, item.itemAmount);
                                    }
                                }
                            }
                        }
                        unit.unitInventoryStart = createdInventory;

                    }
                    else if (inventory.inventoryName == "equipmentInvof_" + unit.unitName)
                    {
                        //setup a new equipment inventory
                        UnitInventory createdEquipments = new UnitInventory(3);

                        for (int i = 0; i < createdEquipments.GetSlots.Length; i++)
                        {
                            createdEquipments.GetSlots[i] = new InventorySlot(i, null, 0);
                        }
                        foreach (ItemBasics item in itemList)
                        {
                            if (item.inventoryName == inventory.inventoryName)
                            {
                                foreach (ItemObject itemobjprefab in itemObjectList)
                                {
                                    if (itemobjprefab.itemName == item.itemName)
                                    {
                                        ItemObject createdItemprefab = itemobjprefab;
                                        Item createdItem = new Item(createdItemprefab);
                                        createdItem.itemId = item.itemId;
                                        createdItem.slotId = item.slotId;
                                        foreach (ItemAttributeBasics itemattr in itemAttributeList)
                                        {
                                            if (item.itemId == itemattr.itemId)
                                            {
                                                for (int i = 0; i < createdItem.buffs.Length; i++)
                                                {
                                                    if (createdItem.buffs[i].attribute.ToString() == itemattr.attributeName)
                                                    {
                                                        createdItem.buffs[i].value = itemattr.attributeValue;
                                                        createdItem.buffs[i].min = itemattr.attributeMin;
                                                        createdItem.buffs[i].max = itemattr.attributeMax;
                                                    }
                                                }
                                            }
                                        }
                                        createdEquipments.AddItem(createdItem, item.itemAmount);
                                    }
                                }
                            }
                        }
                        unit.unitEqInventoryStart = createdEquipments;
                    }

                }
            }
        }

    }

}




public class UnitBasics
{
    public string unitName;
    public string unitType;
    public Vector3 position;
    public Vector3 rotation;
    public bool isAttacking;
    public Vector3 destination;
    public string interactName;

    public UnitBasics(string _unitName, string _unitType, Vector3 _position, Vector3 _rotation,
        bool _isAttacking, Vector3 _destination, string _interactName)
    {
        unitName = _unitName;
        unitType = _unitType;
        position = _position;
        rotation = _rotation;
        isAttacking = _isAttacking;
        destination = _destination;
        interactName = _interactName;
    }
}

public class InteractableBasics
{
    public string interactableName;
    public string interactableType;
    public float currentAmount;
    public float spawnTimer;
    public Vector3 position;
    public Vector3 rotation;

    public InteractableBasics(string _interactableName, string _interactableType, Vector3 _position, Vector3 _rotation, float _currentAmount, float _spawnTimer)
    {
        interactableName = _interactableName;
        interactableType = _interactableType;
        position = _position;
        rotation = _rotation;
        currentAmount = _currentAmount;
        spawnTimer = _spawnTimer;

    }
}

public class InventoryBasics
{
    public string inventoryName;
    public string unitName;


    public InventoryBasics(string _inventoryName, string _unitName)
    {
        inventoryName = _inventoryName;
        unitName = _unitName;
    }
}

public class ItemBasics
{
    public string itemId;
    public string itemName;
    public string itemtype;
    public string inventoryName;
    public int itemAmount;
    public int slotId;

    public ItemBasics(string _itemId, string _itemName, string _itemtype, int _itemAmount, string _inventoryName, int _slotId)
    {
        itemId = _itemId;
        itemName = _itemName;
        itemtype = _itemtype;
        itemAmount = _itemAmount;
        inventoryName = _inventoryName;
        slotId = _slotId;
    }
}

public class ItemAttributeBasics
{
    public string itemId;
    public string attributeName;
    public int attributeMin;
    public int attributeMax;
    public int attributeValue;
    public ItemAttributeBasics(string _itemId, string _attributeName, int _attributeValue, int _attributeMin, int _attributeMax)
    {
        itemId = _itemId;
        attributeName = _attributeName;
        attributeMin = _attributeMin;
        attributeMax = _attributeMax;
        attributeValue = _attributeValue;
    }
}
