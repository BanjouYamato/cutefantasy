using System.Collections;
using UnityEngine;
using DG.Tweening;
using BehaviorDesigner.Runtime;

public class BossData : MonoBehaviour
{
    [SerializeField]
    int Health;
    SpriteRenderer spriteRenderer;
    bool shield;
    int countEye;
    bool canDestroyEye;
    [SerializeField]
    BehaviorTree tree;
    [SerializeField]
    Animator anim;
    [SerializeField]
    string stunAnimName, deathAnimName;
    [SerializeField]
    CapsuleCollider2D col;
    [SerializeField]
    AudioClip caveMusic, takeDmgSFX, sturnSFX, beforeDeathSFX, deathSFX;
    public bool Shield
    {
        get { return shield; }
        private set { shield = value; }
    }
    int curHeath;

    private void Start()
    {   
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        curHeath = Health;
        Observer.Instance.AddToList(ObserverCostant.BOSS_EYE, EyeDestroyed);
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList(ObserverCostant.BOSS_EYE, EyeDestroyed);
        DOTween.KillAll();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shield)
        {
            DamageSource damage = collision.GetComponent<DamageSource>();
            if (damage != null)
            {
                SoundManager.Instance.PlayOS(takeDmgSFX);
                TakeDamage(damage.Weapon.Damage);
            }
        }
    }
    void TakeDamage(int damage)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(spriteRenderer.DOColor(Color.red, 0.1f)
            .SetLoops(2, LoopType.Yoyo));
        sequence.AppendCallback(() =>
        {
            curHeath -= damage;
            curHeath = Mathf.Clamp(curHeath, 0, Health);
        });
        sequence.AppendCallback(() =>
        {
            if (curHeath <= 0)
                StartCoroutine(DeathRoutine());
        });
    }
    IEnumerator DeathRoutine()
    {
        SoundManager.Instance.PlayOS(beforeDeathSFX);
        tree.enabled = false;
        anim.Play(deathAnimName);
        col.enabled = false;
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayOS(deathSFX);
        gameObject.SetActive(false);
        BossGate gate = FindObjectOfType<BossGate>();
        GameControler.Instance.SetBossDefeated(gate.bossID);
        gate.ObstacleBossPerform(false);
        PlayerControler.instance.PlayerStats.ResetStats(PlayerControler.instance.PlayerStats.weapon);
        SoundManager.Instance.BackGroundMusic(caveMusic);
    }
    public void GetShield()
    {   
        Shield = true;
        countEye = 4;
    }
    void EyeDestroyed()
    {
        if (canDestroyEye)
            return;
        countEye--;
        StartCoroutine(EyeDestroyCooldown());
        if (countEye < 1)
        {
            StartCoroutine(CanAttackState());
        }
    }
    IEnumerator EyeDestroyCooldown()
    {
        canDestroyEye = true;
        yield return new WaitForSeconds(0.1f);
        canDestroyEye = false;
    }
    IEnumerator CanAttackState()
    {   
        SoundManager.Instance.PlayOS(sturnSFX);
        Shield = false;
        tree.enabled = false;
        anim.Play(stunAnimName);
        yield return new WaitForSeconds(5f);
        tree.enabled = true;
    }
    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
