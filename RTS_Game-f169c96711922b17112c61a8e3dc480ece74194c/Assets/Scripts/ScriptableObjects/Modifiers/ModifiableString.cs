using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ModifiableString
{
    [SerializeField]
    private string baseValue;

    public string BaseValue { get => baseValue; set => baseValue = value; }


    [SerializeField]
    private string modifiedValue;
    public string ModifiedValue { get => modifiedValue; private set => modifiedValue = value; }

    public event ModifiedEvent ValueModified;

    public ModifiableString(ModifiedEvent method = null)
    {
        modifiedValue = BaseValue;
        if (method != null)
            ValueModified += method;
    }

    public void RegisterModEvent(ModifiedEvent method)
    {
        ValueModified += method;
    }
    public void UnregisterModEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void SetModifier(string _string)
    {
        ModifiedValue = _string;
        if (ValueModified != null)
            ValueModified.Invoke();

    }    
}

