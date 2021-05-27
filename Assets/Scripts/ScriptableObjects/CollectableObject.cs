using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable", menuName = "Collectables/Collectable")]
public class CollectableObject : ScriptableObject
{
    public string collectablename;
    public string description;
    public float maxAmount;
    public float respawnTime;
    public int interactSlot;
    public ItemObject item;
}
