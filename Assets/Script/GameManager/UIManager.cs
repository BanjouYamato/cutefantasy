using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    CanvasGroup fishingGame;

    [SerializeField] 
    GameObject fishingGamePanel;

    [SerializeField]
    UIShop UIShop;

    Observer observer;
    private void Start()
    {   
        
        observer = Observer.Instance;
        observer.AddToList("Hit Fish", FishingGameIn);
        observer.AddToList("EndFishing", FishingGameOut);
    }
    private void OnDestroy()
    {   
        transform.DOKill();
        observer.RemoveToList("Hit Fish", FishingGameIn);
        observer.RemoveToList("EndFishing", FishingGameOut);
    }
    void ResetUI()
    {

    }
    void FishingGameOut()
    {
        fishingGame.DOFade(0, 1f)
            .OnComplete(() =>
            {
                fishingGamePanel.SetActive(false);
                Time.timeScale = 1;

            })
            .SetUpdate(true);
    }
    void FishingGameIn()
    {
        fishingGamePanel.SetActive(true);
        fishingGame.DOFade(1, 1f);
    }

}
