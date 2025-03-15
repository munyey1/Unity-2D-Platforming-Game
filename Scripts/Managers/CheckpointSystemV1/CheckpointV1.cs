using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointV1 : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gm.lasCheckPointPos = transform.position;
        } // if the checkpoint has detected a collision with the player
        // update the last checkpoint position to this checkpoint position
    }
}