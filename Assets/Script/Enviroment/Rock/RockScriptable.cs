using UnityEngine;

[CreateAssetMenu (fileName = "Rock", menuName = "Resource/Rock")]
public class RockScriptable : ScriptableObject
{
    public int MaxHP;
    public int DropCount;
    public GameObject DropResourcePrefab;
}
