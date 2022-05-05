using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public bool isCopy = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Renderer spriteRenderer = GetComponent<Renderer>();
        spriteRenderer.enabled = false;
    }

    public Plastic Spawn()
    {
        GameObject plasticCopy = Instantiate(gameObject);
        Plastic plasticObjCopy = GetComponent<Plastic>();
        plasticObjCopy.isCopy = true;
        plasticObjCopy.GetComponent<Renderer>().enabled = true;
        return plasticObjCopy;
    }
}
