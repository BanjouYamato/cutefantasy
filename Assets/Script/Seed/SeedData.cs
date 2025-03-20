using UnityEngine;

[CreateAssetMenu (fileName = "NewSeed", menuName = "Farming/Seed")]
public class SeedData : ScriptableObject
{
    public string SeedName;
    public ItemScriptable PickUpItem;
    public GameObject seedSprite;
    public Sprite[] growStages = new Sprite[5];
    public float GrowthTime;
    public float DehydrateTime;
}
