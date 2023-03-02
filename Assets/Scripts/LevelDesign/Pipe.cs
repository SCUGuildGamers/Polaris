using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private bool isClogged;

    // Direction of projectile shooting ("Up", "Right", "Down", "Left")
    [SerializeField]
    private string direction;

    // Duration between firing
    private float period = 5f;

    private void Start()
    {
        isClogged = false;
        StartCoroutine(StartShooting());
    }

    // Starts automatic shooting given the period time
    IEnumerator StartShooting()
    {
        while (true) 
        {
            Shoot();
            yield return new WaitForSeconds(period);
        }
    }

    void Shoot() {
        Debug.Log("Shoot");
    }
}
