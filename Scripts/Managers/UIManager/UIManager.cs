using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class UIManager : MonoBehaviour{

    private static UIManager instance;
    public static UIManager MyInstance{
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    /*This is a singleton pattern taht is different used to the gamemaster
     * What this version does, is allow other scripts to call MyInstance 
     * without having to specify where to get it in the awake method
    */
    [SerializeField]
    private CanvasGroup keybindMenu;
    /*SerializeField allows the variable to be seen in the Unity inspector.
     *This is set to private so no other script can alter this canvas. 
    */

    private GameObject[] keybindButtons;
    /*keybindButtons is a gameobject type of array.
     * This array will hold all buttons of the keybind menu.
    */

    private void Awake(){
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        /*The array is initialised and finds all gameobject with tag "Keybind"
         * and adds them to the array.
        */
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            OpenCloseMenu();
        }
    }

    public void OpenCloseMenu(){
        if (keybindMenu.alpha > 0) {
            keybindMenu.alpha = 0;
        } else{
            keybindMenu.alpha = 1;
        }
        //Make the pause menu appear.

        if (keybindMenu.blocksRaycasts == true){
            keybindMenu.blocksRaycasts = false;
        } else{
            keybindMenu.blocksRaycasts = true;
        }
        //Block any raycasts from triggering.

        if (Time.timeScale > 0){
            Time.timeScale = 0;
        } else{
            Time.timeScale = 1;
        }
        //Set the time scale to 0.
    }

    public void UpdateKeyText(string key, KeyCode code){//Funciton solely for updating the text on the buttons.
        TextMeshProUGUI tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<TextMeshProUGUI>();
        /*This line of code finds the button in the keybindButtons array that has the same name as the string key
         * passed to it. It does this by checking all gameobjects in the array and compares it too the string.
         * It is like a foreach loop.
         */
        tmp.text = code.ToString();
        //Text is updated.
    }

}
