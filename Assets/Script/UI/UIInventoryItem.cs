using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IEndDragHandler, IDropHandler, IPointerUpHandler
{
    [SerializeField]
    Image itemimage;
    [SerializeField]
    TextMeshProUGUI qtyText;
    [SerializeField]
    Image borderImage;
    bool isHolding;
    float lastClickTime;

    public event Action<UIInventoryItem> OnItemClicked,
        OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
        OnrightMouseClicked, OnItemDobleClick;

    bool empty = true;
    private void Awake()
    {
        ResetData();
        Deselect();
    }
    public void ResetData()
    {
        this.itemimage.gameObject.SetActive(false);
        empty = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite, int qty)
    {
        this.itemimage.gameObject.SetActive(true);
        this.itemimage.sprite = sprite;
        this.qtyText.text = qty.ToString();
        empty = false;
    }
    public void Select()
    {
        borderImage.enabled = true;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty || eventData.button == PointerEventData.InputButton.Right) return;
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
        Invoke(nameof(TriggerHoldAction), 1f);
        
    }
    private void TriggerHoldAction()
    {
        if (isHolding)
        {
            OnrightMouseClicked?.Invoke(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        CancelInvoke(nameof(TriggerHoldAction));

        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        if (timeSinceLastClick <= 0.5f)
        {
            OnItemDobleClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
