using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopBuy : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI itemName, qtyBuy, totalPrice;

    [SerializeField]
    Button increaseBtn, decreaseBtn, buyBtn, cancelBtn;

    [SerializeField]
    InventorySO inventoryData;

    int qty, price;

    ItemScriptable item;


    private void Start()
    {
        increaseBtn.onClick.AddListener(IncreaseQty);
        decreaseBtn.onClick.AddListener(DecreaseQty);
        cancelBtn.onClick.AddListener(() => gameObject.SetActive(false));
        buyBtn.onClick.AddListener(HandleBuyItem);   
    }

    public void SetDataShop(UIShopSlot slot)
    {   
        item = slot.item;
        icon.sprite = item.Icon;
        itemName.text = item.ItemName;
        ResetQty(slot);
    }
    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }
    public void ResetQty(UIShopSlot slot)
    {
        qty = 1;
        price = slot.item.Price;
        InformAboutChange();
    }
    void InformAboutChange()
    {
        qtyBuy.text = qty.ToString();
        price = UpdateTotalPrice();
        totalPrice.text = price.ToString();

    }
    int UpdateTotalPrice()
    {
        return item.Price * qty;
    }
    void IncreaseQty()
    {
        qty++;
        qty = Mathf.Clamp(qty, 1, 99);
        InformAboutChange();
    }
    void DecreaseQty()
    {
        qty--;
        qty = Mathf.Clamp(qty, 1, 99);
        InformAboutChange();
    }
    void HandleBuyItem()
    {
        if (GameControler.Instance.gold < price)
            return;
        GameControler.Instance.SetGold(-price);
        inventoryData.AddItem(item, qty);
        Toggle(false);
    }
}
