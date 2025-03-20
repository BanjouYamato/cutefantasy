using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCollision : MonoBehaviour
{
    [SerializeField]
    AudioClip hitFlameSfx;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerControler controler))
        {   
            if (transform.GetComponentInParent<FireBallState>().IsFlameMoving())
                transform.GetComponentInParent<FireBallState>().GetHit();
            SoundManager.Instance.PlayOS(hitFlameSfx);
            controler.Player.TakeDamage(1, transform);
        }
        
    }
}
