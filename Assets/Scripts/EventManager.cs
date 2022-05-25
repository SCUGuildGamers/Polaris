using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private string[] flag_keys = new string[] { "Net", "Stick", "Loop", "ConstructedNet" };

    private Dictionary<string, bool> _flags;

    // Temporary field to implement the net crafting event
    private DialogueManager _dialogueManager;

    void Start()
    {
        _flags = new Dictionary<string, bool>();

        foreach (string key in flag_keys)
            _flags.Add(key, false);
    }

    // Parses the string flag and updates the _flags dictionary appropriately
    public void ProcessFlag(string flag)
    {
        if (flag == "gotNet")
            _flags["Net"] = true;

        else if (flag == "gotStick")
            _flags["Stick"] = true;

        else if (flag == "gotLoop")
            _flags["Loop"] = true;
        
        // Debug statement to check the current value of all flags
        ShowFlags();
    }

    // Internally reviews the current state of the flags to check for any special cases
    public void InternalUpdate()
    {
        // Constructed Net case
        if (_flags["Net"] && _flags["Stick"] && _flags["Loop"])
        {
            _flags["ConstructedNet"] = true;
            _dialogueManager.SayDialogue(GetComponent<Interactable>().Dialogue);
        }
            
    }

    // Helper function to debug and check the current value of all flags
    private void ShowFlags()
    {
        foreach (string key in flag_keys)
            Debug.Log(key + ", " + _flags[key]);
    }
}
