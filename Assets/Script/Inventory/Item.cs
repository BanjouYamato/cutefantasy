using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] 
    public ItemScriptable item;

    [field: SerializeField]
    public int qty { get; set; } = 1;

    [SerializeField]
    float duration = 0.3f;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.Icon;
    }
    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUP());
    }

    private IEnumerator AnimateItemPickUP()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float curTime = 0;
        while (curTime < duration)
        {
            curTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, curTime/duration);
            yield return null;
        }
        transform.localScale = endScale;
        Destroy(gameObject);
    }
}
