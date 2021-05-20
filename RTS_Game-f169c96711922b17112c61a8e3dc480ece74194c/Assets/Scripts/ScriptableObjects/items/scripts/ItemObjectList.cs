using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item List", menuName = "Inventory System/Item List")]
public class ItemObjectList : ScriptableObject
{
    public List<ItemObject> itemObjectList;
}
