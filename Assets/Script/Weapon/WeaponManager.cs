using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{   
    public Animator animator;
    [SerializeField] WeaponBase[] weaponScriptList;
    [SerializeField] WeaponBase currentWeaponScript;
    [SerializeField] List<GameObject> weaponList = new List<GameObject>();
    [SerializeField] Animator[] animList;
    GameObject currentWeapon;
    WeaponManager we;


    private void Start()
    {
        we = this;
        foreach (Transform child in transform)
        {
            weaponList.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
        /*currentWeaponScript = weaponScriptList[0];  
        currentWeapon = weaponList[0];*/
        Observer.Instance.AddToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, OnWeaponChange);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<ItemScriptable>(ObserverCostant.INVENTORY_SET_WEAPON, OnWeaponChange);
    }
    public void Action()
    {
        if (currentWeapon == null)
        {
            OnWeaponChange(GameControler.Instance.runTimeData.stats.weapon);
        }
        currentWeaponScript.PlayAnimation();
    }
    public void PlayAnimation(string name)
    {   
        this.currentWeapon.SetActive(true);
        this.animator.Play(name, 0, 0f);
    }
    void OnWeaponChange(ItemScriptable weapon)
    {
        if (weapon == null)
            return;
        if(we == this) { 
        int weaponIndex = Array.FindIndex(weaponScriptList, w => w.name == weapon.ItemName);
        if (weaponIndex >= 0)
        {
            this.currentWeaponScript = weaponScriptList[weaponIndex];
            this.currentWeapon = weaponList[weaponIndex];
            this.animator = animList[weaponIndex];
            }
        }
    }
}
