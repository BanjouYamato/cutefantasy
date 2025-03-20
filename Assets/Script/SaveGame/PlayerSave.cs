using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int HP, MP, Energy;
    public Direction dir;
    public Vector3 position;
    public EquipableItemSO weapon;
}

public class PlayerSave : SaveLoadControler
{
    PlayerControler controler;

    private void Awake()
    {
        controler = GetComponent<PlayerControler>();
    }

    public override void OnLoad()
    {
        PlayerData data = SaveGameManager.Instance.Load<PlayerData>(GameConstant.GAME_SAVE_PLAYERSTAT);
        controler.PlayerStats.HP = data.HP;
        controler.PlayerStats.mana = data.MP;
        controler.PlayerStats.energy = data.Energy;
        controler.PlayerStats.dir = data.dir;
        transform.position = data.position;
        controler.PlayerStats.weapon = data.weapon;
        Observer.Instance.Notify<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, data.weapon);
    }

    public override void OnNewGame()
    {
        controler.PlayerStats.ResetStats();
        Observer.Instance.Notify(ObserverCostant.INVENTORY_REMOVE_WEAPON);
    }

    public override void Onsave()
    {
        PlayerData data = new PlayerData
        {
            HP = controler.PlayerStats.HP,
            MP = controler.PlayerStats.mana,
            Energy = controler.PlayerStats.energy,
            dir = controler.PlayerStats.dir,
            position = transform.position,
            weapon = controler.PlayerStats.weapon
        };
        SaveGameManager.Instance.Save<PlayerData>(GameConstant.GAME_SAVE_PLAYERSTAT, data);
    }
}
