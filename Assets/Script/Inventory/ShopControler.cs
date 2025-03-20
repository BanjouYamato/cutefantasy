using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControler : MonoBehaviour
{
    [SerializeField]   
    UIShop uIShop;

    [SerializeField]
    List<ItemScriptable> itemCanBuy;

    [SerializeField]
    InventorySO inventoryData;

    [SerializeField]
    Button exitShopBttn;

    private void Start()
    {
        PrepareUIShop();
        exitShopBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS();
            uIShop.Hide();
        });
        Observer.Instance.AddToList(GameConstant.NPC_FEATURES_SHOP, OpenShopUI);
    }
    private void OnDestroy()
    {
        ClearUIShop();
        Observer.Instance.RemoveToList(GameConstant.NPC_FEATURES_SHOP, OpenShopUI);
    }

    private void PrepareUIShop()
    {
        uIShop.InitializeShop(itemCanBuy);
        uIShop.InitializedInventory(inventoryData.size);
        //inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateShopUI;
        uIShop.onItemSellRequested += HandleItemSell;
    }
    void ClearUIShop()
    {
        uIShop.ClearInventory();
        inventoryData.OnInventoryUpdated -= UpdateShopUI;
        uIShop.onItemSellRequested -= HandleItemSell;
    }

    private void HandleItemSell(int itemIndex)
    {
        InventoryItem item = inventoryData.GetItemAt(itemIndex);
        if (item.IsEmpty)
            return;
        uIShop.CreateUISell(item.item.Icon, item.item.Price, item.quantity, itemIndex);
    }

    private void UpdateShopUI(Dictionary<int, InventoryItem> inventoryState)
    {
        uIShop.ResetAllItem();
        foreach (var item in inventoryData.GetCurInventoryState())
        {
            uIShop.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity,
                item.Value.item.Price);
        }
    }
    void OpenShopUI()
    {
        uIShop.Show();
        foreach (var item in inventoryData.GetCurInventoryState())
        {
            uIShop.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity,
                item.Value.item.Price);
        }
    }
}
