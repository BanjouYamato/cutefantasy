using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiBarManager : MonoBehaviour
{
    Observer observer;
    [Header("Healh Bar")]
    [SerializeField]
    RectTransform healtBar, healtContent;
    [Header("Mana Bar")]
    [SerializeField]
    RectTransform manaBar, manaContent;
    [Header("Energy Bar")]
    [SerializeField]
    RectTransform energytBar, energyContent;

    private void Start()
    {
        observer = Observer.Instance;
        observer.AddToList<float>(ObserverCostant.UPDATE_HPBAR, UpdateHealtBar);
        observer.AddToList<float>(ObserverCostant.UPDATE_MANABAR, UpdateManaBar);
        observer.AddToList<float>(ObserverCostant.UPDATE_ENERGYBAR, UpdateEnergyBar);
    }

    void UpdateHealtBar(float val)
    {
        healtContent.sizeDelta = new Vector2(val * healtBar.sizeDelta.x
            , healtContent.sizeDelta.y);
    }
    void UpdateManaBar(float val)
    {
        manaContent.sizeDelta = new Vector2(val * manaBar.sizeDelta.x
            , manaContent.sizeDelta.y);
    }
    void UpdateEnergyBar(float val)
    {
        energyContent.sizeDelta = new Vector2(val * energytBar.sizeDelta.x
            , energyContent.sizeDelta.y);
    }
    private void OnDestroy()
    {
        observer.RemoveToList<float>(ObserverCostant.UPDATE_HPBAR, UpdateHealtBar);
        observer.RemoveToList<float>(ObserverCostant.UPDATE_MANABAR, UpdateManaBar);
        observer.RemoveToList<float>(ObserverCostant.UPDATE_ENERGYBAR, UpdateEnergyBar);
    }
}
