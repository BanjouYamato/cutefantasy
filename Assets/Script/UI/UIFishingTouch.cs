using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIFishingTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    FishingGame game;
    public void OnPointerDown(PointerEventData eventData)
    {
        game.OnTouch(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        game.OnTouch(false);
    }
}
