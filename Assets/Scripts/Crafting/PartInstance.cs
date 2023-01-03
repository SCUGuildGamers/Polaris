using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PartInstance : MonoBehaviour
{
    public Part PartType;
    public PartBible partBible;
    public CraftingManager craftingManager;

    private Vector3 TargetDirection;

    private float _speed = 0.004f;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = PartType.icon;
    }

    private void Update(){
        transform.position += TargetDirection * _speed;
    }
    
    // Spawns a random part instance
    public void Spawn(Vector3 spawnPosition, Vector3 targetDirection) {
        GameObject partCopy = Instantiate(gameObject);
        partCopy.SetActive(true);

        // Assign position
        partCopy.GetComponent<Transform>().position = spawnPosition;

        Vector3 target = targetDirection.normalized;

        // Assign the part a random part type
        PartInstance partInstance = partCopy.GetComponent<PartInstance>();
        Random rnd = new Random();
        do{
            int random_index = rnd.Next(partBible.parts.Count);
            partInstance.PartType = partBible.parts[random_index];
        } while (partInstance.PartType == craftingManager.get_part());
        partInstance.TargetDirection = target;
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "PlayerPlaceholder")
        {
            Destroy(gameObject);
        }
    }
}
