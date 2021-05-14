using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCollector : MonoBehaviour
{
    private Interactable[] interacts;
    private void OnEnable()
    {
        SaveLoadHandlers.interactableCollectorGetInteracts.AddListener(GetInteracts);
    }
    private void OnDisable()
    {
        SaveLoadHandlers.interactableCollectorGetInteracts.RemoveListener(GetInteracts);
    }
    private void GetInteracts()
    {
        interacts = transform.GetComponentsInChildren<Interactable>();
        SaveLoadHandlers.interactableCollectorSetInteracts?.Invoke(interacts);
    }
}
