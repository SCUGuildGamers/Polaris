using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 _startingPosition;

    void Start()
    {
        _startingPosition = transform.position;
    }

    void Update() { 
        transform.position = _startingPosition + (Vector3.up * (Mathf.Cos(Time.time * 5f) * 0.1f));
    }
}
