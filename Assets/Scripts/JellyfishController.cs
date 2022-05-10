using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : MonoBehaviour
{
    public Transform jellyfish;
    public Transform plasticBoss;
    public Transform player;

    public GameObject plasticDrop;
    public int plasticDropFreq = 5;
    private int plasticDropCounter = 0;

    private bool spawned = false;
    private Vector3 targetPosition;    

    private Vector2 startPosition;
    private float lifeSpan;
    private int duration;
    private float delta;
    private float speed;
    public Vector2 pos;

    private int attack_mode;

    private void Update()
    {
        if (spawned == true)
        {
            if (attack_mode == 1){
                loop_clockwise();
            } else if (attack_mode == 2){
                loop_counter_clockwise();
            } else if (attack_mode == 3){
                straight_line();
            } else if (attack_mode == 4){
                pos = jellyfish.position;
                to_target();
            }

            if (duration > lifeSpan)
            {
                Destroy(gameObject);
            }
            // DropTrash();
        }
    }

    private void loop_clockwise(){
        jellyfish.RotateAround(targetPosition, new Vector3(0,0,1), 0.03f);
        targetPosition.x += delta;
        duration++;
    }

    private void loop_counter_clockwise(){
        jellyfish.RotateAround(targetPosition, new Vector3(0,0,-1) , 0.03f);
        targetPosition.x -= delta;
        duration++;
    }

    private void straight_line(){
        // jellyfish.position = Vector3.MoveTowards(jellyfish.position, targetPosition, 0.005f);
        jellyfish.position += (targetPosition - jellyfish.position).normalized * speed;
        duration++;
    }

    private void to_target(){
        if (jellyfish.position == targetPosition){
            Destroy(gameObject);
        }
        jellyfish.position = Vector3.MoveTowards(jellyfish.position, targetPosition, 0.003f);
    }

    private void DropTrash()
    {
        if(plasticDropCounter == plasticDropFreq)
        {
            GameObject plasticDropCopy = Instantiate(plasticDrop, jellyfish.position, jellyfish.rotation);
            plasticDropCopy.SetActive(true);
            plasticDropCopy.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
            plasticDropCounter = 0;
            return;
        }

        plasticDropCounter++;
    }

    public void Spawn(int x, int y, int mode, float d)
    {
        gameObject.SetActive(true);
        spawned = true;

        //set start position and attack mode
        lifeSpan = 4000;
        startPosition = jellyfish.position;
        delta = d;
        targetPosition = new Vector2(x,y);
        attack_mode = mode;
        duration = 0;
        speed = 0.004f;
    }  
}
