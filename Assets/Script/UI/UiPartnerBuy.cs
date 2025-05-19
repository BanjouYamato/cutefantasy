using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPartnerBuy : MonoBehaviour
{   
    GameControler _gameControler;

    [Header("UI partner slot")]
    [SerializeField] List<PartnerStats> _partnerBuyList;
    public PartnerBuySlotUi partnerSlotPrefab;
    public Transform partnerShopContent;
    public GameObject partnerPanel;

    [Header("Ui buy partner panel")]
    [SerializeField] GameObject _partnerBuyPanel;
    [SerializeField] Image _avatarImage, _buyBttn;
    [SerializeField] TextMeshProUGUI _hp, _attack, _move, _range, _description, _buyTxt;
    [SerializeField] Sprite _canBuy, _canNotBuy;
    [SerializeField] Color _canBuyTxtColor, _canNotBuyTxtColor;
    [SerializeField] Button _buyBtn;
    [SerializeField] AudioClip _buyClip;
    private PartnerStats _currentPartner;
    
    private void Start()
    {   
        PreparePartner();
        Observer.Instance.AddToList<bool>(GameConstant.NPC_FEATURES_SHOP_PARTNER, PartnerPanelToggle);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<bool>(GameConstant.NPC_FEATURES_SHOP_PARTNER, PartnerPanelToggle);
    }
    void PreparePartner()
    {
        //_partnerBuyList = new List<PartnerStats>();
        foreach (var p in _partnerBuyList)
        {
            var slot = Instantiate(partnerSlotPrefab);
            slot.Init(p, this);
            slot.transform.SetParent(partnerShopContent);
        }
        PartnerPanelToggle(false);
        BuyPartnerPanelToggle(false);
    }

    public void OpenBuyPartnerPanel(PartnerStats partnerStats)
    {
        BuyPartnerPanelToggle(true);
        _avatarImage.sprite = partnerStats.avatar;
        _hp.text = partnerStats.MaxHP.ToString();
        _attack.text = partnerStats.attack.ToString();
        _move.text = partnerStats.speed.ToString();
        _range.text = partnerStats.range.ToString();
        _description.text = partnerStats.description.ToString();
        foreach (var p in _partnerBuyList)
        {
            if (GameControler.Instance.partnerTraveler.Contains(p))
            {   
                if (_buyBttn.gameObject.activeSelf)
                    _buyBttn.gameObject.SetActive(false);
            }
            else
            {
                if (!_buyBttn.gameObject.activeSelf)
                    _buyBttn.gameObject.SetActive(true);
                if (GameControler.Instance.gold >= partnerStats.price)
                {
                    _buyBttn.sprite = _canBuy;
                    _buyTxt.text = "Buy";
                    _buyTxt.color = _canBuyTxtColor;
                    _buyBtn.enabled = true;
                    _currentPartner = partnerStats;
                }
                else
                {
                    _buyBttn.sprite = _canNotBuy;
                    _buyTxt.text = "Can't Buy";
                    _buyTxt.color = _canNotBuyTxtColor;
                    _buyBtn.enabled = false;
                }
            }
        }
    }
    public void PartnerPanelToggle(bool toggle)
    {
        partnerPanel.gameObject.SetActive(toggle);
    }
    public void BuyPartnerPanelToggle(bool val)
    {
        _partnerBuyPanel.SetActive(val);
    }
    public void PerformBuyPartner()
    {
        SoundManager.Instance.PlayOS(_buyClip);
        GameControler.Instance.AddPartner(_currentPartner);
        GameControler.Instance.SetGold(-_currentPartner.price);
        BuyPartnerPanelToggle(false);
    }
}
