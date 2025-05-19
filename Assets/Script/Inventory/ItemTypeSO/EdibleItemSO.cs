using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EdibleItem", menuName = "Inventory/Item/EdibleItem")]
public class EdibleItemSO : ItemScriptable, IDestroyableItem, IItemAction
{
    [SerializeField]
    List<ModifierData> modifierData = new List<ModifierData>();
    public string ActionName => "Consume";

    [field: SerializeField]
    public AudioClip actionSFX {get; private set;}

    public bool PerFormAction(GameObject character)
    {
        foreach (ModifierData data in modifierData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }
}
public interface IDestroyableItem
{

}
public interface IItemAction
{
    public string ActionName { get; }
    public AudioClip actionSFX { get; }
    bool PerFormAction(GameObject character);
}
[Serializable]
public class ModifierData
{
    public CharacterStatModifier statModifier;
    public int value;
}
