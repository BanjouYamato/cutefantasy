using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTouchPanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(DialougeManager.Instance.isDialogueActive)
            DialougeManager.Instance.DisplayNextDialogueLine();

    }

}
