using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleInEntrance : MonoBehaviour
{
    [SerializeField]
    string teleInEntranceName;

    [SerializeField]
    Transform player;

    private void Start()
    {
        TeleEntrancePlayer();
    }
    void TeleEntrancePlayer()
    {   
        if (teleInEntranceName == SceneControler.Instance.TransitionName)
        {
            player.position = this.transform.position;
        }
    }
}
