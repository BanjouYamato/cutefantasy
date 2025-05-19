using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartnerBuySlotUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Color chooseColor;
    private Color _orginalColor;
    private UiPartnerBuy _uiPartnerBuy;
    private PartnerStats _partnerStats;
    [SerializeField] Image _panelImage, _avatarImage;
    [SerializeField] TextMeshProUGUI _nameTxt, _priceTxt;
    [SerializeField] AudioClip _chooseClip;

    public void Init(PartnerStats partnerData, UiPartnerBuy ui)
    {
        _orginalColor = _panelImage.color;
        _avatarImage.sprite = partnerData.avatar;
        _nameTxt.text = partnerData.name;
        _priceTxt.text = partnerData.price.ToString();
        _uiPartnerBuy = ui;
        _partnerStats = partnerData;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangePanelColor(chooseColor);
        SoundManager.Instance.PlayOS(_chooseClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ChangePanelColor(_orginalColor);
        _uiPartnerBuy.OpenBuyPartnerPanel(_partnerStats);
    }

    void ChangePanelColor(Color color)
    {
        _panelImage.color = color;
    }
}
