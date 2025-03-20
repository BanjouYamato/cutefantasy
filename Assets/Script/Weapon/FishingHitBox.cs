using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            Observer.Instance.Notify(ObserverCostant.OBSERVER_FISHING);
        }
    }

}
