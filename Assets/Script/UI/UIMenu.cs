using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup menuCG, optionCG;

    [SerializeField]
    Button resumeBttn, optionBttn, quitBttn, quitOptionBttn;

    [SerializeField]
    AudioClip soundUiSFX;

    
    
    private void Start()
    {
        InitializeMenu();
    }
    void InitializeMenu()
    {
        resumeBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(soundUiSFX);
            Hide();
        });
        optionBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(soundUiSFX);
            OptionShow();
        });
        quitOptionBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(soundUiSFX);
            OptionHide();
        });
        quitBttn.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlayOS(soundUiSFX);
            GameStateManager.Instance.ChangeState(GameState.normal);
            SceneControler.Instance.LoadMainMenu();
        });
        optionCG.gameObject.SetActive(false);
        menuCG.gameObject.SetActive(false);
    }
    public void Show()
    {   
        SoundManager.Instance.PlayOS();
        UiHelper.Toogle(true);
        menuCG.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(menuCG.DOFade(1, 0.5f).SetUpdate(true));
        sequence.AppendCallback(() => GameStateManager.Instance.ChangeState(GameState.pause));
    }
    void Hide()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(menuCG.DOFade(0, 0.5f));
        sequence.AppendCallback(() =>
        {
            menuCG.gameObject.SetActive(false);
            UiHelper.Toogle(false);
            GameStateManager.Instance.ChangeState(GameState.normal);
        });
        sequence.SetUpdate(true);
    }
    void OptionShow()
    {
        optionCG.gameObject.SetActive(true);
        optionCG.DOFade(1, 0.5f).SetUpdate(true);    }
    void OptionHide()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(optionCG.DOFade(0, 0.5f));
        sequence.AppendCallback(() => optionCG.gameObject.SetActive(false));
        sequence.SetUpdate(true);
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
