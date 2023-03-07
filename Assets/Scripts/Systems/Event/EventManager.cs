using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    private string[] flag_keys = new string[] { "Net", "Stick", "Loop", "ConstructedNet", "Test choice 1", "Test choice 2", "boss1beat", "boss2beat" };

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

    void Update(){
        if(Input.GetKeyDown(KeyCode.L)){
            foreach (var pair in _flags)
            {
                Debug.Log(pair.Key + ' ' + pair.Value.ToString());
            }
        }
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

        // Increase glide charge from NPC interaction
        else if (flag == "increaseGlideCharge") {
            FindObjectOfType<GlideCharge>().AddCharge();
        }
    }

    public void ProcessChoice(string flag)
    {
        _flags[flag] = true;
        ShowFlags();
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

    public bool FlagValue(string flag)
    {
        ShowFlags();

        return _flags[flag];
    }

    public bool NetCompleted()
    {
        return _flags["ConstructedNet"];
    }

    public void SetFlags(bool [] vals){
        _flags = new Dictionary<string, bool>();

        int i = 0;
        foreach (string key in flag_keys){
            _flags.Add(key, vals[i]);
            i++;
        }

    }

    public Dictionary<string, bool> GetFlags(){
        return _flags;
    }
}
