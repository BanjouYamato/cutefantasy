using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPlantInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Image image, iconImage;
    [SerializeField]
    Sprite disableSprite, pressedSprite;

    public static event Action<bool> onPlantTouch;

    private void Start()
    {
        Observer.Instance.AddToList<Sprite>(ObserverCostant.UI_PLANT_BUTTON, SetImage);
        Observer.Instance.AddToList(ObserverCostant.UI_PLANT_BUTTON, () => iconImage.enabled = false);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<Sprite>(ObserverCostant.UI_PLANT_BUTTON, SetImage);
        Observer.Instance.RemoveToList(ObserverCostant.UI_PLANT_BUTTON, () => iconImage.enabled = false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {   
        image.sprite = pressedSprite;
        onPlantTouch.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = disableSprite;
        onPlantTouch.Invoke(false);
    }

    void SetImage(Sprite sprite)
    {
        if (iconImage.enabled == false)
            iconImage.enabled = true;
        iconImage.sprite = sprite;
    }
}
