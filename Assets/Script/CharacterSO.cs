using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    public string charName;
    public Sprite icon;

    [TextArea]
    public string description;
}
