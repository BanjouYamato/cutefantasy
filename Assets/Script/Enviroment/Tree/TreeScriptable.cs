using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Tree", menuName = "New Tree")]
public class TreeScriptable : ScriptableObject
{
    public Sprite YoungStump;
    public Sprite MatureStump;
    public Sprite YoungFallenTree;
    public Sprite MatureFallenTree;
    public Sprite YoungTree;
    public Sprite MatureTree;
    public GameObject FallenLeaves;
    public int YoungHp;
    public int MatureHp;
    public int YoungStumpHP;
    public int MatureStumpHP;
}
