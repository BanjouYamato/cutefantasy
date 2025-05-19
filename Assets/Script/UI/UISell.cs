using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISell : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshProUGUI totalPriceTxt, qtyTxt;

    [SerializeField]
    Button increase, decrease, agreeBuy, cancelBuy;

    int qty, price, qtyCanSell, itemIndex;

    private void Start()
    {
        increase.onClick.AddListener(() =>
        {
            qty++;
            InformAboutChange();
        });
        decrease.onClick.AddListener(() =>
        {
            qty--;
            InformAboutChange();
        });
        agreeBuy.onClick.AddListener(EnterRequest);
        cancelBuy.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS();
            Toggle(false);
        });
    }

    public void SetData(Sprite sprite, int price, int qtyCanSell, int itemIndex)
    {
        this.itemIndex = itemIndex;
        icon.sprite = sprite;
        qty = 1;
        this.qtyCanSell = qtyCanSell;
        qtyTxt.text = qty.ToString();
        this.price = price;
        totalPriceTxt.text = price.ToString();
    }
    int GetTotalPrice()
    {
        return price * qty;
    }
    void InformAboutChange()
    {
        qtyTxt.text = qty.ToString();
        totalPriceTxt.text = GetTotalPrice().ToString();
    }
    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    void EnterRequest()
    {
        SoundManager.Instance.PlayOS();
        if (qty <= qtyCanSell &&
            GameControler.Instance.gold >= GetTotalPrice())
        {
            GameControler.Instance.runTimeData.inventoryData.RemoveItem(itemIndex, qty);
            GameControler.Instance.SetGold(GetTotalPrice());
            Toggle(false);
        }
            
    }
    public void ResetData()
    {
        this.icon.gameObject.SetActive(false);
    }
}
