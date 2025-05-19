using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControler : MonoBehaviour
{
    [SerializeField]
    UIShop uIShop;

    [SerializeField]
    List<ItemScriptable> _weaponList, _toolList, _foodList, _seedList, _resourceList;

    [SerializeField]
    InventorySO inventoryData;

    [SerializeField]
    Button exitShopBttn;

    ItemType currentItemType;
    private List<UIItemType> _uiItems = new List<UIItemType>();
    public ItemType GetItemType()
    {
        return currentItemType;
    }

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
    public void RegisterUIItem(UIItemType uiItem)
    {
        if (!_uiItems.Contains(uiItem))
            _uiItems.Add(uiItem);
    }
    private void PrepareUIShop()
    {
        UpdateItemType(ItemType.Weapon);
        uIShop.InitializedInventory(inventoryData.size);
        //inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateShopUI;
        uIShop.onItemSellRequested += HandleItemSell;
    }
    public void UpdateItemType(ItemType newType)
    {
        currentItemType = newType;

        foreach (UIItemType item in _uiItems)
            item.UpdateVisual();

        switch (currentItemType)
        {
            case ItemType.Weapon:
                uIShop.Init(_weaponList);
                break;
            case ItemType.Tool:
                uIShop.Init(_toolList);
                break;
            case ItemType.Food:
                uIShop.Init(_foodList);
                break;
            case ItemType.Seed:
                uIShop.Init(_seedList);
                break;
            case ItemType.Resource:
                uIShop.Init(_resourceList);
                break;
            default:
                break;
        }
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
public enum ItemType
{
    Weapon = 0,
    Tool = 1,
    Food = 2,
    Seed = 3,
    Resource = 4
}
