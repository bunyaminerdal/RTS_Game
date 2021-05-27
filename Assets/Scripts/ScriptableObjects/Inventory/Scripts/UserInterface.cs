using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.InputSystem;

public abstract class UserInterface : MonoBehaviour
{
    protected UnitController unit;
    protected UnitInventory unitInventory;
    protected Interactable interactable;
    protected Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
    protected Dictionary<GameObject, Attribute> textOnInterface = new Dictionary<GameObject, Attribute>();
    protected Dictionary<GameObject, InteractableAttribute> interactableInterface = new Dictionary<GameObject, InteractableAttribute>();

    void Start()
    {
        CreateSlots();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slotsOnInterface.UpdateSlotDisplay();
        textOnInterface.UpdateTextDisplay();
        interactableInterface.UpdateInteractableText();
    }

    public abstract void CreateSlots();
    public abstract void UpdateSlots(UnitInventory inventory);
    public abstract void UpdateUnit(PlayerUnitController _unit);
    public abstract void UpdateInteractable(Interactable _interact);
    public abstract void CloseScreen();

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }
    public void OnDragStart(GameObject obj)
    {

        MouseData.tempItemBeingDragged = CreatTempItem(obj);
    }

    public GameObject CreatTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnInterface[obj].item != null)
        {
            tempItem = new GameObject();
            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(80, 80);
            tempItem.transform.SetParent(transform.parent.parent);
            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].item.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        if (MouseData.interfaceMouseIsOver == null)
        {
            //burada inventory dışında bir yere atılırsa ne olacağını belirliyoruz.
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        if (slotsOnInterface[obj].item != null)
        {
            if (MouseData.slotHoveredOver != null)
            {
                InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                if (mouseHoverSlotData.item == null)
                {
                    if (mouseHoverSlotData.CanPlaceInSlot(slotsOnInterface[obj].item.itemtype))
                    {
                        unitInventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData.parent.slotsOnInterface[MouseData.slotHoveredOver]);
                    }
                }
                else
                {
                    if (mouseHoverSlotData.CanPlaceInSlot(slotsOnInterface[obj].item.itemtype) && slotsOnInterface[obj].CanPlaceInSlot(mouseHoverSlotData.item.itemtype))
                    {
                        unitInventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData.parent.slotsOnInterface[MouseData.slotHoveredOver]);
                    }
                }
            }
        }


    }
    public void OnDrag(GameObject obj)
    {
        Vector2 position = Mouse.current.position.ReadValue();
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = position;
        }
    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }

}

public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;
    public static GameObject tempItemBeingDragged;
    public static GameObject slotHoveredOver;
}

public static class ExtensionMethods
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _SlotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _SlotsOnInterface)
        {
            if (_slot.Value.amount > 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.item.uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TMP_Text>().text = _slot.Value.item.isStackable == false ? "" : _slot.Value.amount.ToString();
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TMP_Text>().text = "";

            }
        }
    }

    public static void UpdateTextDisplay(this Dictionary<GameObject, Attribute> _TextOnInterface)
    {
        foreach (KeyValuePair<GameObject, Attribute> _Text in _TextOnInterface)
        {
            switch (_Text.Value.type)
            {
                case Attributes.AttackSpeed:
                    _Text.Key.GetComponent<TMP_Text>().text = "Attack Speed: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case Attributes.AttackDamage:
                    _Text.Key.GetComponent<TMP_Text>().text = "Attack Damage: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case Attributes.AttackRange:
                    _Text.Key.GetComponent<TMP_Text>().text = "Attack Range: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case Attributes.Armour:
                    _Text.Key.GetComponent<TMP_Text>().text = "Armour: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case Attributes.Health:
                    _Text.Key.GetComponent<TMP_Text>().text = "Health: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case Attributes.Name:
                    _Text.Key.GetComponent<TMP_Text>().text = "Name: " + _Text.Value.stringValue.ModifiedValue;
                    break;
                case Attributes.Status:
                    _Text.Key.GetComponent<TMP_Text>().text = "Status: " + _Text.Value.stringValue.ModifiedValue;
                    break;
                default:
                    break;
            }
        }
    }
    public static void UpdateInteractableText(this Dictionary<GameObject, InteractableAttribute> _InteractableInterface)
    {
        foreach (KeyValuePair<GameObject, InteractableAttribute> _Text in _InteractableInterface)
        {
            switch (_Text.Value.type)
            {
                case InteractableAttributes.Name:
                    _Text.Key.GetComponent<TMP_Text>().text = "Name: " + _Text.Value.stringValue.ModifiedValue;
                    break;
                case InteractableAttributes.Description:
                    _Text.Key.GetComponent<TMP_Text>().text = "Description: " + _Text.Value.stringValue.ModifiedValue;
                    break;                
                case InteractableAttributes.Amount:
                    _Text.Key.GetComponent<TMP_Text>().text = "Amount: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case InteractableAttributes.Slot:
                    _Text.Key.GetComponent<TMP_Text>().text = "Slot: " + _Text.Value.value.ModifiedValue.ToString();
                    break;
                case InteractableAttributes.Event:
                    if (_Text.Value.value.ModifiedValue > 0)                        
                        _Text.Key.GetComponent<Button>().interactable = !_Text.Value.isDepleted;
                    else
                        _Text.Key.GetComponent<Button>().interactable = false;
                    break;
                default:
                    break;
            }
        }
    }
}