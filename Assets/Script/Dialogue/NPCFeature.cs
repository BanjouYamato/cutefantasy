using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class NPCFeature : MonoBehaviour
{
    public void ActivateFeature(string featureName)
    {
        switch (featureName)
        {
            case GameConstant.NPC_FEATURES_SHOP:
                Observer.Instance.Notify(GameConstant.NPC_FEATURES_SHOP);
                break;
            default:
                break;
        }
    }
}
