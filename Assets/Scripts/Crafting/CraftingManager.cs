using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    private Part _part;
    private Item _item;

    public CraftingBible bible;
    public PartInstance partInstance;

    private void Start() {
        _part = null;
        _item = null;
    }    

    // Checks if the collided object is a part, if so, then it calls a function to handle the part pickup
    private void OnTriggerEnter2D(Collider2D collider) {
        PartInstance partInstance = collider.gameObject.GetComponent<PartInstance>();
        if (partInstance) {
            PickupDrop(partInstance.PartType);
        }
    }

    // Handles logic when the player picks up a part
    private void PickupDrop(Part part) {
        // Player not holding item
        if (!_item) {
            Debug.Log(part.displayName + " was picked up.");
            // Player not holding part
            if (!_part)
                _part = part;
            // Player crafting
            else
                CraftingHandler(_part, part);
        }
    }

    // Handles crafting logic
    private void CraftingHandler(Part part1, Part part2) {
        foreach (Item item in bible.recipes) {
            // Checks for the correct combination of materials
            if ((item.parts[0] == part1 & item.parts[1] == part2) || (item.parts[1] == part1 & item.parts[0] == part2)) {
                _part = null;
                _item = item;
                Debug.Log(item.displayName + " was crafted.");
                return;
            }
        }   
    }

    // Debugging script for random part spawning
    private void Update() {
        if (Input.GetKeyDown("u")) {
            partInstance.Spawn(transform.position);
        }
    }
}
