using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBreakRock : MonoBehaviour
{
    public LayerMask RockLayer;
    public void BreakRock()
    {
        Collider2D[] rocks = Physics2D.OverlapCircleAll(transform.position, 1f, RockLayer);
        foreach (Collider2D rock in rocks)
        {
            Vector2 dirToRock = (rock.transform.position - transform.position).normalized;
            if (Vector2.Dot(dirToRock, PlayerControler.instance.PlayerMovement.VectorDirPlayer()) > 0.5f)
                rock.GetComponent<Rock>().BreakRock(1);
        }
    }
}
