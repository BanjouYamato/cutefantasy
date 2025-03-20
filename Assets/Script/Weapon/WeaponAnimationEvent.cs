using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponAnimationEvent : MonoBehaviour
{
    public void EnableWeapon()
    {
        gameObject.SetActive(true);
    }
    public void DisableWeapon()
    {
        gameObject.SetActive(false);
    }
}
