using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField]
    UIShopSlot shopSlot;

    [SerializeField]
    RectTransform contentShop, contentInventoryShop;

    [SerializeField]
    UIShopBuy buyDisplay;

    [SerializeField]
    UIInventoryShop UIInventoryShop;

    [SerializeField]
    UISell UISell;

    List<UIInventoryShop> items = new List<UIInventoryShop>();

    public Action<int> onItemSellRequested;

    private void Awake()
    {
        Hide();
    }
    public void InitializeShop(List<ItemScriptable> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            UIShopSlot uIShopSlot = Instantiate(shopSlot, Vector3.zero, Quaternion.identity);
            uIShopSlot.transform.SetParent(contentShop);
            uIShopSlot.SetData(list[i]);
            if (uIShopSlot.GetComponent<Button>() != null)
                uIShopSlot.GetComponent<Button>().onClick.AddListener(() => HandleBuyItem(uIShopSlot));
            else
                Debug.Log("null ba roi");
            //uIShopSlot.OnClickItemBuy += HandleBuyItem;
        }
    }

    private void HandleBuyItem(UIShopSlot slot)
    {
        buyDisplay.Toggle(true);
        buyDisplay.SetDataShop(slot);
    }
    public void Show()
    {
        UiHelper.Toogle(true);
        gameObject.SetActive(true);
        DeselectAllItems();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        buyDisplay.Toggle(false);
        UISell.Toggle(false);
        UiHelper.Toogle(false);
    }
    public void InitializedInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryShop item = Instantiate(UIInventoryShop, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentInventoryShop);
            items.Add(item);
            item.OnSeletedItem += HandleItemSelection;
            item.OnSellItem += HandleSellItem;
        }
    }
    public void ClearInventory()
    {
        foreach (var item in items)
        {
            item.OnSeletedItem -= HandleItemSelection;
            item.OnSellItem -= HandleSellItem;
        }
        items.Clear();
    }

    private void HandleItemSelection(UIInventoryShop item)
    {
        DeselectAllItems();
        int index = items.IndexOf(item);
        if (index == -1) 
            return;
        items[index].Select();
    }

    private void HandleSellItem(UIInventoryShop item)
    {
        int index = items.IndexOf(item);
        if (index == -1) 
            return;
        onItemSellRequested?.Invoke(index);
    }
    private void DeselectAllItems()
    {
        foreach (UIInventoryShop item in items)
        {
            item.Deselect();
        }
    }
    public void ResetAllItem()
    {
        foreach (UIInventoryShop item in items)
        {
            item.Deselect();
            item.ResetData();
        }
    }
    public void UpdateData(int itemIndex,
        Sprite itemImage, int itemQty, int price)
    {
        if (items.Count > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQty, price);
        }
    }
    public void CreateUISell(Sprite icon, int price, int qtyCanSell, int itemIndex)
    {
        UISell.Toggle(true);
        UISell.SetData(icon, price, qtyCanSell, itemIndex);
    }
}
