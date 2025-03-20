using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDashInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Image image;
    [SerializeField]
    Sprite disableSprite, pressedSprite;

    public static event Action<bool> onDashTouch;

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = pressedSprite;
        onDashTouch.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = disableSprite;
        onDashTouch.Invoke(false);
    }
}
