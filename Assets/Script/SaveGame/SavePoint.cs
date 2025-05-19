using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerControler player))
        {   
            UITalkButton.isFromSavePoint = true;
            Observer.Instance.Notify<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.TryGetComponent(out PlayerControler player))
        {
            UITalkButton.isFromSavePoint = false;
            Observer.Instance.Notify<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, false);
        }
    }
}
