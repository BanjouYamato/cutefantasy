using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    RectTransform logoGame;
    [SerializeField]
    Button startBttn, loadBttn, ptsBttn, backMenuBttn;
    [SerializeField]
    CanvasGroup ptsTxt;
    [SerializeField]
    AudioClip clickSFX, menuSFX;
    [SerializeField]
    CanvasGroup ptsCanvasGr, menuPanel;

    private void Start()
    {
        startBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(clickSFX);
            SceneControler.Instance.StartGame(GameConstant.NEW_GAME);
            GameControler.Instance.IsNewGame = true;
            GameControler.Instance.IsChangScene = false;
        });

        loadBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(clickSFX);
            SceneControler.Instance.StartGame(GameConstant.LOAD_GAME);
            GameControler.Instance.IsNewGame = false;
            GameControler.Instance.IsChangScene = false;
        });
        backMenuBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(menuSFX);
            MenuOff();
        });
        AnimUI();
    }
    
    void AnimUI()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => 
        {
            MenuToggle(false);
        });
        sequence.Append(logoGame.DOAnchorPosY(logoGame.anchoredPosition.y - 50, 1f)
            .SetLoops(10000, LoopType.Yoyo)
            .SetEase(Ease.InOutSine));
        sequence.Join(DOVirtual.DelayedCall(1f, () =>
        {
            ptsCanvasGr.DOFade(1, 0.5f)
            .SetLoops(10000, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        }));
        sequence.JoinCallback(() =>
        {
            ptsBttn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayOS(menuSFX);
                MenuOn();

            });
        });
    }
    void MenuOn()
    {
        MenuToggle(true);
        menuPanel.DOFade(1, 0.5f);
    }
    void MenuOff()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(menuPanel.DOFade(0, 0.5f));
        sequence.AppendCallback(() => MenuToggle(false));
        
    }
    void MenuToggle(bool val)
    {
        menuPanel.gameObject.SetActive(val);
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
