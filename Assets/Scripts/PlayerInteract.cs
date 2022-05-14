using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private float range = 5;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    // Finds the nearest interactable within the range
    private void Interact()
    {
        float minDistance = range;

        Interactable minInteractable;
        Interactable[] interactables = FindObjectsOfType<Interactable>();
        foreach (Interactable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);
            if (distance <= minDistance)
                minInteractable = interactable;
        }

        Debug.Log(minInteractable);
    }
}
