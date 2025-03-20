using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowEvent : MonoBehaviour
{
    [SerializeField] 
    GameObject arrowPrefab;
    List<GameObject> arrowList = new List<GameObject>();
    [SerializeField] 
    Transform arrowPos;
    [SerializeField] 
    PlayerControler controler;
    [SerializeField] 
    Transform arrowBag;
    int currentIndex;

    private void Start()
    {
        controler = PlayerControler.instance;
        AddArrow();
    }
    void AddArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newArrow = Instantiate(arrowPrefab, arrowPos.position, ArrowDir());
            newArrow.transform.SetParent(arrowBag);
            arrowList.Add(newArrow);
            newArrow.SetActive(false);
        }
    }
    public GameObject GetArrow()
    {
        for (int i = 0; i < arrowList.Count; i++)
        {
            if (!arrowList[currentIndex].activeInHierarchy)
            {   
                GameObject arrow = arrowList[currentIndex];
                arrow.SetActive(true);
                arrow.transform.position = arrowPos.position;
                arrow.transform.rotation = ArrowDir();
                currentIndex = (currentIndex + 1) % arrowList.Count;
                return arrow;
            }
        }
        GameObject newArrow = Instantiate(arrowPrefab, arrowPos.position, ArrowDir());
        newArrow.transform.SetParent(arrowBag);
        arrowList.Add(newArrow);
        return newArrow;
    }
    Quaternion ArrowDir()
    {
        if (controler.PlayerStats.dir == Direction.up) return Quaternion.Euler(0, 0, 180f);
        else if (controler.PlayerStats.dir == Direction.down) return Quaternion.Euler(0, 0, 0);
        else
        {
            if (PlayerControler.instance.PlayerMovement.GetRightFace()) return Quaternion.Euler(0, 0, 90);
            else return Quaternion.Euler(0, 0, -90);
        }
    }
}
