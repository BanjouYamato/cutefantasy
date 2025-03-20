using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadows : MonoBehaviour
{
    [SerializeField] GameObject shadowPrefab;
    [SerializeField] List<GameObject> shadowsPool = new List<GameObject>();
    [SerializeField] Transform shadowBag;
    public GameObject GetShadow()
    {
        for (int i = 0; i < shadowsPool.Count; i++)
        {
            if (!shadowsPool[i].activeInHierarchy)
            {
                shadowsPool[i].SetActive(true);
                shadowsPool[i].GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                shadowsPool[i].transform.position = transform.position;
                shadowsPool[i].transform.rotation = transform.rotation;
                return shadowsPool[i];
            }
        }
        GameObject newShadow = Instantiate(shadowPrefab, transform.position, transform.rotation);
        newShadow.transform.SetParent(shadowBag);
        newShadow.GetComponent<SpriteRenderer>().sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        shadowsPool.Add(newShadow);
        return newShadow;
    }
}
