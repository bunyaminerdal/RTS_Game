using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableScreen : UserInterface
{
    public override void CloseScreen()
    {
        gameObject.SetActive(false);
    }

    public override void CreateSlots()
    {
        //Already created
        //maybe I will create button here
    }

    public override void UpdateInteractable(Interactable _interact)
    {
        gameObject.SetActive(true);
        interactableInterface.Clear();
        interactable = _interact;
        for (int i = 0; i < transform.childCount; i++)
        {
            interactableInterface.Add(transform.GetChild(i).gameObject, interactable.attributes[i]);
            if(transform.GetChild(i).TryGetComponent<Button>(out Button button))
            {
                if (interactable.attributes[i].unityAction != null)
                    button.onClick.AddListener(interactable.attributes[i].unityAction);                
            }
        }
    }

    public override void UpdateSlots(UnitInventory inventory)
    {
        //no inventory
    }

    public override void UpdateUnit(PlayerUnitController _unit)
    {
        //no playerunit
    }
}
