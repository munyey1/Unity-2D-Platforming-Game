using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{
    private PlayableDirector cutscene;

    private void Awake(){
        cutscene = GetComponent<PlayableDirector>();
        if(PlayerPrefs.GetInt("Saved") != 0){
            cutscene.Play();
            //If the user has loaded from the previous scene, then play this cutscene.
        }
    }
}
