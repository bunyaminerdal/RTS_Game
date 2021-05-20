using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitInfoScreen : UserInterface
{
    public override void CreateSlots()
    {
        if (unit!=null)
        {
            if (transform.childCount == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                    textOnInterface.Add(obj, unit.attributes[i]);                   

                }
            }
        }
    }
}
