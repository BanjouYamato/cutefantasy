using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UILoadScene : MonoBehaviour
{   
    [SerializeField]
    TextMeshProUGUI loadingText;
    [SerializeField]
    CanvasGroup loadScenePanel;

    public bool isLoading;


    IEnumerator AnimateDots()
    {
        while (true)
        {
            loadingText.text = ".";
            yield return new WaitForSeconds(0.2f);
            loadingText.text = "..";
            yield return new WaitForSeconds(0.2f);
            loadingText.text = "...";
            yield return new WaitForSeconds(0.2f);
            loadingText.text = "";
            yield return new WaitForSeconds(0.2f);
        }
    }
    void Toggle(bool val)
    {
        if (!GameControler.Instance.isMainMapScene)
            return;
        if (!val)
        {
            StopAllCoroutines();
            loadScenePanel.DOFade(0, 0.05f);
        }  
        loadScenePanel.gameObject.SetActive(val);
        if (val)
        {
            loadScenePanel.DOFade(1, 0.05f);
            StartCoroutine(AnimateDots());
        }
    }
    private void Start()
    {
        Observer.Instance.AddToList<bool>(GameConstant.UI_LOADSCENE, Toggle);
    }
    private void OnDestroy()
    {
        Observer.Instance.RemoveToList<bool>(GameConstant.UI_LOADSCENE, Toggle);
        transform.DOKill();
    }
}
