using UnityEngine;

[CreateAssetMenu(fileName = "CharacterNPC", menuName = "UI/Dialogue/NPC")]
public class CharacterSO : ScriptableObject
{
    public string charName;
    public Sprite icon;

    [TextArea]
    public string description;
}
