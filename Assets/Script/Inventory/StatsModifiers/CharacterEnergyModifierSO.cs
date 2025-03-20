using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterEnergyModifierSO : CharacterStatModifier
{
    public override void AffectCharacter(GameObject character, int val)
    {
        PlayerControler controler = FindObjectOfType<PlayerControler>();
        if (controler != null)
        {
            controler.PlayerStats.StaminaRecovery(val);
        }
    }
}
