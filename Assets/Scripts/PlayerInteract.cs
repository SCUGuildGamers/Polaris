using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private float _interactRange = 5;

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
        Interactable[] interactables = FindObjectsOfType<Interactable>();

        // Ensures that there are interactables in the scene
        if (interactables.Length != 0)
        {
            // Iterate through the interactables to find the closest interactable to the player
            int minInteractIndex = 0;
            float minDistance = Vector3.Distance(transform.position, interactables[0].transform.position);
            for (int i = 0; i < interactables.Length; i++)
            { 
                float distance = Vector3.Distance(transform.position, interactables[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minInteractIndex = i;
                }
            }

            // Ensures that the player only interacts with interactables that are within their range
            if (minDistance <= _interactRange)
            {
                bool canPlayerMove = interactables[minInteractIndex].SayDialogue();

                // Updates whether or not the player can move or not
                GetComponent<PlayerMovement>().CanPlayerMove = canPlayerMove;
            }
                
        }
    }
}
