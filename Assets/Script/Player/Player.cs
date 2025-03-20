using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerControler controler;
    public bool Death { get; private set; }
    [SerializeField]
    AudioClip takeDmgSFX, DeathSFX;

    private void Start()
    {
        controler = GetComponent<PlayerControler>();
        Invoke(nameof(UpdatePlayerBar), 0.1f);
    }

    public void UpdatePlayerBar()
    {
        Observer.Instance.Notify<float>(ObserverCostant.UPDATE_HPBAR, InteliToFloatHP());
    }

    private void Update()
    {
        if (CanNotAction())
            return;
        controler.PlayerStats.dir = GetDirectionFromInput();
    }
    Direction GetDirectionFromInput()
    {   
        if (controler.PlayerMovement.movement == Vector2.zero)
        {   
            return controler.PlayerStats.dir;
        }
        else
        {
            if (Mathf.Abs(controler.PlayerMovement.movement.y)
            > Mathf.Abs(controler.PlayerMovement.movement.x))
            {
                if (controler.PlayerMovement.movement.y > 0)
                    return Direction.up;
                else if (controler.PlayerMovement.movement.y < 0)
                    return Direction.down;
            }
            else if (Mathf.Abs(controler.PlayerMovement.movement.y)
                < Mathf.Abs(controler.PlayerMovement.movement.x))
                return Direction.side;
            else
                return controler.PlayerStats.dir;
        }
        return controler.PlayerStats.dir;
    }
    public bool CanNotAction()
    {
        return (Death || controler.PlayerAction.PerformingAction || controler.PlayerFishing.IsInWater
            || controler.PlayerKnockBack.KnockBack || GameStateManager.Instance.curstate != GameState.normal);
    }
    public void TakeDamage(int damage, Transform enemyPos)
    {
       
        if (controler.PlayerKnockBack.KnockBack)
            return;
        if (Debug.isDebugBuild)
        {
            if (!GameControler.Instance.unDead)
            {
                SoundManager.Instance.PlayOS(takeDmgSFX);
                controler.PlayerStats.HP -= damage;
                controler.PlayerStats.HP = Mathf.Clamp(controler.PlayerStats.HP, 0, controler.PlayerStats.MaxHP);
                UpdatePlayerBar();
            }
        }
        else
        {
            SoundManager.Instance.PlayOS(takeDmgSFX);
            controler.PlayerStats.HP -= damage;
            controler.PlayerStats.HP = Mathf.Clamp(controler.PlayerStats.HP, 0, controler.PlayerStats.MaxHP);
            UpdatePlayerBar();
        }
        controler.PlayerKnockBack.GetKnockBack(enemyPos);
    }
    public void TakeEnergy()
    {
        controler.PlayerStats.energy--;
        controler.PlayerStats.energy = Mathf.Clamp(controler.PlayerStats.energy, 0, controler.PlayerStats.maxEnergy);
        Observer.Instance.Notify<float>(ObserverCostant.UPDATE_ENERGYBAR, InteliToFloatEnergy());
    }
    float InteliToFloatHP()
    {   
        return (float)controler.PlayerStats.HP / (float)controler.PlayerStats.MaxHP;
    }
    float InteliToFloatEnergy()
    {
        return (float)controler.PlayerStats.energy / (float)controler.PlayerStats.maxEnergy;
    }
    public void DetecDeath()
    {
        if (controler.PlayerStats.HP <= 0)
        {
            SoundManager.Instance.PlayOS(DeathSFX);
            Death = true;
            GameStateManager.Instance.ChangeState(GameState.GameOver);
            controler.col.enabled = false;
            controler.PlayerStateMachine.ChangeState(new DeathState(controler, controler.PlayerStats.dir));
        }
    }
}
