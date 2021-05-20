using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable List", menuName = "Collectables/Collectable List")]
public class CollectableObjectList : ScriptableObject
{
    public List<CollectableObject> collectableObjectList;
}
