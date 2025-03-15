using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player") && !other.isTrigger){
            virtualCam.SetActive(true);
            //If the player triggers the zone, then activate the camera.
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Player") && !other.isTrigger){
            virtualCam.SetActive(false);
            //When the player leaves the trigger zone, disable the camera.
        }
    }
}

