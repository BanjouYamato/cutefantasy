using UnityEngine;

[CreateAssetMenu(fileName = "ResourcableItem", menuName = "Inventory/Item/ResourcableItem")]
public class ResourcableItemSO : ItemScriptable, IDestroyableItem
{
    [Range(0f, 1f)]   
    
    public float DropRate;
}   
