using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanted : MonoBehaviour
{

    [SerializeField] FarmingSystem farmingSystem;
    PlayerMovement movement;
    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }
    
}
