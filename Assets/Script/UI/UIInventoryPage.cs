using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    UIInventoryItem itemPrefab;
    [SerializeField]
    RectTransform contentPanel;
    [SerializeField]
    MouseFollower mouseFollower;
    [SerializeField]
    ItemDescription itemDescription;
    [SerializeField]
    UIUsingWeapon uIUsingWeapon;
    [SerializeField]
    Button exitBttn;

    List<UIInventoryItem> items = new List<UIInventoryItem>();

    int curdragedItemIndex = -1;

    public event Action<int> OnItemActionRequested,
         OnStartDragging, OnItemDescriptionRequested;

    public event Action<int, int> OnSwapItems;

    [SerializeField]
    ItemActionPanel actionPanel;

    private void Awake()
    {
        Hide();
    }
    public void InitializedInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(contentPanel);
            items.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnrightMouseClicked += HandleShotItemDescription;
            item.OnItemDobleClick += HandleShowItemActions;
        }
    }
    public void ClearInventory()
    {
        foreach (var item in items)
        {
            item.OnItemClicked -= HandleItemSelection;
            item.OnItemBeginDrag -= HandleBeginDrag;
            item.OnItemDroppedOn -= HandleSwap;
            item.OnItemEndDrag -= HandleEndDrag;
            item.OnrightMouseClicked -= HandleShotItemDescription;
            item.OnItemDobleClick -= HandleShowItemActions;
        }
        items.Clear();
    }

    private void HandleShotItemDescription(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
            return;
        OnItemDescriptionRequested?.Invoke(index);
    }

    public void UpdateData(int itemIndex,
        Sprite itemImage, int itemQty)
    {
        if (items.Count > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQty);
        }
    }
    private void HandleShowItemActions(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (index == -1)
            return;
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem item)
    {
        ResetDraggtedItem();
    }

    private void HandleSwap(UIInventoryItem item)
    {
        int index = items.IndexOf(item);
        if (curdragedItemIndex == -1)
            return;
        if (index == -1)
            return;
        OnSwapItems?.Invoke(curdragedItemIndex, index);
        HandleItemSelection(item);
    }

    private void ResetDraggtedItem()
    {
        mouseFollower.Toogle(false);
        curdragedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem item)
    {   
        int index = items.IndexOf(item);
        if (index == -1) 
            return;
        curdragedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }
    public void CreateDraggedItem(Sprite sprite, int qty)
    {
        mouseFollower.Toogle(true);
        mouseFollower.SetData(sprite, qty);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {   
        ResetSelection();
        int index = items.IndexOf(item);
        actionPanel.Toggle(false);
        if (index == -1) return;
        items[index].Select();

    }

    public void Show()
    {
        UiHelper.Toogle(true);
        gameObject.SetActive(true);
        ResetSelection();
        uIUsingWeapon.UpdateData();
    }

    public void ResetSelection()
    {
        DeselectAllItems();
    }
    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButton(actionName, performAction);
    }
    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = items[itemIndex].transform.position;
    }
    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in items)
        {
            item.Deselect();
        }
        actionPanel.Toggle(false);
        itemDescription.Toogle(false);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        mouseFollower.Toogle(false);
        itemDescription.Toogle(false);
        UiHelper.Toogle(false);
        ResetDraggtedItem();
    }
    public void ExitButton()
    {
        exitBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS();
            Hide();
        });
    }

    internal void ResetAllItems()
    {
        foreach (var item in items)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    internal void CreateDescriptionItem(InventoryItem inventoryItem, int index)
    {
        itemDescription.Toogle(true);
        itemDescription.SetData(inventoryItem.item);
        itemDescription.transform.position = items[index].transform.position;
    }
}
