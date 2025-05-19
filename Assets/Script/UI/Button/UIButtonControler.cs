using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonControler : MonoBehaviour
{
    [SerializeField]
    Button inventoryButton, menuButton;

    [SerializeField]
    InventoryControler inventoryControler;

    [SerializeField]
    UIMenu uiMenu;

    [SerializeField]
    TextMeshProUGUI goldTMP;

    private void Start()
    {
        inventoryButton.onClick.AddListener(inventoryControler.OpenInventoryUI);
        menuButton.onClick.AddListener(uiMenu.Show);
        GameControler.Instance.onGoldChanged += UpdateGold;
        UpdateGold(GameControler.Instance.gold);
    }

    public void UpdateGold(int val)
    {
        goldTMP.text = val.ToString();
    }
    private void OnDestroy()
    {
        GameControler.Instance.onGoldChanged -= UpdateGold;
    }
}
