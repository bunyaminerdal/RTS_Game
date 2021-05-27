

[System.Serializable]
public class InteractableAttribute
{
    [System.NonSerialized]
    public Interactable parent;
    public InteractableAttributes type;
    public ModifiableInt value;
    public ModifiableString stringValue;
    public Item item;

    public void SetParent(Interactable _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
        stringValue = new ModifiableString(AttributeModified);
        item = null;
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
    Item,
    Amount,
    Slot
}