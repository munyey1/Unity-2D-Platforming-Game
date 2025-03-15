using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathV1 : MonoBehaviour
{
    private GameMaster gm;
    public float spawnDelay;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Invoke("Respawn", spawnDelay);
        } // if the player collides with an enemy, deactivate and enter respawn function
    }
    private void Respawn()
    {
        transform.position = gm.lasCheckPointPos;
        gameObject.SetActive(true);
    } // teleport the player at the lastcheckpoint and then activate the player again
}
