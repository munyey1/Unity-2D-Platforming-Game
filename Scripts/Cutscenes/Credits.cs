using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    private int index;
    public float typingSpeed;
    public float wait;
    public float pause;

    public AudioSource sound;

    private void Start(){
        StartCoroutine(Type());
        //Start the scene with the credits. 
    }

    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            sound.Play();
            //Player the dialogue sound. 
            yield return new WaitForSeconds(typingSpeed);
        }
        StartCoroutine(NextSentence());
        //After each line, increment and enter NextSentence.
    }

    IEnumerator NextSentence(){
        if (index < sentences.Length - 1){
            index++;

            yield return new WaitForSeconds(wait);

            textDisplay.text += "\n";
            //Starts the next line
            StartCoroutine(Type());
            //Plays the next sentence
        }else {

            yield return new WaitForSeconds(pause);
            textDisplay.text = "Thanks For Playing!";
            //Final message to the user.
            yield return new WaitForSeconds(pause);
            PlayerPrefs.DeleteAll();
            SaveSystem.DeleteSave();
            //Delete all saved data to reset.
            SceneManager.LoadScene(0);
            //Load main menu.
        }
    }
}
