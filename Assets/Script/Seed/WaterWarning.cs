using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterWarning : MonoBehaviour
{
    Tween tween;
    private void OnEnable()
    {
        WarningMove();
    }
    private void OnDisable()
    {
        tween.Kill();
    }
    void WarningMove()
    {
        tween =  transform.DOBlendableMoveBy(new Vector3(0, 0.5f, 0), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
