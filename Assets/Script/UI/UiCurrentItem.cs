using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCurrentItem : MonoBehaviour
{
    bool isEmpty;
    [SerializeField]
    Image itemImage;

    private void Start()
    {
        Observer.Instance.AddToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, SetData);
        Observer.Instance.AddToList(ObserverCostant.INVENTORY_REMOVE_WEAPON, ResetItem);
    }
    private void OnDisable()
    {
        Observer.Instance.RemoveToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, SetData);
        Observer.Instance.RemoveToList(ObserverCostant.INVENTORY_REMOVE_WEAPON, ResetItem);
    }

    void SetData(ItemScriptable img)
    {
        if (img == null)
        {
            ResetItem(); 
            return;
        }
        if (isEmpty)
        {
            isEmpty = false;
            itemImage.gameObject.SetActive(true);
        }
        itemImage.sprite = img.Icon;
    }
    void ResetItem()
    {
        isEmpty = true;
        itemImage.gameObject.SetActive(false);
    }
}
