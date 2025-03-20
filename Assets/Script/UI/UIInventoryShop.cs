using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryShop : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Image border, icon;

    [SerializeField]
    TextMeshProUGUI qtyTxt;

    int price;

    float lastClickTime;

    public Action<UIInventoryShop> OnSeletedItem, OnSellItem;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.icon.gameObject.SetActive(false);
    }

    public void Select()
    {
        border.enabled = true;
    }

    public void Deselect()
    {
        border.enabled = false;
    }

    public void SetData(Sprite sprite, int qty, int price)
    {
        this.icon.gameObject.SetActive(true);
        this.icon.sprite = sprite;
        this.qtyTxt.text = qty.ToString();
        this.price = price;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;
        if (timeSinceLastClick <= 0.5f)
        {
            OnSellItem?.Invoke(this);
        }
        else
        {
            OnSeletedItem?.Invoke(this);
        }
    }
}
