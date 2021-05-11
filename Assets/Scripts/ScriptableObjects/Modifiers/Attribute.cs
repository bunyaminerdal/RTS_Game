using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute 
{
    [System.NonSerialized]
    public UnitController parent;
    public Attributes type;
    public ModifiableInt value;
    public ModifiableString stringValue;

    public void SetParent(UnitController _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
        stringValue = new ModifiableString();
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
