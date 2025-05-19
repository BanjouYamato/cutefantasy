using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopSlot : MonoBehaviour, IPointerClickHandler
{

    public Image itemImage;


    public TextMeshProUGUI itemName;


    public TextMeshProUGUI price;

    public ItemScriptable item;


    public Action<UIShopSlot> OnClickItemBuy;

    public void SetData(ItemScriptable item)
    {   
        this.item = item;
        itemImage.sprite = item.Icon;
        itemImage.SetNativeSize();
        RectTransform rectTransform = itemImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta *= 4;
        rectTransform.sizeDelta = new Vector2(
            Mathf.Clamp(rectTransform.sizeDelta.x, 0, 64),
            Mathf.Clamp(rectTransform.sizeDelta.x, 0, 64));
        itemName. text = item.ItemName;
        price.text = item.Price.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        OnClickItemBuy?.Invoke(this);
    }
}
