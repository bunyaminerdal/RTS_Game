using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitCollector : MonoBehaviour
{
    private PlayerUnitController[] playerUnits;
    private void OnEnable()
    {
        SaveLoadHandlers.playerUnitCollectorGetUnits.AddListener(GetUnits);
    }
    private void OnDisable()
    {
        SaveLoadHandlers.playerUnitCollectorGetUnits.RemoveListener(GetUnits);
    }

    private void GetUnits()
    {
       playerUnits =  transform.GetComponentsInChildren<PlayerUnitController>();
       SaveLoadHandlers.playerUnitCollectorSetUnits?.Invoke(playerUnits);
    }
}
