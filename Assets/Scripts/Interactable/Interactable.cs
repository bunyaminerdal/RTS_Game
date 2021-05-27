
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public static List<Interactable> AllInteractables { get; private set; }
    public Action OnCollectButtonpressed { get; internal set; }

    public Transform InteractableTransform;

    public float radius = 3f;
    public InteractableAttribute[] attributes;

    //bool isFocus = false;
    public GameObject interactMenu;

    protected GameObject Menu;
    protected Button yourButton;

    public CollectableObject collectable;
    private float maxAmount;
    private float minAmount;
    public float CurrentAmount;
    private bool isDepleted;
    public float respawnTime;
    public Item item; 
    public string interactName;
    public string interactType;

    //Düğme basıldığında trigerlanacak

    private void OnEnable()
    {
        if (AllInteractables == null)
            AllInteractables = new List<Interactable>();
        AllInteractables.Add(this);
    }

    private void OnDisable()
    {
        AllInteractables.Remove(this);
    }
    void Start()
    {
        maxAmount = collectable.maxAmount;
        minAmount = 0;
        item = new Item(collectable.item);
        //yourButton = Menu.GetComponentInChildren<Button>();
        //yourButton.onClick.AddListener(TaskOnClick);
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        //attributelar içinde name ve status ayarlama
        attributes[0].stringValue.BaseValue = interactName;
        attributes[1].stringValue.BaseValue = collectable.description;
        attributes[2].item = item;
        attributes[3].value.BaseValue = (int)CurrentAmount;
        attributes[4].value.BaseValue = collectable.interactSlot;
    }

    void Update()
    {
        if (respawnTime > 0)
        {
            isDepleted = true;
            respawnTime -= Time.deltaTime;
            if (respawnTime < 0)
            {
                Debug.Log("respawned");
                attributes[3].value.BaseValue = (int)maxAmount;
                isDepleted = false;
            }
        }

    }

    public void SetInteractableSelected(bool isSelected)
    {
        transform.Find("InteractHighlight").gameObject.SetActive(isSelected);

    }


    void TaskOnClick()
    {
        OnCollectButtonpressed?.Invoke();
    }

    public float getCurrentAmount()
    {
        return attributes[3].value.ModifiedValue;
    }

    public void setCurrentAmount()
    {
        if (attributes[3].value.ModifiedValue > minAmount)
        {
            attributes[3].value.BaseValue--;
            if (attributes[3].value.ModifiedValue <= minAmount)
            {
                respawnTime = collectable.respawnTime;
            }
        }

    }


    public bool checkInteractSlot()
    {
        if (attributes[4].value.BaseValue <= 0)
        {
            return false;
        }
        else if (attributes[4].value.BaseValue > 0)
        {
            takeInteractSlot();
            return true;
        }
        else
        {
            return false;
        }

    }

    public void giveInteractSlot()
    {
        attributes[4].value.BaseValue += 1;
    }

    public void takeInteractSlot()
    {
        attributes[4].value.BaseValue -= 1;
    }
    public void AttributeModified(InteractableAttribute _attribute)
    {
        if (_attribute.type == InteractableAttributes.Amount)
        {
            Debug.Log(_attribute.value.ModifiedValue);
        }

    }
}
