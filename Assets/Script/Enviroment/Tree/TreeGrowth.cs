using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeData
{
    //public GameObject treeObject;
    public float x, y, GrowthTimer, stumpTimer;
    public TreeState state;
    public int hp;
    public bool wasMatureBeforeCut;

    public TreeData(TreeGrowth tree)
    {   
        //treeObject = tree.gameObject;
        x = tree.transform.position.x;
        y = tree.transform.position.y;
        GrowthTimer = tree.GrowthTimer;
        stumpTimer = tree.stumpTimer;
        state = tree.curState;
        hp = tree.curHP;
        wasMatureBeforeCut = tree.wasMatureBeforeCut;
    }
}
public enum TreeState { Young, Mature, Stump }
public class TreeGrowth : MonoBehaviour
{
    public TreeState curState { get; private set; }
    [SerializeField] 
    GameObject woodPrefab;
    GameObject curFallerTree;
    [SerializeField] 
    TreeScriptable treeScriptable;
    [SerializeField]
    AudioClip chopSFX;

    public int curHP { get; private set; }
    float GrowthTime = 10f;
    public float GrowthTimer { get; private set; }
    float stumpTime = 20f;
    public float stumpTimer { get; private set; }
    SpriteRenderer spriteRenderer;
    public bool wasMatureBeforeCut { get; private set; }
    Sequence sequence;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        curFallerTree = transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        curFallerTree.SetActive(false);
        curState = TreeState.Mature;
        UpdateTreeAppearance();
    }
    private void Update()
    {
        if (curState == TreeState.Young)
        {
            GrowthTimer += Time.deltaTime;
            if (GrowthTimer >= GrowthTime)
            {
                GrowToMature();
            }
        }
        if (curState == TreeState.Stump)
        {
            stumpTimer += Time.deltaTime;
            if (stumpTimer >= stumpTime)
            {
                StumpHealing();
                stumpTimer = 0;
                GrowthTimer = 0;
            }
        }
    }
    public void ChopTree(int damage)
    {
        curHP -= damage;
        SoundManager.Instance.PlayOS(chopSFX);
        if (curState == TreeState.Mature || curState == TreeState.Young)
        {
            EffectManager.Instance.GetEffect(treeScriptable.FallenLeaves, transform.GetChild(1).position);
        }
        if (curHP <= 0 )
        {   
            
            if (curState == TreeState.Stump)
            {   
                int woodCount = wasMatureBeforeCut ? 3 : 1;
                DropWood(woodCount, transform);
                stumpTimer = 0;
                gameObject.SetActive(false );
            }
            else
            {
                if (curState == TreeState.Mature)
                {
                    TreeFall(treeScriptable.MatureFallenTree);
                    curState = TreeState.Stump;
                    UpdateTreeAppearance();
                }
                else if (curState == TreeState.Young)
                {
                    TreeFall(treeScriptable.YoungFallenTree);
                    curState = TreeState.Stump;
                    UpdateTreeAppearance();
                }
            }
        } 
    }
    void StumpHealing()
    {
        if (!wasMatureBeforeCut)
        {
            GrowthTimer = 0;
            curState = TreeState.Young;
            UpdateTreeAppearance();
        }
        else
        {
            GrowthTimer = 0;
            curState = TreeState.Mature;
            UpdateTreeAppearance();
        }
    }
    void TreeFall(Sprite sprite)
    {   
        SpriteRenderer spriteRenderer = curFallerTree.transform.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        int faceDIr = PlayerControler.instance.PlayerMovement.GetRightFace() ? 1 : -1;
        Vector3 fallMoveDir = new Vector3(1f, 0, 0);
        curFallerTree.SetActive(true);
        sequence = DOTween.Sequence();
        sequence.Append(curFallerTree.transform.DOMove(curFallerTree.transform.position + fallMoveDir * faceDIr, 0.5f)
            .SetEase(Ease.OutCubic));
        sequence.Append(curFallerTree.transform.DORotate(Vector3.forward * -90 * faceDIr, 1f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(FallTreeFinish));
    }
    void CutTree()
    {
        int dropCount = Random.Range(1, 4);
        DropWood(dropCount, transform);
    }
    void DropWood(int dropCount, Transform transform)
    {
        for (int i = 0; i < dropCount; i++)
        {
            Vector2 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            pos.y += Random.Range(-1f, 1f);
            GameObject wood = Instantiate(woodPrefab);
            wood.transform.position = pos;
            
        }
    }
    void FallTreeFinish()
    {
        int dropCount = Random.Range(3, 9);
        DropWood(dropCount, curFallerTree.transform);
        curFallerTree.SetActive(false);
        curFallerTree.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        sequence.Kill();
    }
    public void ResetTreeState()
    {
        curState = TreeState.Young;
        GrowthTimer = 0f;
        UpdateTreeAppearance();
    }
    void GrowToMature()
    {
        curState = TreeState.Mature;
        GrowthTimer = 0;
        UpdateTreeAppearance();
    }
    public void UpdateTreeAppearance()
    {
        switch (curState)
        {
            case TreeState.Young:
                spriteRenderer.sprite = treeScriptable.YoungTree;
                curHP = treeScriptable.YoungHp;
                wasMatureBeforeCut = false;
                break;
            case TreeState.Mature:
                spriteRenderer.sprite = treeScriptable.MatureTree;
                curHP = treeScriptable.MatureHp;
                wasMatureBeforeCut = true;
                break;
            case TreeState.Stump:
                if (!wasMatureBeforeCut)
                {
                    spriteRenderer.sprite = treeScriptable.YoungStump;
                    curHP = treeScriptable.YoungStumpHP;
                }
                else
                {
                    spriteRenderer.sprite = treeScriptable.MatureStump;
                    curHP = treeScriptable.MatureStumpHP;
                }
                break;
        }
    }
    public void GetStartGame()
    {
        curState = TreeState.Mature;
    }
}
