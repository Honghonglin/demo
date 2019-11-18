using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollision : MonoBehaviour
{
    [HideInInspector]
    public GameMode gameMode;

    private void Start()
    {
        gameMode = GameObject.FindObjectOfType<GameMode>();
    }

    private void Update()
    {
        
    }


    private void OnTriggerExit(Collider other)
    {
    //    var player=other.gameObject.GetComponent
    }
}
