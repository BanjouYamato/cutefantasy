using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    InventorySO inventoryData;
    [SerializeField]
    AudioClip pickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FruitTree fruitTree = collision.GetComponent<FruitTree>();
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.item, item.qty);
            SoundManager.Instance.PlayOS(pickUpSFX);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.qty = reminder;
        }
        if (fruitTree != null && fruitTree.IsFUllyGrow())
        {
            SoundManager.Instance.PlayOS(pickUpSFX);
            fruitTree.HarvestFruit();
        }
    }



}
