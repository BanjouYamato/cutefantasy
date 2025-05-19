using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EyeControl : MonoBehaviour
{
    SpriteRenderer sp;
    Collider2D col;
    Rigidbody2D rb;
    Animator anim;
    float distanceAppear = 5f;
    Observer observer;
    float speed = 1f;
    [SerializeField]
    string laserAnimName, baseAnimName, deathAnimName;
    bool isMoving;
    bool isPaused;
    LaserBody body;
    [SerializeField]
    AudioClip deathSFX, laserSFX;

    private void Start()
    {
        observer = Observer.Instance;
        sp = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        observer.AddToList<bool>(ObserverCostant.BOSS_TELEPORT, TeleportEye);
        observer.AddToList(ObserverCostant.BOSS_LASERBEAM_ON, LaserBeam);
        observer.AddToList(ObserverCostant.BOSS_LASERBEAM_OFF, LaserBeamOff);
    }
    private void OnDestroy()
    {
        observer.RemoveToList<bool>(ObserverCostant.BOSS_TELEPORT, TeleportEye);
        observer.RemoveToList(ObserverCostant.BOSS_LASERBEAM_ON, LaserBeam);
        observer.RemoveToList(ObserverCostant.BOSS_LASERBEAM_OFF, LaserBeamOff);
        transform.DOKill();
    }
    
    public void EyeAppear(Vector2 dir, Transform bossPos)
    {
        Transform childTransform = transform.GetChild(0);
        body = childTransform.GetComponentInChildren<LaserBody>();
        body.gameObject.SetActive(false);
        var sequence = DOTween.Sequence();
        sequence.AppendCallback(() => col.enabled = false);
        sequence.Append(transform.DOBlendableLocalMoveBy(dir * distanceAppear, 0.5f));
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() =>
        {
            col.enabled = true;
            StartCoroutine(CircleMove(bossPos));
        });
    }
    IEnumerator CircleMove(Transform centerPos)
    {
        isMoving = true;
        isPaused = false;
        float angle = Mathf.Atan2(transform.position.y - centerPos.position.y,
            transform.position.x - centerPos.position.x);
        while (isMoving)
        {   
            if (!isPaused)
            {
                float x = centerPos.position.x - Mathf.Cos(angle) * distanceAppear;
                float y = centerPos.position.y - Mathf.Sin(angle) * distanceAppear;
                transform.position = new Vector3(x, y, transform.position.z);
                angle += speed * Time.deltaTime;
            }
            yield return null;
        }
    }
    void PauseMovement(bool val)
    {
        isPaused = val;
    }
    void TeleportEye(bool val)
    {
        sp.enabled = val;
        col.enabled = val;
    }
    void LaserBeam()
    {   
/*        StopAllCoroutines();
        isMoving = false;*/
        PauseMovement(true);
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() => anim.SetTrigger(laserAnimName));
        sequence.AppendInterval(0.5f);
        sequence.AppendCallback(() => SoundManager.Instance.PlayOS(laserSFX));
        sequence.AppendCallback(() =>
        {
            body.GetComponent<LaserBody>().StartLaser();
        });
        
    }
    void LaserBeamOff()
    {
        anim.Play(baseAnimName);
        body.GetComponent<LaserBody>().IsHitOff();
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() =>
        {   
            PauseMovement(false);
            //StartCoroutine(CircleMove(transform.parent));
        });
    }
    IEnumerator EyeDeath()
    {
        SoundManager.Instance.PlayOS(deathSFX);
        col.enabled = false;
        anim.Play(deathAnimName);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControler player = collision.GetComponent<PlayerControler>();
        if (collision.GetComponent<DamageSource>() != null)
        {
            observer.Notify(ObserverCostant.BOSS_EYE);
            StopAllCoroutines();
            StartCoroutine(EyeDeath());
        }
        else if (player != null)
        {
            player.Player.TakeDamage(1, transform);
        }
    }

}
