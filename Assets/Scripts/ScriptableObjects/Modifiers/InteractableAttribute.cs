

using UnityEngine.Events;

[System.Serializable]
public class InteractableAttribute
{
    [System.NonSerialized]
    public Interactable parent;
    public InteractableAttributes type;
    public ModifiableInt value;
    public ModifiableString stringValue;
    public UnityAction unityAction;
    public bool isDepleted;

    public void SetParent(Interactable _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
        stringValue = new ModifiableString(AttributeModified);
        unityAction = null;
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
public enum InteractableAttributes
{    
    Name,
    Description,    
    Amount,
    Slot,
    Event
}