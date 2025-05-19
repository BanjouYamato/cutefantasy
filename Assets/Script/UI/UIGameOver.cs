using UnityEngine;
using DG.Tweening;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    CanvasGroup panelCanvas, txtCanvas;

    private void Start()
    {   
        GameStateManager.onGameOver += GameOverPerform;
        panelCanvas.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameStateManager.onGameOver -= GameOverPerform;
        DOTween.KillAll();
    }

    void GameOverPerform()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback( () => panelCanvas.gameObject.SetActive(true));
        sequence.Append(panelCanvas.DOFade(1, 0.5f));
        sequence.Append(txtCanvas.DOFade(1, 0.5f)
            .SetLoops(100, LoopType.Yoyo)
            .SetEase(Ease.InOutSine));
    }
}
