using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public bool Death {  get; private set; }
    int currentHP;
    EnemyControler controler;
    [SerializeField]
    AudioClip hitSfx;
    private void Start()
    {
        controler = GetComponent<EnemyControler>();
        currentHP = controler.EnemyInfo.hp;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, controler.EnemyInfo.hp);
        controler.knockBack.GetKnockBack(PlayerControler.instance.transform);
    }
    public void DetecDeath()
    {
        if (currentHP <= 0)
        {   
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
