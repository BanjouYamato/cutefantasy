using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI itemName;

    [SerializeField]
    TextMeshProUGUI itemDescription;

    [SerializeField]
    Image icon;

    public void SetData(ItemScriptable item)
    {
        itemName.text = item.ItemName;
        itemDescription.text = item.Description;
        icon.sprite = item.Icon;
    }
    public void Toogle(bool val)
    {
        gameObject.SetActive(val);
    }

}
