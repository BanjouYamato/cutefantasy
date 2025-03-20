using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControler : MonoBehaviour
{
    [SerializeField]    
    UIInventoryPage inventoryPage;
    [SerializeField]
    InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    [SerializeField]
    AudioClip dropClip, edibleSFX, plantableSFX;

    private void Start()
    {
        PrepareUI();
        PrepareinventoryData();
    }

    private void OnDestroy()
    {   
        ClearUI();
        inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
    }

    private void PrepareinventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
    }

    public void CreateItemNewGame()
    {
        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
            inventoryData.AddItem(item);
            
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryPage.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryPage.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryPage.InitializedInventory(inventoryData.size);
        inventoryPage.ExitButton();
        this.inventoryPage.OnSwapItems += HandleSwapItems;
        this.inventoryPage.OnStartDragging += HandleDragging;
        this.inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        this.inventoryPage.OnItemDescriptionRequested += HandleItemDescriptionRequest;
    }
    void ClearUI()
    {
        inventoryPage.ClearInventory();
        this.inventoryPage.OnSwapItems -= HandleSwapItems;
        this.inventoryPage.OnStartDragging -= HandleDragging;
        this.inventoryPage.OnItemActionRequested -= HandleItemActionRequest;
        this.inventoryPage.OnItemDescriptionRequested -= HandleItemDescriptionRequest;
    }

    private void HandleItemDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        inventoryPage.CreateDescriptionItem(inventoryItem, itemIndex);
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryPage.ShowItemAction(itemIndex);
            inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {   if (itemAction == null)
                inventoryPage.ShowItemAction(itemIndex);
            inventoryPage.AddAction("Delete", () =>
            {
                SoundManager.Instance.PlayOS(dropClip);
                DropItem(itemIndex, inventoryItem.quantity);
            });
        }
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerFormAction(PlayerControler.instance.gameObject);
            if (inventoryItem.item is EdibleItemSO)
            {
                SoundManager.Instance.PlayOS(edibleSFX);
                inventoryData.RemoveItem(itemIndex, 1);
            }
            else if (inventoryItem.item is PlantableItemSO)
            {
                SoundManager.Instance.PlayOS(plantableSFX);
                GameControler.Instance.runTimeData.currentSeed = inventoryItem;
            }
                
            //audioSource.PlayOneShot(itemAction.actionSFX);
            /*if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryPage.ResetSelection();*/
        }
        inventoryPage.ResetSelection();
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryPage.ResetSelection();
        //audioSource.PlayOneShot(dropClip);
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        inventoryPage.CreateDraggedItem(inventoryItem.item.Icon, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    public void OpenInventoryUI()
    {
        SoundManager.Instance.PlayOS();
        inventoryPage.Show();
        foreach (var item in inventoryData.GetCurInventoryState())
        {
            inventoryPage.UpdateData(item.Key,
                item.Value.item.Icon,
                item.Value.quantity);
        }
    }
    void CloseInventoryUI()
    {
        inventoryPage.Hide();
    }
}
