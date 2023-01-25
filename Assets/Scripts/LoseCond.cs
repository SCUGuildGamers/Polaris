using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCond : MonoBehaviour
{
    public int Respawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
            
        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Player Kill & respawn");
            SceneManager.LoadScene(Respawn);
        }
       
    }
}
