using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInstance : MonoBehaviour
{
    public Drop DropType;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = DropType.icon;
    }
}
