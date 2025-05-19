using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CreateShield : EnemyActions
{
    [SerializeField]
    GameObject eyePrefab;
    [SerializeField]
    Transform skillPos;
    Vector2[] directions = new Vector2[4]
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };
    public override TaskStatus OnUpdate()
    {
        for (int i = 0; i < directions.Length; i++)
        {
            GameObject eyeClone = GameObject.Instantiate(eyePrefab, 
                skillPos.position, 
                Quaternion.identity);
            eyeClone.transform.SetParent(skillPos);
            eyeClone.GetComponent<EyeControl>().EyeAppear(directions[i], skillPos);
        }
        return TaskStatus.Success;
    }
}
