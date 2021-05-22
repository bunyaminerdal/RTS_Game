using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitInventoryScreen : UserInterface
{
    public override void CreateSlots()
    {
        if (transform.childCount <= 0) return;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject InventorySlot = transform.GetChild(i).gameObject;
            AddEvent(InventorySlot, EventTriggerType.PointerEnter, delegate { OnEnter(InventorySlot); });
            AddEvent(InventorySlot, EventTriggerType.PointerExit, delegate { OnExit(InventorySlot); });
            AddEvent(InventorySlot, EventTriggerType.BeginDrag, delegate { OnDragStart(InventorySlot); });
            AddEvent(InventorySlot, EventTriggerType.EndDrag, delegate { OnDragEnd(InventorySlot); });
            AddEvent(InventorySlot, EventTriggerType.Drag, delegate { OnDrag(InventorySlot); });
        }
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    public override void CloseScreen()
    {
        gameObject.SetActive(false);
    }

    public override void UpdateSlots(UnitInventory inventory)
    {
        gameObject.SetActive(true);
        slotsOnInterface.Clear();
        unitInventory = inventory;
        for (int i = 0; i < unitInventory.Container.Slots.Length; i++)
        {
            unitInventory.Container.Slots[i].parent = this;
            slotsOnInterface.Add(transform.GetChild(i).gameObject, unitInventory.Container.Slots[i]);
        }
        slotsOnInterface.UpdateSlotDisplay();
    }

    public override void UpdateUnit(PlayerUnitController _unit)
    {
        //no unit
    }
}
