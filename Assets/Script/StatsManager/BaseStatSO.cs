using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BaseStatSO : ScriptableObject
{
    public int HP, MaxHP;
    public float speed;

    public void TakeDamage(int val)
    {
        HP -= val;
        HP = Mathf.Clamp(HP, 0, MaxHP);
    }
}
