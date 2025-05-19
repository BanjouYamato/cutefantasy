using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected int damage;
    protected WeaponManager manager;
    protected float count;
    [SerializeField]
    AudioClip actionSFX;
    protected virtual void Update()
    {
        count += Time.deltaTime;
        if (count >= manager.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            gameObject.SetActive(false);
        }
    }
    public int GetDamage()
    {
        return damage;
    }
    public virtual void PlayAnimation()
    {   
        if (manager == null)
            manager = FindObjectOfType<WeaponManager>();    
        manager.PlayAnimation($"{GameControler.Instance.runTimeData.stats.weapon.ItemName}_{GameControler.Instance.runTimeData.stats.dir}_{PlayerControler.instance.PlayerAction.comboStep}");
        SoundManager.Instance.PlayOS(actionSFX);
        count = 0;
    }
}
