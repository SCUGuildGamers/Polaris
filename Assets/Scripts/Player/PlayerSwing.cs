using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    // Swing values
    private float _swingRange = 2f;
    public float swingCD = 1.0f;
    public float nextSwing = 0.0f;

    // For reference
    private BossHealth _bossHealth;
    

    //public Animator animator;

    void Start()
    {
        _bossHealth = FindObjectOfType<BossHealth>();
    }

    void Update()
    {
        //animator.SetBool("isSwinging", isSwing);

        // Check for left-click
        if (Input.GetMouseButtonDown(0))
        {
            // Off-cooldown
            if (Time.time > nextSwing)
            {
                //animator.SetBool("isSwinging", isSwing);

                // Swing if in range
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

            // On-cooldown
            else
            {
                Debug.Log("on cooldown");
            }
            

        }  
    }

    // Looks for the nearest plastic within range and reflects the nearest towards the boss; returns the reflected plastic
    private bool Swing()
    {
        Plastic[] plasticList = FindObjectsOfType<Plastic>();
        bool reflected = false;
        Plastic minPlastic = null;
        float minDistance = _swingRange;
        
        
        // Find the closest plastic in range
        foreach (Plastic plastic in plasticList)
        {
            if (Vector3.Distance(transform.position, plastic.transform.position) <= minDistance && plastic.CanReflect)
            {
                Debug.Log("in range");
                minPlastic = plastic;
                minDistance = Vector3.Distance(transform.position, plastic.transform.position);
            }
        }

        // Reflect the closest plastic if it exists
        if (minPlastic != null)
        {
            minPlastic.ReflectDirection();
            reflected = true;
            //_bossHealth.ReduceHealth(10);
        }

        return reflected;
    }
}
