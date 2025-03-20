using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    down,
    up,
    side
}
public class PlayerMovement : MonoBehaviour
{
    PlayerControler controler;

    public AudioClip dashSFX, moveSFX;
    public Vector2 movement { get; set; }
    bool rightFace = true;
    bool isDashing;
    bool dashCD;
    InputManager input;
    private void Awake()
    {
        input = InputManager.Instance;

    }
    private void Start()
    {
        controler = GetComponent<PlayerControler>();
        if (transform.position.z != 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    private void Update()
    {
        if (controler.Player.CanNotAction())
            return;
        movement = input.GetJoystickInput().normalized;
        Dash();
        PlayerFlip();
    }
    private void FixedUpdate()
    {   
        if (controler.Player.CanNotAction())
            return;
        PlayerMove(SpeedChange());
    }
    float SpeedChange()
    {
        if (controler.Player.CanNotAction())
            return 0f;
        if (isDashing)
            return controler.PlayerStats.dashSpeed;
        return controler.PlayerStats.speed;
    }

    public void PlayerMove(float speed)
    {   
        controler.Rigidbody2D.MovePosition(controler.Rigidbody2D.position + movement * (speed * Time.fixedDeltaTime));
    }
    
    void PlayerFlip()
    {   
        if (Mathf.Abs(movement.y) > Mathf.Abs(movement.x))
        {
            if (!rightFace)
                rightFace = true;
        }
        else if (Mathf.Abs(movement.y) < Mathf.Abs(movement.x))
        {
            if (rightFace && input.GetJoystickInput().x < 0)
                rightFace = false;
                
            else if (!rightFace && input.GetJoystickInput().x > 0)
                rightFace = true;
        }
        flip();
    }
    void flip()
    {
        transform.localScale = new Vector3(!rightFace ? -1 : 1, 1, 1);
    }
    void Dash()
    {
        if (controler.PlayerStats.energy == 0)
            return;
        if (input.isDashPressed && !controler.Player.CanNotAction())
        {
            if (!dashCD && !controler.PlayerKnockBack.KnockBack)
            {
                dashCD = true;
                isDashing = true;
                controler.Player.TakeEnergy();
                SoundManager.Instance.PlayOS(dashSFX);
                StartCoroutine(DashRoutine());
            }
        }
    }
    IEnumerator DashRoutine()
    {
        float dashDuration = 0.1f;
        float dashCD = 0.3f;
        int shadowCount = 3;
        float shadowDelay = dashDuration / shadowCount;
        for (int i = 0; i < shadowCount; i++)
        {
            controler.Shadows.GetShadow();
            yield return new WaitForSeconds(shadowDelay);
        }
        isDashing = false;
        //speed = savedSpeed;
        yield return new WaitForSeconds(dashCD);
        this.dashCD = false;
    }
    public bool GetRightFace()
    {
        return rightFace;
    }
    public Vector2 VectorDirPlayer()
    {
        if (controler.PlayerStats.dir == Direction.up) return Vector2.up;
        else if (controler.PlayerStats.dir == Direction.down) return Vector2.down;
        else
        {
            if (rightFace) return Vector2.right;
            else return Vector2.left;
        }
    }
}
