using UnityEngine;

[CreateAssetMenu(fileName = "PlantableItem", menuName = "Inventory/Item/PlantableItem")]
public class PlantableItemSO : ItemScriptable, IDestroyableItem, IItemAction
{
    public string ActionName => "Seeds";

    [field: SerializeField]
    public AudioClip actionSFX { get; private set; }

    public ItemScriptable PickUpItem;
    public GameObject seedSprite;
    public Sprite[] growStages;
    public float GrowthTime;
    public float DehydrateTime;
    public EdibleItemSO pickUpItem;

    public bool PerFormAction(GameObject character)
    {
        EffectAction effectAction = character.GetComponent<EffectAction>();
        if (effectAction != null)
        {
            effectAction.SetSeed(this);
            return true;
        }
        return false;
    }
}
