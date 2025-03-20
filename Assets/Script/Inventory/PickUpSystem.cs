using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    InventorySO inventoryData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null )
        {
            int reminder = inventoryData.AddItem(item.item, item.qty);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.qty = reminder;
        }
    }
}
