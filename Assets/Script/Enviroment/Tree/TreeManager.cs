using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeManager : MonoBehaviour
{   
    public static TreeManager Instance;
    GameControler controler;
    [SerializeField] 
    GameObject[] treeLists;

    [SerializeField] 
    List<GameObject> treesInArea = new List<GameObject>();

    [SerializeField]
    BoxCollider2D treeRange;

    [SerializeField]
    Tilemap groundTilemap;

    [SerializeField]
    string grassTileName;

    [SerializeField]
    LayerMask obstacleLayer, treeLayer;

    int initialTreeCount = 10;
    int maxTree = 30;
    float respawnTime = 10;
    float respawnTimer;
    string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, GameConstant.FOREST_DATA_JSON);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {   
        controler = GameControler.Instance;
        InitializeTrees();
    }
    private void Update()
    {
        respawnTimer += Time.deltaTime;
        if (respawnTimer >= respawnTime)
        {
            SpawnTree();
            respawnTimer = 0;
        }
    }
    void InitializeTrees()
    {
        if (GameControler.Instance.IsFirstTimeLoad(
            SceneControler.Instance.currentMapScene)
            && GameControler.Instance.IsNewGame)
        {
            for (int i = 0; i < initialTreeCount; i++)
            {
                GameObject tree = Instantiate(treeLists[Random.Range(0, treeLists.Length)], GetRandomPos(), Quaternion.identity);
                TreeGrowth treeScript = tree.GetComponent<TreeGrowth>();
                treeScript.GetStartGame();
                treeScript.UpdateTreeAppearance();
                tree.transform.SetParent(transform);
                treesInArea.Add(tree);
            }
            return;

        }
    }
    GameObject GetTree()
    {
        foreach (var tree in treesInArea)
        {
            if (!tree.activeInHierarchy)
            {
                tree.SetActive(true);
                tree.transform.position = GetRandomPos();
                tree.GetComponent<TreeGrowth>().ResetTreeState();
                return tree;
            }
        }
        GameObject newTree = Instantiate(treeLists[Random.Range(0, treeLists.Length)]);
        newTree.transform.position = GetRandomPos();
        newTree.GetComponent<TreeGrowth>().ResetTreeState();
        newTree.transform.SetParent(transform);
        treesInArea.Add(newTree);
        return newTree;
    }
    public void SpawnTree()
    {   
        if (treesInArea.Count >= maxTree) return;
        GetTree();
    }
    Vector3 GetRandomPos()
    {   
        Vector3 pos = Vector3.zero;
        int maxAttempts = 100;
        for (int i = 0; i < maxAttempts; i++)
        {
            pos = new Vector3(
                Random.Range(treeRange.bounds.min.x, treeRange.bounds.max.x),
                Random.Range(treeRange.bounds.min.y, treeRange.bounds.max.y),
                0);
            if (!IsOnGrass(pos)) 
                continue;
            if (HasObstacleNearby(pos, obstacleLayer) ||
                HasObstacleNearby(pos, treeLayer)) 
                continue;
            return pos;
        } 
        return pos;
    }
    bool IsOnGrass(Vector3 pos)
    {
        Vector3Int tilePosition = groundTilemap.WorldToCell(pos);
        TileBase tile = groundTilemap.GetTile(tilePosition);

        if (tile != null && tile.name == grassTileName)
            return true;

        return false;
    }
    bool HasObstacleNearby(Vector3 position, LayerMask layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 1f, layerMask);
        return colliders.Length > 0;
    }
    
}
