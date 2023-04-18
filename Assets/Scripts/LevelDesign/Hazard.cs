using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player Kill & respawn");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }  
    }
}
