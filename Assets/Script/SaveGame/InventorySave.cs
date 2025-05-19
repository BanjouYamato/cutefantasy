using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponListWrapper
{
    public List<InventoryItem> inventoryItems;
    public WeaponListWrapper(List<InventoryItem> inventoryItems)
    {
        this.inventoryItems = inventoryItems;
    }
}

public class InventorySave : SaveLoadControler
{
    [SerializeField]
    InventoryControler controler;

    [SerializeField]
    InventorySO inventoryData;

    public override void OnLoad()
    {
        inventoryData.SetItemData(SaveGameManager.Instance.Load<WeaponListWrapper>(GameConstant.GAME_SAVE_INVENTORY));
    }

    public override void OnNewGame()
    {
        controler.CreateItemNewGame();
    }

    public override void Onsave()
    {
        inventoryData.SaveItem();
    }
}
