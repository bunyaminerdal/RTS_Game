using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCanvas : MonoBehaviour
{
    private MenuSelector[] menus;
    private void OnEnable()
    {
        InteractableMenuEventHandler.ClearInteractMenus.AddListener(ClearAllInteractableMenus);
    }
    private void OnDisable()
    {
        InteractableMenuEventHandler.ClearInteractMenus.RemoveListener(ClearAllInteractableMenus);
    }

    private void ClearAllInteractableMenus()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);

        }

    }
}
