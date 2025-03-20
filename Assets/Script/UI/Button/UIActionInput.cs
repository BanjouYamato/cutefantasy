using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIActionInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    Image image;
    [SerializeField]
    Sprite disableSprite, pressedSprite;
    
    public static event Action<bool> onTouch;

    public void OnPointerDown(PointerEventData eventData)
    {   
        image.sprite = pressedSprite;
        onTouch.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {   
        image.sprite = disableSprite;
        onTouch.Invoke(false);
    }

}
