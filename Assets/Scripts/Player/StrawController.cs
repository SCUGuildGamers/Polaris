using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawController : MonoBehaviour
{
    // For reference
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference
        _animator = GetComponent<Animator>();
    }

    // Performs swing animation
    public void SwingAnimation() {
        // Set the trigger
        _animator.ResetTrigger("isSwinging");
        _animator.SetTrigger("isSwinging");
    }
}
