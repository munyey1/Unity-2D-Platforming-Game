using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour{

    public void LoadScene(){
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("Saved", 1);
            PlayerPrefs.Save();

        }
        else{
            SceneManager.LoadScene(data.savescene);//Loads scene that was saved
        }
    }
    public void QuitGame(){
        Debug.Log("The application has closed.");
        PlayerPrefs.SetInt("Saved", 0);
        PlayerPrefs.Save();

        Application.Quit();
    }

}
