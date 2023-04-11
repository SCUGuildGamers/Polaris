using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCut : MonoBehaviour
{
    public SpriteRenderer background;
    public Sprite original;
    public Sprite cut;

    public void Cut()
    {
        if(background.sprite == original)
            background.sprite = cut;
        else
            background.sprite = original;
    }
}
