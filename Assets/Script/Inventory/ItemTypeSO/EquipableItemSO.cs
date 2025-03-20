using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItemSO : ItemScriptable, IDestroyableItem, IItemAction
{
    public string ActionName => "Equip";

    [field: SerializeField]
    public AudioClip actionSFX { get; private set; }

    public int maxComboStep;

    public int indexLayer;

    public bool PerFormAction(GameObject character)
    {
        AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
        if (weaponSystem != null)
        {
            weaponSystem.SetWeapon(this);
            return true;
        }
        return false;
    }
}
