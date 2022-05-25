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
    }

    void Update()
    {    
    }
}
