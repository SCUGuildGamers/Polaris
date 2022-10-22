using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartInstance : MonoBehaviour
{
    public Part PartType;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PartType.icon;
    }
}
