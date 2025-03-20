using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Resource", menuName = "New Resource")]
public class RockScriptable : ScriptableObject
{
    public int MaxHP;
    public int DropCount;
    public GameObject DropResourcePrefab;
}
