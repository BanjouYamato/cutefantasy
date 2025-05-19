using UnityEngine;

[CreateAssetMenu(fileName = "CharacterHealthModifier", menuName = "CharacterModifier/CharacterHealthModifier")]
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
