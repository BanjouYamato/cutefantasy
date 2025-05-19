using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartnerStats", menuName = "BaseStats/PartnerStats")]
public class PartnerStats : BaseStatSO
{
    public string partnerName;
    public GameObject prefab;
    public int price, attack;
    public Sprite avatar;
    public float range;
    [TextArea]
    public string description;
}
