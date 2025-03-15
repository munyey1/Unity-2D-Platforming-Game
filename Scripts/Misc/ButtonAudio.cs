using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Positive;
    public AudioClip Negative;

    public void pos(){
        Source.PlayOneShot(Positive);
        //Play the forward sound once.
    }
    public void neg(){
        Source.PlayOneShot(Negative);
        //Play the backwards sound once.
    }

}
