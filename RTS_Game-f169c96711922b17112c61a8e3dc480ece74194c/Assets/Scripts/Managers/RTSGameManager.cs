using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameManager : MonoBehaviour {

    public static void UnitTakeDamage(UnitController attackingController, UnitController attackedController)
    {
        var damage = attackingController.attributes[5].value.ModifiedValue;

        attackedController.TakeDamage(attackingController, damage);
    }


    public static void UnitGather(UnitController gatherer, Interactable resource)
    {
        
        if(resource.getCurrentAmount() <= 0 || gatherer.getUnitInventory().calculateFull(resource.item)){            
            gatherer.stopGather();
        }else if(resource.getCurrentAmount()>0 )
        {
            resource.setCurrentAmount();
            gatherer.addItemToInventory(resource.item);
        }
        
    }
}
