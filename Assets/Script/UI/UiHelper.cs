using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHelper : MonoBehaviour
{
    [SerializeField]
    GameObject uiHelper;

    public static UiHelper Instance { get; private set; }

    private void Awake()
    {   
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {   
        uiHelper.SetActive(false);
    }
    public static void Toogle(bool val)
    {
        Instance.uiHelper.SetActive(val);
    }
}
