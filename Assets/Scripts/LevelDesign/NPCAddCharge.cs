using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAddCharge : MonoBehaviour
{
    public GlideCharge glideCharge;
    private bool _gained;

    // Start is called before the first frame update
    void Start()
    {
        glideCharge = FindObjectOfType<GlideCharge>();
        _gained = false;
    }

    public void GainCharge()
    {
        if (!_gained)
        {
            glideCharge.AddCharge();
            _gained = true;
        }
    }
}
