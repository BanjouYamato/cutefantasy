using UnityEngine;

[CreateAssetMenu(fileName = "CharacterEnergyModifier", menuName = "CharacterModifier/CharacterEnergyModifier")]
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
