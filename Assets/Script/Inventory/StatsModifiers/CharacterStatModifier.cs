using UnityEngine;

public abstract class CharacterStatModifier : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, int val);
}
