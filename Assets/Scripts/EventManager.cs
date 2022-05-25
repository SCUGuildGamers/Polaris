using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public bool Net;
    public bool Stick;
    public bool Loop;
    public bool ConstructedNet;

    private bool _inDialogue;

    void Start()
    {
        Net = false;
        Stick = false;
        Loop = false;
        ConstructedNet = false;
        _inDialogue = false;
    }

    void Update()
    {
        if (!ConstructedNet && Net && Stick & Loop)
            ConstructedNet = true;

        if (ConstructedNet && FindObjectOfType<PlayerMovement>().CanPlayerMove && !_inDialogue)
        {
            GetComponent<Interactable>().SayDialogue();
            _inDialogue = true;
        }

        if (ConstructedNet && _inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Interactable>().SayDialogue();
            }
        }
            
    }
}
