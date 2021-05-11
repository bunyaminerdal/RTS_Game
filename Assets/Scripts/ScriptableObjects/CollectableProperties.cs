using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/CollectableProperties")]
public class CollectableProperties : ScriptableObject
{
    public string collectablename;
    public string description;
    public float maxAmount;
    public float minAmount;
    public float CurrentAmount;
    public float respawnTime;
    public int interactSlot;
    public ItemObject item;
}
