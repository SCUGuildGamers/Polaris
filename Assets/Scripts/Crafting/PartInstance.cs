using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PartInstance : MonoBehaviour
{
    public Part PartType;
    public PartBible partBible;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PartType.icon;
    }
    
    // Spawns a random part instance
    public void Spawn(Vector3 spawnPosition) {
        GameObject partCopy = Instantiate(gameObject);
        partCopy.SetActive(true);

        // Assign position
        partCopy.GetComponent<Transform>().position = spawnPosition;

        // Assign the part a random part type
        PartInstance partInstance = partCopy.GetComponent<PartInstance>();
        Random rnd = new Random();
        int random_index = rnd.Next(partBible.parts.Count);
        partInstance.PartType = partBible.parts[random_index];
    }
}
