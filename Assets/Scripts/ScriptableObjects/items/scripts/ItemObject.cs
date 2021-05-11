using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
    Helmet,
    Chest,
    Weapon,
    Food,
    Default
}

public enum Attributes
{
    AttackSpeed,
    AttackDamage,
    AttackRange,
    Armour,
    Health,
    Name,
    Status,
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public SkinnedMeshRenderer unitDisplay;
    public bool isStackable;
    public string itemName;
    public ItemType type;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] buffs;
    
}
[System.Serializable]
public class Item
{
    public int slotId;
    public string itemId;
    public string itemName;
    public string itemtype;
    public Sprite uiDisplay;
    public SkinnedMeshRenderer unitDisplay;
    public bool isStackable;
    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {        
        slotId = -1;
        itemId = System.Guid.NewGuid().ToString();
        itemName = item.itemName;
        itemtype = item.type.ToString();
        uiDisplay = item.uiDisplay;
        unitDisplay = item.unitDisplay;
        isStackable = item.isStackable;
        buffs = new ItemBuff[item.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);            
            buffs[i].attribute = item.buffs[i].attribute;           
            
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
       value = UnityEngine.Random.Range(min,max); 
    }
}
