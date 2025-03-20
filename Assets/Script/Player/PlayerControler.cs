using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Collider2D col { get; private set; }
    public Player Player { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerAction PlayerAction { get; private set; }
    public PlayerStateMachine PlayerStateMachine { get; private set; }
    public EffectAction EffectAction { get; private set; }
    public PlayerKnockBack PlayerKnockBack { get; private set; }
    public Shadows Shadows { get; private set; }
    public AgentWeapon AgentWeapon { get; private set; }
    public PlayerFishing PlayerFishing { get; private set; }

    [SerializeField]
    PlayerStats playerStats;

    public PlayerStats PlayerStats { get => playerStats; }

    public static PlayerControler instance { get; private set; }


    private void Awake()
    {   
        instance = this;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        Player = GetComponent<Player>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAction = GetComponent<PlayerAction>();
        PlayerStateMachine = GetComponent<PlayerStateMachine>();
        EffectAction = GetComponent<EffectAction>();
        PlayerKnockBack = GetComponent<PlayerKnockBack>();
        Shadows = GetComponent<Shadows>();
        AgentWeapon = GetComponent<AgentWeapon>();
        PlayerFishing = GetComponent<PlayerFishing>();
    }
    private void Start()
    {
        AgentWeapon.LoadCurrentWeapon();
    }
}
