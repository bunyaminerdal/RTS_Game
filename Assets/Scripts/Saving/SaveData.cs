using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData 
{
    public List<UnitData> myUnitsData;
    public List<InteractableData> myInteractsData;
    public List<InventoryData> myInventoryData;
    public List<ItemData> myItemData;
    public List<ItemAttributeData> myItemAttributeData;
    public ControllerData myControllerData;

    public SaveData()
    {
        myInteractsData = new List<InteractableData>();
        myUnitsData = new List<UnitData>();        
        myInventoryData = new List<InventoryData>();        
        myItemData = new List<ItemData>();     
        myItemAttributeData = new List<ItemAttributeData>(); 
    }
}

[Serializable]
public class ControllerData
{
    public float pause;
    public float positionx;
    public float positiony;
    public float positionz;
    public float rigRotationy;
    public float camPositiony;
    public float camPositionz;


    public ControllerData(float _pause,
        float _positionx , float _positiony, float _positionz,
        float _rigRotationy,float _camPositiony,float _camPositionz
        )
    {
        pause = _pause;
        positionx = _positionx;
        positiony = _positiony;
        positionz = _positionz;
        rigRotationy = _rigRotationy;
        camPositiony = _camPositiony;
        camPositionz = _camPositionz;        
    }
}

[Serializable]
public class InteractableData
{
    public string interactableName;
    public string interactableType;
    public float currentAmount;
    public float spawnTimer;
    public float positionx;
    public float positiony;
    public float positionz;
    public float rotationx;
    public float rotationy;
    public float rotationz;

    public InteractableData(string _interactableName,string _interactableType,
        float _positionx , float _positiony, float _positionz,
        float _rotationx , float _rotationy, float _rotationz,
        float _currentAmount,float _spawnTimer)
    {
        interactableName = _interactableName;
        interactableType = _interactableType;
        positionx = _positionx;
        positiony = _positiony;
        positionz = _positionz;
        rotationx = _rotationx;
        rotationy = _rotationy;
        rotationz = _rotationz;
        currentAmount = _currentAmount;
        spawnTimer = _spawnTimer;

    }
}

[Serializable]
public class UnitData
{
    public string unitName;
    public string unitType;
    public float positionx;
    public float positiony;
    public float positionz;
    public float rotationx;
    public float rotationy;
    public float rotationz;
    public float destinationx;
    public float destinationy;
    public float destinationz;   
    public string interactName;

    public UnitData(string _unitName,string _unitType ,
        float _positionx , float _positiony, float _positionz,
        float _rotationx , float _rotationy, float _rotationz,
        float _destinationx , float _destinationy, float _destinationz,
        string _interactName
        )
    {
        unitName = _unitName;
        unitType = _unitType;
        positionx = _positionx;
        positiony = _positiony;
        positionz = _positionz;
        rotationx = _rotationx;
        rotationy = _rotationy;
        rotationz = _rotationz;
        destinationx = _destinationx;
        destinationy = _destinationy;
        destinationz = _destinationz;
        interactName = _interactName;
    }
}

[Serializable]
public class ItemData
{
    public string itemId;
    public string itemName;
    public string itemtype;
    public string inventoryName;
    public int itemAmount;
    public int slotId;


    public ItemData(string _itemId,string _itemName,string _itemtype,int _itemAmount,string _inventoryName, int _slotId )
    {
        itemId = _itemId;
        itemName = _itemName;
        itemtype = _itemtype;
        itemAmount = _itemAmount;
        inventoryName = _inventoryName;
        slotId = _slotId;
    }
}

[Serializable]
public class ItemAttributeData
{
    public string itemId;
    public string attributeName;
    public int attributeMin;
    public int attributeMax;
    public int attributeValue;
    public ItemAttributeData(string _itemId,string _attributeName,int _attributeValue,int _attributeMin,int _attributeMax )
    {
        itemId = _itemId;
        attributeName = _attributeName;
        attributeMin = _attributeMin;
        attributeMax = _attributeMax;
        attributeValue = _attributeValue;
    }
}

[Serializable]
public class InventoryData
{
    public string inventoryName;
    public string unitName;
    

    public InventoryData(string _inventoryName,string _unitName)
    {
        inventoryName = _inventoryName;
        unitName = _unitName;
    }
}
