using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Playback : MonoBehaviour
{
    // Shows the private variable in the editor
    [SerializeField] private PlayableDirector _playDirector;
    private DialogueManager _dialogueManager;
    private EventManager _eventManager;

    // Start is called before the first frame update
    private void Start()
    {
        _playDirector = GetComponent<PlayableDirector>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _eventManager = FindObjectOfType<EventManager>();
    }

    // Pauses the timeline
    public void PauseTimeline()
    {
        Debug.Log("Pause Timeline signal recieved");
        _playDirector.Pause();
    }

    // Resets the timeline
    public void ResetTimeline()
    {
        if (_eventManager != null && _eventManager.FlagValue("repeat"))
        {
            _eventManager.ResetChoice("repeat");
            _playDirector.time = 0;
            _playDirector.Stop();
            _playDirector.Evaluate();
            _playDirector.Play();

        }
    }

    // Resumes the timeline once dialogue is over
    private void Update()
    {
        if (!_dialogueManager.InDialogue)
            _playDirector.Play();
    }
}
