using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUsingWeapon : MonoBehaviour, IPointerClickHandler
{
    /*[SerializeField]
    UIInventoryItem item;*/

    [SerializeField]
    Image childImage;

    bool isEmpty;

    private void OnEnable()
    {
        Observer.Instance.AddToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, SetData);
    }
    private void OnDisable()
    {
        Observer.Instance.RemoveToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, SetData);
    }

    public void SetData(ItemScriptable item)
    {
        if (item == null)
        {
            RemoveUsingItem();
            return;
        }
        if (isEmpty)
        {
            isEmpty = false;
            childImage.gameObject.SetActive(true);
        }
        childImage.sprite = item.Icon;
    }
    public void UpdateData()
    {
        PlayerControler player = PlayerControler.instance;
        if (player.PlayerStats.weapon == null)
        {
            RemoveUsingItem();
            return;
        }
        if (childImage.sprite == player.PlayerStats.weapon.Icon) 
            return;
        childImage.sprite = player.PlayerStats.weapon.Icon;   
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isEmpty) 
            return;
        RemoveUsingItem();
        Observer.Instance.Notify(ObserverCostant.INVENTORY_REMOVE_WEAPON);
    }
    void RemoveUsingItem()
    {
        isEmpty = true;
        childImage.gameObject.SetActive(false);
    }
    
}
