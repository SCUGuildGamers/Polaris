using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{
    private float _swingRange = 2f;

    private BossHealth _bossHealth;

    void Start()
    {
        _bossHealth = FindObjectOfType<BossHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Swing();
    }

    public void Swing()
    {
        Plastic[] plasticList = FindObjectsOfType<Plastic>();

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
            Destroy(minPlastic.gameObject);
            _bossHealth.ReduceHealth(10);
        }
        
    }
}
