using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootEvent : MonoBehaviour
{
    [SerializeField]
    GameObject arrowPrefab;
    [SerializeField]
    Transform spawnPos;
    [SerializeField]
    EnemyControler controler;

    public void SpawnArrow()
    {
        GameObject arrow= Instantiate(arrowPrefab, spawnPos.position, ArrowDir());
        arrow.transform.SetParent(transform);
    }
    Quaternion ArrowDir()
    {   
        if (controler.behavior.attackDir == Direction.up) return Quaternion.Euler(0, 0, 180f);
        else if (controler.behavior.attackDir == Direction.down) return Quaternion.Euler(0, 0, 0);
        else
        {
            if (!controler.pathFinding.leftFace) return Quaternion.Euler(0, 0, 90);
            else return Quaternion.Euler(0, 0, -90);
        }
    }
}
