using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallState : MonoBehaviour
{
    public enum state
    {
        moving,
        hit,
        off
    }
    public state curState;
    Animator anim;
    [SerializeField]
    string movingAnim, hitAnim, offAnim;
    [SerializeField]
    float shootForce;
    float timer;
    float hitDuration = 5f;
    Rigidbody2D rb;
    float speed;
    float movingDuration;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {   
        if (curState == state.moving)
        {
            timer += Time.deltaTime;
            if (timer >= movingDuration)
            {
                GetHit();
            }
        }
        if (curState == state.hit)
        {
            timer += Time.deltaTime;
            if (timer >= hitDuration)
            {
                ChangeState(state.off);
            }
        }
    }
    public void FireBallFly(Vector2 dir)
    {
        rb.AddForce(dir * shootForce, ForceMode2D.Impulse);
    }
    void ChangeState(state newState)
    {
        curState = newState;
        UpdateState();
    }
    public void GetHit()
    {
        if (curState == state.hit)
            return;
        ChangeState(state.hit);
        timer = 0;
        //UpdateState();
    }
    public void ResetState()
    {
        curState = state.moving;
        movingDuration = Random.Range(1f, 5f);
        timer = 0f;
        UpdateState() ;
    }
    void UpdateState()
    {
        switch (curState)
        {
            case state.moving:
                anim.Play(movingAnim);
                break;
            case state.hit:
                anim.SetTrigger(hitAnim);
                rb.velocity = Vector3.zero;
                break;
            case state.off:
                anim.SetTrigger(offAnim);
                break;
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    IEnumerator OffFlame()
    {
        anim.SetTrigger(offAnim);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public bool IsFlameMoving()
    {
        return curState == state.moving;
    }
}
