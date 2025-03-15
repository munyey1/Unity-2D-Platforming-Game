using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class keybindManager : MonoBehaviour{

    private static keybindManager instance;
    public static keybindManager MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<keybindManager>();
            }
            return instance;
        }
    }
    /*This is a singleton pattern that is different used to the gamemaster
     * What this version does, is allow other scripts to call MyInstance 
     * without having to specify where to get it in the awake method
     */

    public Dictionary<string, KeyCode> Keybinds { get; private set; }
    /*Keybinds is a dictionary that will store all the keybinds.
     * It has a property assigned to it, where other scripts can access it
     * but cannot change it.
     */
    public string bindName { get; private set; }
    //bindName is used for changing the keybind and checking if that keybind is empty.

    // Start is called before the first frame update
    void Start(){
        Keybinds = new Dictionary<string, KeyCode>();
        //The dictionary is initialised.

        BindKey("UP", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("UP", "UpArrow")));
        BindKey("LEFT", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LEFT", "LeftArrow")));
        BindKey("DOWN", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DOWN", "DownArrow")));
        BindKey("RIGHT", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RIGHT", "RightArrow")));
        BindKey("JUMP", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JUMP", "Space")));
        BindKey("DASH", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DASH", "LeftShift")));
        /*BindKey is called for all available keybinds in the game. The first parameter corresponds to all 
         * the movemment. The second parameter, gathers the saved data from the user and converts the saved 
         * string to an Enum, as keycodes are Enums
         */
    }

    public void BindKey(string key, KeyCode keyBind){
        if (!Keybinds.ContainsKey(key)){
            //Checks if the dictionary has the key that is passed to it
            Keybinds.Add(key, keyBind);
            //If there is not then add the key and keybind to the dictionary
            UIManager.MyInstance.UpdateKeyText(key, keyBind);
            //Update the text on the button to display the keybind        
        } else if(Keybinds.ContainsValue(keyBind)){
            //If there is already an existing keybind
            string myKey = Keybinds.FirstOrDefault(x => x.Value == keyBind).Key;
            /*Search through the dictionary to find what the old key is that is assinged to the 
             * existing keybind, and set it to a new string called myKey.
             */
            Keybinds[myKey] = KeyCode.None;
            //Set the old key to have that had the existing keybind to nothing
            UIManager.MyInstance.UpdateKeyText(myKey, KeyCode.None);
            //Update the button text to have the word 'none' 
        }

        Keybinds[key] = keyBind;
        //The new key gets the keybind
        UIManager.MyInstance.UpdateKeyText(key, keyBind);
        //Button text is updated.
        bindName = string.Empty;
        //Checking condition for assigning
    }

    public void KeyBindOnClick(string bindName){
        this.bindName = bindName;
        /*This funtion is assigned to a keybind button that once pressed
         * will enter this function with the corresponding keybind passed 
         * as an argument, and sets it to bindName
         */
    }

    private void OnGUI(){
        if(bindName != string.Empty){
            //Checks if bindName has been assigned
            Event e = Event.current;
            /*If it has, then check for user input and assign to random
             * variable, e
             */
            if (e.isKey && e.keyCode != KeyCode.Escape && e.keyCode != KeyCode.LeftWindows && e.keyCode != KeyCode.RightWindows){
                //if e is a key on the keyboard, then assign
                BindKey(bindName, e.keyCode);
            } 
        }
    }

    public void SaveKeys(){
        foreach(var key in Keybinds){
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            //For each key in the dictionary, set its value to the action.
        }
        PlayerPrefs.Save();
        //Save the dictionary values once they all have been set.
    }
}
    