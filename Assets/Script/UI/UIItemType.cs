using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemType : MonoBehaviour, IPointerClickHandler
{
    Image _image;
    Color _originalColor;
    [SerializeField] Color _chooseColor;
    [SerializeField] ShopControler _shopControler;
    [SerializeField] ItemType _itemType;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _originalColor = _image.color;
        _shopControler.RegisterUIItem(this);
    }
    public void UpdateVisual()
    {
        if (IsCurrentSelected())
            _image.color = _chooseColor;
        else
            _image.color = _originalColor;
    }
    bool IsCurrentSelected()
    {
        return _shopControler.GetItemType() == _itemType;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsCurrentSelected()) return;
        _shopControler.UpdateItemType(_itemType);
    }
}
