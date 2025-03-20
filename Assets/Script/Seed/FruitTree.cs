using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class FruitTreeData
{
    public Vector2 pos;
    public PlantableItemSO seedData;
    public int curStage;
    public Sprite sprite;
    public bool isWatered;
    public void SetData(FruitTree data)
    {   
        this.pos = data.transform.position;
        this.seedData = data.seedData;
        this.curStage = data.curStage;
        this.sprite = data.spriteRenderer.sprite;
        this.isWatered = data.isWatered;
    }
}
public class FruitTree : MonoBehaviour
{
    public PlantableItemSO seedData { get; private set; }
    public int curStage { get; private set; }
    float timer;
    float dehydrateTimer;
    public SpriteRenderer spriteRenderer {  get; private set; }
    public bool isWatered {  get; private set; }
    private void Update()
    {
        UpdateStage();
    }
    void UpdateStage()
    {
        if (seedData == null || curStage >= seedData.growStages.Length - 1) 
            return;
        timer += Time.deltaTime;
        if (isWatered)
        {
            if (timer >= seedData.GrowthTime)
            {
                Grow();
                timer = 0;
            }
        }
        else
        {
            if (timer >= seedData.DehydrateTime)
            {
                Destroy(gameObject);
                timer = 0;
            }
        }
    }
    public void Initialize(PlantableItemSO seed)
    {
        seedData = seed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (seedData.growStages.Length > 0 )
        {
            curStage = 0;
            spriteRenderer.sprite = seedData.growStages[curStage];
            isWatered = false;
            UpdateWarning();
        }
        FarmingSystem.instance.AddFruitTree(this);
    }
    void Grow()
    {   
        isWatered = false;
        curStage++;
        if (curStage <  seedData.growStages.Length)
        {
            spriteRenderer.sprite = seedData.growStages[curStage];
        }
        if (curStage < seedData.growStages.Length - 1)
            UpdateWarning();
        FarmingSystem.instance.UpdateFruitTreeData(this);
    }
    public bool IsFUllyGrow()
    {
        return curStage == seedData.growStages.Length - 1;
    }
    public void HarvestFruit()
    {
        FarmingSystem.instance.AvaiableGround(transform.position);
        GameControler.Instance.runTimeData.inventoryData.AddItem(seedData.pickUpItem, 1);
        FarmingSystem.instance.RemoveFruitTree(this);
        Destroy(gameObject);
        /*if (inventory != null)
        {
            
            inventory.AddItem(item, qty);
            Destroy(gameObject);
        }*/
    }
    public void Watering()
    {
        isWatered = true;
        timer = 0;
        UpdateWarning();
        FarmingSystem.instance.UpdateFruitTreeData(this);
    }
    void UpdateWarning()
    {
        switch(isWatered)
        {
            case true:
                transform.GetChild(0).gameObject.SetActive(false);
                break;
            case false:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }
    public void SetData(FruitTreeData data)
    {
        this.seedData = data.seedData;
        this.curStage = data.curStage;
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.sprite = data.sprite;
        this.isWatered = data.isWatered;
        FarmingSystem.instance.UpdateFruitTreeData(this);
    }
}
