using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RunTimeData : ScriptableObject
{
    public PlayerStats stats;
    public InventorySO inventoryData;

    public InventoryItem currentSeed;

    public List<DugTileData> dugTileList = new List<DugTileData>();

    public List<FruitTreeData> fruitTreeDataList = new List<FruitTreeData>();

    public void ResetListData()
    {   
        inventoryData.ResetData();
        dugTileList.Clear();
        fruitTreeDataList.Clear();
    }
    public void UpdateCurrentSeed()
    {
        inventoryData.RemoveItem(inventoryData.GetIndextOf(currentSeed), 1);
        currentSeed.ChangeQuantity(currentSeed.quantity--);
        if (currentSeed.quantity == 0 )
        {
            currentSeed = null;
            Observer.Instance.Notify(ObserverCostant.UI_PLANT_BUTTON);
        }
    }
}
