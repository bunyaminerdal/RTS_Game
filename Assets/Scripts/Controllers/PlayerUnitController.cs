using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitController : UnitController
{
    public static List<PlayerUnitController> AllPlayerUnits;

    private void OnEnable()
    {
        if (AllPlayerUnits == null)
        {
            AllPlayerUnits = new List<PlayerUnitController>();
        }
        AllPlayerUnits.Add(this);
    }

    private void OnDisable()
    {
        AllPlayerUnits.Remove(this);
    }
}
