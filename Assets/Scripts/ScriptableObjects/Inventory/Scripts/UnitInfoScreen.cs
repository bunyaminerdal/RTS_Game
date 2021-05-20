using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitInfoScreen : UserInterface
{
    public override void CreateSlots()
    {

        //Already created

    }

    public override void SetNullUnitInventory()
    {
        gameObject.SetActive(false);
    }

    public override void UpdateSlots(UnitInventory inventory)
    {
        //no inventory
    }

    public override void UpdateUnit(PlayerUnitController _unit)
    {
        gameObject.SetActive(true);
        textOnInterface.Clear();
        unit = _unit;
        for (int i = 0; i < transform.childCount; i++)
        {
            textOnInterface.Add(transform.GetChild(i).gameObject, unit.attributes[i]);
        }
    }
}
