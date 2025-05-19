using System.Collections;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public bool Death {  get; private set; }
    int currentHP;
    int maxHp;
    EnemyControler controler;
    [SerializeField]
    AudioClip hitSfx;
    [SerializeField] EnemyHPBar _enemyHp;
    private void Start()
    {
        controler = GetComponent<EnemyControler>();
        maxHp = controler.EnemyInfo.hp;
        currentHP = maxHp;
        _enemyHp.UpdateHealthBar((float)currentHP / (float)maxHp);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, controler.EnemyInfo.hp);
        _enemyHp.UpdateHealthBar((float)currentHP/(float)maxHp);
        controler.knockBack.GetKnockBack(PlayerControler.instance.transform);
        
    }
    public void DetecDeath()
    {
        if (currentHP <= 0)
        {
            controler._enemyManager.RemoveEnemyToList(this);
            controler.behavior.StopAllCoroutines();
            Death = true;
            controler.pathFinding.agent.isStopped = true;
            controler.capsuleCollider.enabled = false;
            StartCoroutine(DeathRoutine());
        }
    }
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
