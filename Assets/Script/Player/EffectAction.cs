using UnityEngine;

public class EffectAction : MonoBehaviour
{
    public LayerMask TreeLayer;
    public LayerMask rockLayer;
    public LayerMask npcLayer;
    PlayerControler controler;
    FarmingSystem farmingSystem;
    Collider2D NPC;
    [SerializeField]
    AudioClip waterSFX;

    private void Start()
    {   
        farmingSystem = FindObjectOfType<FarmingSystem>();
        controler = GetComponent<PlayerControler>();
        Observer.Instance.AddToList(ObserverCostant.UI_TALK_NPC_BUTTON, TalkToNPC);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList(ObserverCostant.UI_TALK_NPC_BUTTON, TalkToNPC);
    }
    private void Update()
    {
        CheckNPC();
        PlantTree();
    }

    private void PlantTree()
    {
        if (controler.Player.CanNotAction())
            return;
        if (InputManager.Instance.isPlantPressed)
            PlayerPlantTree();
    }

    public Collider2D ActionZone(Transform pos, float sizeZone, LayerMask layer)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(pos.position, sizeZone, layer);
        foreach (Collider2D obj in objects)
        {
            Vector2 dirToObj = (obj.transform.position - pos.position).normalized;
            if (Vector2.Dot(dirToObj, controler.PlayerMovement.VectorDirPlayer()) > 0.5f)
                return obj;
        }
        return null;
    }
    public void ChopTree(int val)
    {
        Collider2D tree = ActionZone(transform, 1f, TreeLayer);
        if (tree != null)
            tree.GetComponent<TreeGrowth>().ChopTree(val);
    }
    public void BreakRock(int val)
    {
        Collider2D rock = ActionZone(transform, 1f, rockLayer);
        if (rock != null)
            rock.GetComponent<Rock>().BreakRock(val);
    }
    public void SetSeed(PlantableItemSO seed)
    {
        Observer.Instance.Notify<Sprite>(ObserverCostant.UI_PLANT_BUTTON, seed.Icon);
    }
    void PlayerPlantTree()
    {
        if (GameControler.Instance.runTimeData.currentSeed == null)
            return;
        PlantableItemSO seed = (PlantableItemSO)GameControler.Instance.runTimeData.currentSeed.item;
        farmingSystem.PlantSeed(transform.position, seed);
    }
    public void PlayerWater()
    {   
        farmingSystem.WaterTile(transform.position, controler.PlayerMovement.VectorDirPlayer());
    }
    void CheckNPC()
    {
        if (UITalkButton.isFromSavePoint)
            return;
        NPC = ActionZone(transform, 1f, npcLayer);
        if (NPC != null)
        {
            if (UITalkButton.isShow)
                return;
            Observer.Instance.Notify<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, true);
        }
        else
        {
            if (!UITalkButton.isShow)
                return;
            Observer.Instance.Notify<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, false);
        }
    }
    public void TalkToNPC()
    {   
        if (NPC != null)
        {
            controler.PlayerStateMachine.ChangeState(new IdleState(controler, controler.PlayerStats.dir));
            NPC.GetComponent<DialogueTrigger>().TriggerDialogue();
            return;
        }
        Observer.Instance.Notify<bool>(ObserverCostant.SAVE_GAME, true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, 1f, npcLayer);
        foreach (Collider2D obj in objects)
        {
            Vector2 dirToObj = (obj.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(dirToObj, controler.PlayerMovement.VectorDirPlayer());

            Gizmos.color = dot > 0.5f ? Color.green : Color.gray;
            Gizmos.DrawLine(transform.position, obj.transform.position);
        }
    }
}
