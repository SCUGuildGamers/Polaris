using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCollisionHandler 
{
    void CollisionEnter(string colliderName, GameObject other);
}
