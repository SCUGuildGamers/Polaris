using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CraftingBible")]
public class CraftingBible : ScriptableObject
{
    public List<Item> recipes;
}
