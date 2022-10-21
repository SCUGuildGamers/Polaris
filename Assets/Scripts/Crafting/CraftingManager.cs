using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private Drop _drop;
    private Craftable _item;

    public CraftingBible bible;

    private void Start() {
        _drop = null;
        _item = null;
    }    

    // Checks if the collided object is a drop, if so, then it handles the drop pickup
    private void OnTriggerEnter2D(Collider2D collider) {
        DropInstance dropInstance = collider.gameObject.GetComponent<DropInstance>();
        if (dropInstance) {
            PickupDrop(dropInstance.DropType);
        }
    }

    // Handles logic when the player picks up a drop
    private void PickupDrop(Drop drop) {
        // Player not holding item
        if (!_item) {
            Debug.Log(drop.displayName + " was picked up.");
            // Player not holding drop
            if (!_drop)
                _drop = drop;
            // Player crafting
            else
                CraftingHandler(_drop, drop);
        }
    }

    // Handles crafting logic; bug when the same drop is used to create an item
    private void CraftingHandler(Drop drop1, Drop drop2) {
        foreach (Craftable craftable in bible.recipes) {
            if (craftable.items.Contains(drop1) & craftable.items.Contains(drop2)) {
                _item = craftable;
                Debug.Log(craftable.displayName + " was crafted.");
            }
        }   
    }
}
