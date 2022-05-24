using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform indicatorplaceholder;
    public Transform player;
    public Dialogue lines;
    public DialogueTrigger Trigger;

    void Start()
    {
        Vector3 pos = transform.position;
        pos.y += 1.6f;
        indicatorplaceholder.transform.position = pos;
        indicatorplaceholder.gameObject.SetActive(false);
    }

    void Update()
    {
        if(is_interactable()){
            indicatorplaceholder.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Trigger.SayDialogue(lines);
            }
        } else {
            indicatorplaceholder.gameObject.SetActive(false);
        }
    }

    // Runs the dialogue attached to this interactable and returns whether or not the player can move or not
    public bool Interact()
    {
        return GetComponent<DialogueTrigger>().SayDialogue(lines);
    }

    public bool is_interactable(){
        float dist = Mathf.Sqrt(Mathf.Pow(transform.position.x - player.transform.position.x, 2) + Mathf.Pow(transform.position.y - player.transform.position.y, 2));
        if (dist <= 1.5){
            return true;
        }
        return false;
    }
}
