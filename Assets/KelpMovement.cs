using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelpMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _facingRight = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
