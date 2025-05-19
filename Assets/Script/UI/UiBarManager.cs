using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiBarManager : MonoBehaviour
{
    Observer observer;
    [Header("Healh Bar")]
    [SerializeField]
    Image healtContent;
    [Header("Mana Bar")]
    [SerializeField]
    Image manaContent;
    [Header("Energy Bar")]
    [SerializeField]
    Image energyContent;

    const float performTime = 0.5f;
    private void Start()
    {
        observer = Observer.Instance;
        observer.AddToList<float>(ObserverCostant.UPDATE_HPBAR, UpdateHealtBar);
        observer.AddToList<float>(ObserverCostant.UPDATE_MANABAR, UpdateManaBar);
        observer.AddToList<float>(ObserverCostant.UPDATE_ENERGYBAR, UpdateEnergyBar);
    }

    void UpdateHealtBar(float val)
    {
        healtContent.DOFillAmount(val, performTime);
    }
    void UpdateManaBar(float val)
    {
        manaContent.DOFillAmount(val, performTime);
    }
    void UpdateEnergyBar(float val)
    {
        energyContent.DOFillAmount(val, performTime);
    }
    private void OnDestroy()
    {
        observer.RemoveToList<float>(ObserverCostant.UPDATE_HPBAR, UpdateHealtBar);
        observer.RemoveToList<float>(ObserverCostant.UPDATE_MANABAR, UpdateManaBar);
        observer.RemoveToList<float>(ObserverCostant.UPDATE_ENERGYBAR, UpdateEnergyBar);
    }
}
