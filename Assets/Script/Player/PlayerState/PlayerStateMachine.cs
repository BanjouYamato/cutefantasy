using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public BaseState currentState;
    public Animator anim;
    PlayerControler controler;
    string currentAnim;
    private void Start()
    {   
        controler = GetComponent<PlayerControler>();
        currentState = new IdleState(controler, controler.PlayerStats.dir);
        currentState.EnterState();
    }
    private void Update()
    {
        currentState.UpdateState();
    }
    public void ChangeState(BaseState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
    public void PlayAnimation(string animationName, int layerIndex)
    {   
        if (currentAnim == animationName) return;
        anim.Play(animationName, layerIndex, 0f);
        currentAnim = animationName;
    }
}
