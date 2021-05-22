using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScreen : UserInterface
{
    public override void CloseScreen()
    {
        throw new System.NotImplementedException();
    }

    public override void CreateSlots()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateInteractable(Interactable _interact)
    {
        //_interact.collectable.
    }

    public override void UpdateSlots(UnitInventory inventory)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateUnit(PlayerUnitController _unit)
    {
        throw new System.NotImplementedException();
    }
}
