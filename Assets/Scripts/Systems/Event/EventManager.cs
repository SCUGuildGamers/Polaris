using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public List<string> flag_keys = new List<string> { "Net", "Stick", "Loop", "ConstructedNet" };

    private Dictionary<string, bool> _flags;

    // Temporary fields to implement the net crafting event
    private DialogueManager _dialogueManager;
    public Dialogue Dialogue;

    void Start()
    {
        // Intialize the flag dictionary
        _flags = new Dictionary<string, bool>();

        foreach (string key in flag_keys)
            _flags.Add(key, false);

        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Parses the string flag and updates the _flags dictionary appropriately
    public void ProcessFlag(string flag)
    {
        if (flag_keys.Contains(flag)) {
            _flags[flag] = true;
            ShowFlags(); // Debug
        }

        else 
            throw new KeyNotFoundException("Event key not found");
    }

    // Internally reviews the current state of the flags to check for any special cases
    public void InternalUpdate()
    {
        // Constructed Net case
        if (_flags["Net"] && _flags["Stick"] && _flags["Loop"] && !_flags["ConstructedNet"])
        {
            _flags["ConstructedNet"] = true;
            _dialogueManager.StartDialogue(Dialogue);
        }
    }

    // Helper function to debug and check the current value of all flags
    private void ShowFlags()
    {
        foreach (string key in flag_keys)
            Debug.Log(key + ", " + _flags[key]);
    }

    // Returns the boolean for the event
    public bool GetEvent(string flag)
    {
        if (flag_keys.Contains(flag))
            return _flags[flag];
        throw new KeyNotFoundException("Event key not found");
    }
}
