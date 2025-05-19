using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionPanel : MonoBehaviour
{
    [SerializeField]
    GameObject buttonPrefab;

    public void AddButton(string name, Action onClickAction)
    {
        GameObject button = Instantiate(buttonPrefab, transform);
        button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
        button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
    }

    public void Toggle(bool val)
    {
        if (val)
            RemoveOldButtons();
        gameObject.SetActive(val);
    }

    public void RemoveOldButtons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
