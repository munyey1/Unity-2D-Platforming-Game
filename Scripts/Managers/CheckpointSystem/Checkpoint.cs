using UnityEngine;

public class Checkpoint : MonoBehaviour{

    private GameMaster gm;

    void Start(){
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            gm.lasCheckPointPos = transform.position;
        } // if the checkpoint has detected a collision with the player
        // update the last checkpoint position to this checkpoint position
    }
}
