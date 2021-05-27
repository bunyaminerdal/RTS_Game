using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ModifiableString
{
    [SerializeField]
    private string baseValue;

    public string BaseValue { get => baseValue; set { baseValue = value; UpdateModifiedValue(); } }


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

    public void UpdateModifiedValue()
    {        
        ModifiedValue = baseValue ;
        if (ValueModified != null)
            ValueModified.Invoke();
    }
}

