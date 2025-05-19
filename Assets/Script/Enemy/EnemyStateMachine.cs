using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    EnemyBaseState currentState;
    public Animator anim { get; private set; }
    EnemyControler controler;

    private void Start()
    {
        controler = GetComponent<EnemyControler>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        currentState = new EnemyIdleState(controler, Direction.down);
        currentState.EnterState();
    }
    private void Update()
    {
        currentState.UpdateState();
    }
    public void ChangeEnemyState(EnemyBaseState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
    public void PlayAnimation(string animationName)
    {
        anim.Play(animationName, 0, 0);
    }
}
