using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public CharacterSO character;
    [TextArea]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
    public Dialogue dialogue;
    public string featureName;
}
