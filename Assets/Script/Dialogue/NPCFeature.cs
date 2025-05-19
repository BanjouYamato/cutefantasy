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
            case GameConstant.NPC_FEATURES_SHOP_PARTNER:
                Observer.Instance.Notify<bool>(GameConstant.NPC_FEATURES_SHOP_PARTNER, true);
                break;
            default:
                break;
        }
    }
}
