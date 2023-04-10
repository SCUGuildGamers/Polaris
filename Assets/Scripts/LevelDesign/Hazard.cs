using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Player Kill & respawn");

            // Return the player to their checkpoint on death
            FindObjectOfType<CheckpointManager>().ReturnToCheckpoint();
        }  
    }
}
