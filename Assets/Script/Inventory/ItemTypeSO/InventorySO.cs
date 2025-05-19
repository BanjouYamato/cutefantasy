using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Inventory/InventoryData")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    List<InventoryItem> inventoryItems;
    [field: SerializeField]
    public int size { get; set; } = 50;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void SetItemData(WeaponListWrapper data)
    {
        inventoryItems = data.inventoryItems;
    }

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    public void SaveItem()
    {
        WeaponListWrapper list = new WeaponListWrapper(inventoryItems);
        SaveGameManager.Instance.Save<WeaponListWrapper>(GameConstant.GAME_SAVE_INVENTORY, list);
    }
    public int AddItem(ItemScriptable item, int qty)
    {
        if (!item.IsStackable)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {   
                while (qty > 0 && !IsInventoryFull())
                {
                    qty -= AddItemToFirstFreeSlot(item, 1);
                }
            }
            InformAboutChange();
            return qty;
        }
        qty = AddStackableItem(item, qty);
        InformAboutChange();
        return qty;
    }

    private int AddItemToFirstFreeSlot(ItemScriptable item, int qty)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            quantity = qty
        };
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                inventoryItems[i] = newItem;
                return qty;
            }
        }
        return 0;
    }

    private bool IsInventoryFull()
        => inventoryItems.Where(item => item.IsEmpty).Any() == false;

    private int AddStackableItem(ItemScriptable item, int qty)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            if (inventoryItems[i].item.ID == item.ID)
            {
                int amountPossibleToTake =
                    inventoryItems[i].item.MaxStack - inventoryItems[i].quantity;
                if (qty > amountPossibleToTake)
                {
                    inventoryItems[i] = inventoryItems[i]
                        .ChangeQuantity(inventoryItems[i].item.MaxStack);
                    qty -= amountPossibleToTake;
                }
                else
                {
                    inventoryItems[i] = inventoryItems[i]
                        .ChangeQuantity(inventoryItems[i].quantity + qty);
                    return 0;
                }
            }
        }
        while (qty > 0 && !IsInventoryFull())
        {
            int newQty = Mathf.Clamp(qty, 0, item.MaxStack);
            qty -= newQty;
            AddItemToFirstFreeSlot(item, newQty);
        }
        return qty;
    }

    public Dictionary<int, InventoryItem> GetCurInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < inventoryItems.Count; ++i)
        {
            if (inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return inventoryItems[itemIndex];
    }

    public int GetIndextOf(InventoryItem item)
    {
        return inventoryItems.IndexOf(item);
    }
    public void AddItem(InventoryItem item)
    {
        AddItem(item.item, item.quantity);
    }

    public void SwapItems(int itemIndex_1, int itemIndex_2)
    {
        InventoryItem item1 = inventoryItems[itemIndex_1];
        inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
        inventoryItems[itemIndex_2] = item1;
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurInventoryState());
    }

    public void RemoveItem(int itemIndex, int amount)
    {
        if (inventoryItems.Count > itemIndex)
        {
            if (inventoryItems[itemIndex].IsEmpty)
                return;
            int reminder = inventoryItems[itemIndex].quantity - amount;
            if (reminder <= 0)
                inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                inventoryItems[itemIndex] = inventoryItems[itemIndex]
                    .ChangeQuantity(reminder);

            InformAboutChange();
        }
    }
    public void ResetData()
    {
        inventoryItems.Clear();
    }
}
[Serializable]
public class InventoryItem
{
    public int quantity;
    public ItemScriptable item;
    public bool IsEmpty => item == null;
    public InventoryItem ChangeQuantity(int newQty)
    {
        return new InventoryItem
        {
            item = this.item,
            quantity = newQty
        };
    }
    public override bool Equals(object obj)
    {
        if (obj is InventoryItem other)
        {
            return this.item == other.item && this.quantity == other.quantity;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return (item, quantity).GetHashCode();
    }
    public static InventoryItem GetEmptyItem()
    => new InventoryItem
    {
        item = null,
        quantity = 0
    };
}

