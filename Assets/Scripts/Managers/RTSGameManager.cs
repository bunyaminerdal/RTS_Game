using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameManager : MonoBehaviour {

    public static void UnitTakeDamage(UnitController attackingController, UnitController attackedController)
    {
        var damage = attackingController.attributes[5].value.ModifiedValue;

        attackedController.TakeDamage(attackingController, damage);
    }


   
}
