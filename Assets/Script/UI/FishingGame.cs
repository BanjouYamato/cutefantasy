using UnityEngine;
using DG.Tweening;

public class FishingGame : MonoBehaviour
{
    [Header("Fishing Bar")]

    [SerializeField] 
    RectTransform fishingBar;

    [SerializeField] 
    float fisingBarSpeed;

    [Header("Fish")]

    [SerializeField] 
    RectTransform fish;

    [SerializeField] 
    float fishMoveDuration;

    [SerializeField] 
    ItemScriptable fishScript;

    Tweener tweener;

    [Header("Progress")]

    [SerializeField] 
    RectTransform progressBar;

    [SerializeField] 
    RectTransform progressBarMax;

    bool hitFish;

    [SerializeField] 
    RectTransform fishingBarMax;

    bool isTouching;

    Observer observer;
    private void OnEnable()
    {
        progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);
        StartFishMovement();
    }
    private void OnDisable()
    {
        DOTween.KillAll();
        fish.anchoredPosition = Vector3.zero;
        fishingBar.anchoredPosition = Vector3.zero;
    }
    private void Start()
    {
        observer = Observer.Instance;
        observer.AddToList(ObserverCostant.OBSERVER_WONFISH, PlayerWonFish);
    }
    private void OnDestroy()
    {
        observer.RemoveToList(ObserverCostant.OBSERVER_WONFISH, PlayerWonFish);
    }
    private void Update()
    {
        FishingBarMovement();
        CheckCollisionProgress();
    }
    void StartFishMovement()
    {
        float panelWidth = fishingBarMax.rect.width;
        float fishWidth = fish.rect.width;
        tweener = fish.DOAnchorPosX(panelWidth - fishWidth, fishMoveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetUpdate(true);
    }
    void CheckCollisionProgress()
    {
        float fishingBarPos = fishingBar.anchoredPosition.x + fishingBar.rect.width;
        float fishPos = fish.anchoredPosition.x + fish.rect.width / 2;
        if (fishPos <= fishingBarPos && fishPos >= fishingBar.anchoredPosition.x)
        {
            progressBar.sizeDelta += new Vector2(200f * Time.deltaTime, 0);
        }
        else
        {
            progressBar.sizeDelta += new Vector2(-200f * Time.deltaTime, 0);
        }
        if (progressBar.rect.width >= progressBarMax.rect.width && !hitFish)
        {   
            hitFish = true;
            GameControler.Instance.runTimeData.inventoryData.AddItem(fishScript, 1);
            observer.Notify(ObserverCostant.OBSERVER_ENDFISHING);
        }
        else if (progressBar.rect.width < 0)
        {
            observer.Notify(ObserverCostant.OBSERVER_ENDFISHING);
        }
    }
    void PlayerWonFish()
    {
        hitFish = false;
    }

    void FishingBarMovement()
    {
        float moveAmount = (isTouching ? 1 : -1) * Time.unscaledDeltaTime * fisingBarSpeed;
        Vector2 newPos = fishingBar.anchoredPosition + new Vector2(moveAmount, 0);
        float parentWidth = fishingBarMax.rect.width;
        float childWidth = fishingBar.rect.width;
        newPos.x = Mathf.Clamp(newPos.x, 0, parentWidth - childWidth);
        fishingBar.anchoredPosition = newPos;
    }
    public void OnTouch(bool val)
    {
        isTouching = val;
    }
}
