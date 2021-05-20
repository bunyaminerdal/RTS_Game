using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitInventory
{
    private int maxStackAmount=10;
    //private int maxSlot=6;
    public bool isFull;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots;}}
    public UnitInventory(int slot)
    {
        Container = new Inventory(slot);
    }   
    
    public void AddItem(Item _item, int _amount)
    {
        if(!isFull)        
        {
            
            if(_item.isStackable==false)
            {
                SetEmptySlot(_item.slotId, _item, _amount);               
                return;
            }

            for (int i = 0; i < GetSlots.Length; i++)
            {
                if(GetSlots[i].item!=null)
                {
                    if (GetSlots[i].item.itemName == _item.itemName && GetSlots[i].amount < maxStackAmount && GetSlots[i].item.isStackable)
                    {                    
                        GetSlots[i].AddAmount(_amount);
                        return;
                    }
                }
                
            }
            SetEmptySlot(_item.slotId, _item, _amount);         
        }
                
    }

    //bunu bi gözden geçirmek lazım
    public bool calculateFull(Item _item)
    {
        isFull = true;
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(GetSlots[i].amount==0)
            {
                isFull = false;
            }else if(GetSlots[i].item.itemName == _item.itemName && GetSlots[i].amount < maxStackAmount && GetSlots[i].item.isStackable==true)
            {
                isFull = false;                
            }
        }
        return isFull;
    }

    public InventorySlot SetEmptySlot(int _id,Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if(_id <= 0)
            {
                if(GetSlots[i].amount==0)
                {   
                    GetSlots[i].updateSlot(i,_item, _amount);
                    GetSlots[i].item.slotId = i;
                    return GetSlots[i];
                }
            }else{
                if(i==_id)
                {
                    GetSlots[i].updateSlot(_id,_item, _amount);
                    return GetSlots[i];
                }
                
            }
            
        }
        //set up functionality for full inventory
        return null;
    }

    public void SwapItems(InventorySlot itemslot1, InventorySlot itemslot2)
    {   
        
        InventorySlot temp = new InventorySlot(itemslot2.ID,itemslot2.item,itemslot2.amount);
        itemslot2.updateSlot(itemslot2.ID,itemslot1.item,itemslot1.amount);        
        itemslot1.updateSlot(itemslot1.ID,temp.item,temp.amount);
        if(itemslot1.item != null)
        {
            itemslot1.item.slotId = itemslot1.ID;
        }
        if(itemslot2.item != null)
        {
            itemslot2.item.slotId = itemslot2.ID;
        }                        
    }    
}
public delegate void SlotUpdated(InventorySlot _slot);
//[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;

    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;
    public int ID;
    public Item item;
    public int amount;
    public InventorySlot()
    {   
        updateSlot(-1,null,0);
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        updateSlot(_id,_item,_amount);
    }
    public void updateSlot(int _id, Item _item, int _amount)
    {
        if(OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);
        ID = _id;
        item = _item;
        amount = _amount;
        if(OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        updateSlot(-1,null,0);
    }
    public void AddAmount(int value)
    {
        updateSlot(ID,item,amount += value);
    }
    public bool CanPlaceInSlot(string type)
    {
        if(AllowedItems.Length <= 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (type == AllowedItems[i].ToString())
            {
                return true;
            }
        }
        return false;
    }
}
//[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots;
    
    public Inventory(int slot)
    {
        Slots = new InventorySlot[slot];
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i] = new InventorySlot(i,null,0);
        }
    }
    
}

