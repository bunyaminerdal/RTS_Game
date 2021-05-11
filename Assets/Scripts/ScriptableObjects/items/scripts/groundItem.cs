using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class groundItem : MonoBehaviour , ISerializationCallbackReceiver
{
    public ItemObject itemObj;
    public  Item item;
    void Start()
    {
        item = new Item(itemObj);
        
    }
    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {
#if UNITY_EDITOR        
        GetComponentInChildren<SpriteRenderer>().sprite = itemObj.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif    
    }
}
