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
                straight_line_orbit();
            } else if (attack_mode == 5){
                straight_line_orbit_r();
            }

            if (duration > lifeSpan)
            {
                Destroy(gameObject);
            }
            // DropTrash();
        }
    }

    private void loop_clockwise(){
        jellyfish.RotateAround(targetPosition, new Vector3(0,0,1), 0.08f);
        targetPosition.x += delta;
        duration++;
    }

    private void loop_counter_clockwise(){
        jellyfish.RotateAround(targetPosition, new Vector3(0,0,-1) , 0.08f);
        targetPosition.x -= delta;
        duration++;
    }

    private void straight_line(){
        jellyfish.position = Vector3.MoveTowards(jellyfish.position, targetPosition, 0.01f);
        duration++;
    }

    private void straight_line_orbit(){
        float delta_x = (targetPosition.x - startPosition.x) / lifeSpan;
        float delta_y = (targetPosition.y - startPosition.y) / lifeSpan;
        // Debug.Log(delta_x + " " + delta_y);
        Vector2 center = new Vector2(startPosition.x + (delta_x * duration), startPosition.y + ( delta_y * duration));
        jellyfish.RotateAround(center, new Vector3(0,0,-1) , 1.2f);
        duration++;
    }

    private void straight_line_orbit_r(){
        float delta_x = (targetPosition.x - startPosition.x) / lifeSpan;
        float delta_y = (targetPosition.y - startPosition.y) / lifeSpan;
        // Debug.Log(delta_x + " " + delta_y);
        Vector2 center = new Vector2(startPosition.x + (delta_x * duration), startPosition.y + ( delta_y * duration));
        jellyfish.RotateAround(center, new Vector3(0,0,1) , 1.2f);
        duration++;
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
        lifeSpan = 2000;
        startPosition = jellyfish.position;
        delta = d;
        targetPosition = new Vector2(x,y);
        attack_mode = mode;
        duration = 0;
    }  
}
