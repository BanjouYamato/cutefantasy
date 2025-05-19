using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITalkButton : MonoBehaviour, IPointerDownHandler
{   
    public static bool isShow { get; private set; }
    public static bool isFromSavePoint;
    [SerializeField]
    Image image;
    [SerializeField]
    Sprite disableSprite, pressedSprite;
    private void Start()
    {
        gameObject.SetActive(false);
        Observer.Instance.AddToList<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, Toggle);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<bool>(ObserverCostant.UI_TALK_NPC_BUTTON, Toggle);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(ResetInput());
        Observer.Instance.Notify(ObserverCostant.UI_TALK_NPC_BUTTON);
    }
    IEnumerator ResetInput()
    {
        image.sprite = pressedSprite;
        yield return new WaitForSeconds(0.1f);
        image.sprite = disableSprite;
    }
    void Toggle(bool val)
    {
        if (isShow == val) 
            return;
        isShow = val;
        gameObject.SetActive(val);
    }
}
