using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterHealthModifierSO : CharacterStatModifier
{
    public override void AffectCharacter(GameObject character, int val)
    {
        PlayerControler controler = FindObjectOfType<PlayerControler>();
        if (controler != null)
        {
            controler.PlayerStats.Healing(val);
        }    
    }
}
