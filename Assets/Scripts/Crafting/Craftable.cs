using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craftable")]
public class Craftable : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;


    public Drop item1;
    public Drop item2;
}
