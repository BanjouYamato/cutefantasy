using UnityEngine;

public abstract class ItemScriptable : ScriptableObject
{
    [field: SerializeField]
    public string ItemName { get; set; }
    [field: SerializeField]
    public Sprite Icon { get; set; }
    [field: SerializeField]
    public bool IsStackable { get; set; }   
    public int ID => GetInstanceID();
    [field: SerializeField]
    public int MaxStack { get; set; } = 1;
    [field: SerializeField]
    public bool CanConsume;
    [field: SerializeField]
    public string Description;
    [field: SerializeField]
    public int Price { get; set; }
}

