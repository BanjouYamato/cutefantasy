using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    WeaponManager weaponManager;

    PlayerControler controler;

    float comboResetTime = 1f;
    public float lastAttackTime;
    public float attackDuration;
    protected float lastAttackTIme;
    public int comboStep;
    public bool PerformingAction { get; set; }

    private void Start()
    {   
        controler = GetComponent<PlayerControler>();
    }
    private void Update()
    {   
        if (controler.Player.CanNotAction())
            return;
        if (PerformingAction)
            return;
        if (InputManager.Instance.isActionPressed)
        {
            if (controler.PlayerStats.energy == 0)
                return;
            EquipableItemSO curItem = controler.PlayerStats.weapon;
            if (curItem == null ) 
                return;
            if (!CheckFishRod(curItem))
            {
                DoAttack(curItem);
                controler.Player.TakeEnergy();
                return;
            }
            if (!controler.PlayerFishing.IsInWater)
            {
                PerformingAction = true;
                controler.Player.TakeEnergy();
                controler.PlayerStateMachine.ChangeState(new ThrowFishRod(controler, controler.PlayerStats.dir));
                weaponManager.Action();
            }
            else
            {
                controler.PlayerFishing.FinishFishing();
            } 
        }  
    }

    public void DoAttack(EquipableItemSO weapon)
    {
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboStep = 0;
        }

        lastAttackTime = Time.time;
        comboStep++;
        if (comboStep > weapon.maxComboStep) 
            comboStep = 1;
        PerformingAction = true;
        controler.PlayerStateMachine.ChangeState(new AttackState(controler, controler.PlayerStats.dir));
        weaponManager.Action();
        EffectWeapon(weapon);
        
    }
    public void EffectWeapon(EquipableItemSO equipable)
    {   
        if (equipable is ToolItemSO tool)
        {
            switch (equipable.ItemName)
            {
                case "Axe":
                    controler.EffectAction.ChopTree(tool.value);
                    break;
                case "Hammer":
                    controler.EffectAction.BreakRock(tool.value);
                    break;
                case "Hoe":
                    FarmingSystem.instance.Gardening(transform);
                    break;
                case "Watering":
                    controler.EffectAction.PlayerWater();
                    break;
            }
        }
        
    }
    bool CheckFishRod(EquipableItemSO equipableItem)
    {
        return equipableItem.ItemName == "Fishing Rod";
    }
}
