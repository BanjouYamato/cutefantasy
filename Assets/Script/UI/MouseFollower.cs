using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;
    [SerializeField]
    UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }
    public void SetData(Sprite sprite, int qty)
    {
        item.SetData(sprite, qty);
    }
    private void Update()
    {
        GetPos();
    }
    void GetPos()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out pos);
        transform.position = canvas.transform.TransformPoint(pos);
    }
    public void Toogle(bool val)
    {
        gameObject.SetActive(val);
    }
}
