using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    private float _swingRange = 2f;
    public Animator animator;
    public float swingCD = 1.0f;
    public float nextSwing = 0.0f;
    private BossHealth _bossHealth;
    public Transform Boss;
    void Start()
    {
        _bossHealth = FindObjectOfType<BossHealth>();
    }

    void Update()
    {
        bool isSwing = false;
        animator.SetBool("isSwinging", isSwing);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Time.time + " " + nextSwing);
            if (Time.time > nextSwing)
            {
                isSwing = true;
                animator.SetBool("isSwinging", isSwing);

                if (!Swing())
                {
                    Debug.Log("miss");

                    nextSwing = Time.time + 1;
                    
                }
                else
                {
                    Debug.Log("hit");
                    nextSwing = Time.time;
                    
                }
                
            }
            else
            {
                Debug.Log("on cooldown");
            }
            

        } 
        /*       
        {
            Debug.Log("nextSwing" + nextSwing);
            Debug.Log("the time" + Time.time);
            
            Debug.Log("nextSwing" + nextSwing);
            Debug.Log("the time" + Time.time);
            //Swing();
            //test();

        }
        */    
    }
    
    public bool Swing()
    {
        Plastic[] plasticList = FindObjectsOfType<Plastic>();
        bool reflected = false;
        Plastic minPlastic = null;
        float minDistance = _swingRange;
        

        foreach (Plastic plastic in plasticList)
        {
            if (Vector3.Distance(transform.position, plastic.transform.position) <= minDistance && plastic.CanPickup)
            {
                minPlastic = plastic;
                minDistance = Vector3.Distance(transform.position, plastic.transform.position);
            }
        }

        if (minPlastic != null)
        {
            //Destroy(minPlastic.gameObject);
            minPlastic.changeDirection();
            reflected = true;
            _bossHealth.ReduceHealth(10);
        }
        return reflected;
        
    }
}
