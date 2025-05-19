using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "BaseStats/PlayerStats")]
public class PlayerStats : BaseStatSO
{
    public int energy, maxEnergy, mana, maxMana;
    public float dashSpeed;
    public Direction dir;
    public EquipableItemSO weapon;
    public void ResetStats(EquipableItemSO curWeapon = null)
    {
        HP = MaxHP;
        energy = maxEnergy;
        mana = maxMana;
        dir = Direction.down;
        weapon = curWeapon;
    }
    public void Healing(int hp)
    {
        Debug.Log("heal");
        HP += hp;
        HP = Mathf.Clamp(HP, 0, MaxHP);
        Observer.Instance.Notify<float>(ObserverCostant.UPDATE_HPBAR, (float)HP / (float)MaxHP);
    }
    public void StaminaRecovery(int ener)
    {
        Debug.Log("energy");
        energy += ener;
        energy = Mathf.Clamp(energy, 0, maxEnergy);
        Observer.Instance.Notify<float>(ObserverCostant.UPDATE_ENERGYBAR, (float)energy / (float)maxEnergy);
    }
}
