using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyData> enemyDatas;

    public void RemoveEnemyToList(EnemyData enemy)
    {
        enemyDatas.Remove(enemy);
    }
}
