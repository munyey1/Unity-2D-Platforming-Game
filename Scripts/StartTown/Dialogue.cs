using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour{

    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    private int index;
    public float typingSpeed;
    public float wait;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private BoxCollider2D trigger;

    public AudioSource sound;

    private void Update(){
        if(textDisplay.text == sentences[index]){
            StartCoroutine(NextSentence());
        }
    }

    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            sound.Play();
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator NextSentence(){
        if (index < sentences.Length - 1){
            index++;
            yield return new WaitForSeconds(wait);

            textDisplay.text = "";
            StartCoroutine(Type());
        } else{

            yield return new WaitForSeconds(wait);

            textDisplay.text = "";
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            trigger.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            StartCoroutine(Type());
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            trigger.enabled = false;
        }
    }
}
