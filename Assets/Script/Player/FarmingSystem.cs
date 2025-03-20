using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class DugTileData
{
    public int x, y;
    public bool hasSeed;
    public PlantableItemSO plantedSeed;
    public float growTimer;

    public DugTileData(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public class DugTileList
{
    public List<DugTileData> tiles;
    public DugTileList(List<DugTileData> tiles)
    {
        this.tiles = tiles;
    }
}
public class FarmingSystem : MonoBehaviour
{
    [SerializeField] 
    Tilemap Tilemap;

    [SerializeField] 
    RuleTile farmRule;

    [SerializeField] 
    PlayerMovement movement;

    [SerializeField]
    AudioClip gardenSFX;

    GameControler gameControler;
    public static FarmingSystem instance;
    //List<DugTileData> dataList = new List<DugTileData>();
    private void Awake()
    {
        instance = this;
        gameControler = GameControler.Instance;
    }
    private void Start()
    {   
        StartCoroutine(DelayedLoad());
    }
    IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(0.2f);
        LoadDugTileData();
        LoadFruitTree();
        Tilemap.RefreshAllTiles();
    }
    public void Gardening(Transform transform)
    {
        Vector3Int curGridPos = Tilemap.WorldToCell(transform.position);
        curGridPos.z = 0;
        Vector3Int targetGridPos = curGridPos + new Vector3Int(
            Mathf.RoundToInt(movement.VectorDirPlayer().x),
            Mathf.RoundToInt(movement.VectorDirPlayer().y), 
            0);
        if (Tilemap.HasTile(targetGridPos) && 
            !gameControler.runTimeData.dugTileList.Any(t => t.x == targetGridPos.x && t.y == targetGridPos.y))
        {
            SoundManager.Instance.PlayOS(gardenSFX);
            Tilemap.SetTile(targetGridPos, farmRule);
            gameControler.runTimeData.dugTileList.Add(new DugTileData(targetGridPos.x, targetGridPos.y));
        }
    }
    void LoadDugTileData()
    {
        List<DugTileData> loadedData = GameControler.Instance.runTimeData.dugTileList;
        if (loadedData != null)
        {   
            foreach (DugTileData tile in loadedData)
            {
                Vector3Int pos = new Vector3Int(tile.x, tile.y, 0);
                Tilemap.SetTile(pos, farmRule);
            }
        }
    }
    void LoadFruitTree()
    {
        for (int i = 0; i < GameControler.Instance.runTimeData.fruitTreeDataList.Count; i++)
        {
            GameObject treeObj = Instantiate(GameControler.Instance.runTimeData.fruitTreeDataList[i].seedData.seedSprite
                , GameControler.Instance.runTimeData.fruitTreeDataList[i].pos
                , Quaternion.identity);
            treeObj.transform.SetParent(transform);
            FruitTree tree = treeObj.GetComponent<FruitTree>();
            tree.SetData(GameControler.Instance.runTimeData.fruitTreeDataList[i]);
        }
    }
    public void PlantSeed(Vector3 targetPos, PlantableItemSO seed)
    {
        var farmTile = gameControler.runTimeData.dugTileList.FirstOrDefault(
            t => t.x == Mathf.RoundToInt(targetPos.x) 
                                                && t.y == Mathf.RoundToInt(targetPos.y));
        if (farmTile != null && !farmTile.hasSeed)
        {
            farmTile.hasSeed = true;
            farmTile.plantedSeed = seed;
            farmTile.growTimer = seed.GrowthTime;
            Vector3 cellPos = new Vector3(farmTile.x + 0.5f, farmTile.y + 0.5f, 0);
            GameObject plantedSeed = Instantiate(seed.seedSprite, cellPos, Quaternion.identity);
            FruitTree tree = plantedSeed.GetComponent<FruitTree>();
            if (tree != null )
            {
                SoundManager.Instance.PlayOS();
                tree.transform.SetParent(transform);
                tree.Initialize(seed);
                gameControler.runTimeData.UpdateCurrentSeed();
            }
        } 
    }
    public void AddFruitTree(FruitTree tree)
    {
        FruitTreeData data = new FruitTreeData();
        data.SetData(tree);
        GameControler.Instance.runTimeData.fruitTreeDataList.Add(data);
    }
    public void RemoveFruitTree(FruitTree tree)
    {
        Vector2 pos = tree.transform.position;
        var list = GameControler.Instance.runTimeData.fruitTreeDataList;

        FruitTreeData data = list.FirstOrDefault(t => t.pos == pos);
        if (data != null)
        {
            list.Remove(data);
        }
    }
    public void UpdateFruitTreeData(FruitTree tree)
    {
        Vector2 pos = tree.transform.position;
        var list = GameControler.Instance.runTimeData.fruitTreeDataList;

        FruitTreeData data = list.FirstOrDefault(t => t.pos == pos);
        if (data != null)
        {
            data.SetData(tree);
        }
    }
    public void AvaiableGround(Vector3 targetPos)
    {   
        targetPos = new Vector3 (targetPos.x - 0.5f, targetPos.y - 0.5f, 0);
        var farmTile = gameControler.runTimeData.dugTileList.FirstOrDefault(t => t.x == targetPos.x
                                                && t.y == targetPos.y);
        if (farmTile != null && farmTile.hasSeed)
        {
            farmTile.hasSeed = false;
        }
    }
    public void WaterTile(Vector3 playerPos, Vector2 dir)
    {
        Vector3 targetPos = playerPos + new Vector3(dir.x, dir.y, 0);
        Collider2D hit = Physics2D.OverlapPoint(targetPos);
        if (hit != null)
        {
            FruitTree fruitTree = hit.GetComponent<FruitTree>();
            if (fruitTree != null )
            {
                fruitTree.Watering();
            }
        }
    }
}
