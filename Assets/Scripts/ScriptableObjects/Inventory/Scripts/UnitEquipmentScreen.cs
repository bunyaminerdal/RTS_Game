using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UnitEquipmentScreen : UserInterface
{
    public override void CreateSlots()
    {
        if (unitEqInventory!=null)
        {       
            if(transform.childCount == 0)
            {
                for (int i = 0; i < unitEqInventory.Container.Slots.Length; i++)
                {
                    unitEqInventory.Container.Slots[i].parent = this;
                }
                slotsOnInterface = new Dictionary<GameObject,InventorySlot>();
                for (int i = 0; i < unitEqInventory.Container.Slots.Length; i++)
                {
                    GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);                    

                    AddEvent(obj,EventTriggerType.PointerEnter, delegate {OnEnter(obj);});
                    AddEvent(obj,EventTriggerType.PointerExit, delegate {OnExit(obj);});
                    AddEvent(obj,EventTriggerType.BeginDrag, delegate {OnDragStart(obj);});
                    AddEvent(obj,EventTriggerType.EndDrag, delegate {OnDragEnd(obj);});
                    AddEvent(obj,EventTriggerType.Drag, delegate {OnDrag(obj);});
                    
                    slotsOnInterface.Add(obj, unitEqInventory.Container.Slots[i]);
                }
            }     
            
        }
    }
}
