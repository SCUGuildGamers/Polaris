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
    private PlayerMovement _playerMovement;
    private PlayerSwing _playerSwing;
    private bool _trigger;
    private bool _movementAllowed;
    private bool _requestedClick;

    // Start is called before the first frame update
    private void Start()
    {
        _playDirector = GetComponent<PlayableDirector>();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _eventManager = FindObjectOfType<EventManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerSwing = FindObjectOfType<PlayerSwing>();
        _trigger = true;
        _movementAllowed = false;
        _requestedClick = false;
    }

    // Pauses the timeline
    public void PauseTimeline()
    {
        Debug.Log("Pause Timeline signal recieved");
        _playDirector.playableGraph.GetRootPlayable(0).Pause();
    }

    public void setTrigger(bool value)
    {
        _trigger = value;
    }

    public void setMovement(bool value)
    {
        _movementAllowed = value;
        if (_playerMovement != null)
        {
            _playerMovement.CanPlayerMove = value;
            _playerMovement.CanPlayerGlide = value;
        }
        if (_playerSwing != null)
            _playerSwing.CanPlayerSwing = value;
    }

    // Resets the timeline
    public void ResetTimeline(float duration)
    {
        if (_eventManager != null && _eventManager.FlagValue("repeat"))
        {
            _eventManager.ResetChoice("repeat");
            _playDirector.time -= duration;
            _playDirector.Pause();
            _playDirector.Evaluate();
            _playDirector.Resume();

        }
    }

    public void RequestClick()
    {
        _trigger = false;
        _requestedClick = true;
    }

    // Resumes the timeline once dialogue is over
    private void Update()
    {
        if (_dialogueManager)
        {
            if (!_dialogueManager.InDialogue && _trigger)
                _playDirector.playableGraph.GetRootPlayable(0).Play();
            if (Input.GetMouseButtonDown(0) && _requestedClick)
            {
                _requestedClick = false;
                _trigger = true;
            }
            if (!_movementAllowed)
            {
                if (_playerMovement != null)
                {
                    _playerMovement.CanPlayerMove = false;
                    _playerMovement.CanPlayerGlide = false;
                }
                if (_playerSwing != null)
                    _playerSwing.CanPlayerSwing = false;
            }
        }
        
    }
}
