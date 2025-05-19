using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    EquipableItemSO weapon = null;
    PlayerControler controler;
    [SerializeField]
    AudioClip equipSFX, unEquipSFX;
    private void Awake()
    {
        controler = GetComponent<PlayerControler>();
    }
    private void Start()
    {
        Observer.Instance.AddToList(ObserverCostant.INVENTORY_REMOVE_WEAPON, RemoveItem);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList(ObserverCostant.INVENTORY_REMOVE_WEAPON, RemoveItem);
    }


    public void SetWeapon(EquipableItemSO weaponItemSO)
    {   

        this.weapon = weaponItemSO;
        controler.PlayerStats.weapon = weaponItemSO;
        InventoryItem equippedItem = new InventoryItem
        {
            item = weaponItemSO,
            quantity = 1,
        };
        SoundManager.Instance.PlayOS(equipSFX);
        Observer.Instance.Notify<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, weapon);
    }

    public EquipableItemSO GetCurWeapon()
    {
        return weapon;
    }
    public void RemoveItem()
    {
        SoundManager.Instance.PlayOS(unEquipSFX);
        controler.PlayerStats.weapon = null;
    }
    public void LoadCurrentWeapon()
    {
        if (controler == null)
        {
            Debug.LogError("controler");
            return;
        }
        if (controler.PlayerStats == null)
        {
            Debug.LogError("controler.PlayerStats");
            return;
        }
        if (weapon == controler.PlayerStats.weapon)
            return;
        if (controler.PlayerStats.weapon == null)
        {
            RemoveItem();
            return;
        }
        weapon = controler.PlayerStats.weapon;
        InventoryItem equippedItem = new InventoryItem
        {
            item = weapon,
            quantity = 1,
        };
        //OnWeaponEquipped?.Invoke(equippedItem);
        Observer.Instance.Notify<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, weapon);
    }
}
