using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastic : MonoBehaviour
{
    public bool isCopy = false;

    public Plastic Spawn(Vector3 position)
    {
        GameObject plasticCopy = Instantiate(gameObject);

        Plastic plasticObjCopy = plasticCopy.GetComponent<Plastic>();
        plasticObjCopy.isCopy = true;
        plasticObjCopy.GetComponent<Transform>().position = position;
        plasticObjCopy.GetComponent<SpriteRenderer>().enabled = true;
        return plasticObjCopy;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
