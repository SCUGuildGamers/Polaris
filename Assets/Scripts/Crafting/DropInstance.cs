using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropInstance : MonoBehaviour
{
    public Drop dropType;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = dropType.icon;
    }
}
