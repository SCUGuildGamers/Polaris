using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Playback : MonoBehaviour
{
    // Shows the private variable in the editor
    [SerializeField] private PlayableDirector _playDirector;
    private DialogueManager _dialogueManager;

    // Start is called before the first frame update
    private void Start()
    {
        _playDirector = GetComponent<PlayableDirector>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Pauses the timeline
    public void PauseTimeline()
    {
        Debug.Log("Pause Timeline signal recieved");
        _playDirector.Pause();
    }

    // Resumes the timeline once dialogue is over
    private void Update()
    {
        if (!_dialogueManager.InDialogue)
            _playDirector.Play();
    }
}
