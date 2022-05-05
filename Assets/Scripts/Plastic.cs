using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer spriteRenderer = GetComponent<Renderer>();
        spriteRenderer.enabled = false;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Plastic plasticCopy = new Plastic();
    }
}
