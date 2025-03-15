using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator anim;
    public float sceneWait;

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Player")){
            StartCoroutine(LoadScene());
            //When the player collides, load next scene.
        }
    }

    public void StartLoad(){
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene(){
        anim.SetTrigger("StartFade");
        //Starts scene transition.
        yield return new WaitForSeconds(sceneWait); //Delay.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Loads the next scene.
    }
}
