using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance : MonoBehaviour
{
    public Item _Item;

    public GameObject player;

    private bool used;

    public float speed;

    private Vector3 TargetDir;

    private int DistanceTravelled = 0;

    void Update (){
        if (!used){
            if (Input.GetMouseButtonDown(1)) {
                used = true;
                TargetDir = GetMouseDirection();
            } else {
                transform.position = player.transform.position;
            }
        } else if (_Item.displayName == "Item1"){
            Shoot(TargetDir);
            if (DistanceTravelled > 100){
                Destroy(gameObject);
            }
            DistanceTravelled++;
        }
    }

    private void Start()
    {
        transform.position = player.transform.position;
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _Item.icon;
        used = false;
        TargetDir = new Vector3(0,0,0);
    }

    public void Spawn(Item item) {
        GameObject ItemInstance = Instantiate(gameObject);
        ItemInstance.gameObject.SetActive(true);

        ItemInstance instance = ItemInstance.GetComponent<ItemInstance>();
        instance._Item = item;
    }

    private void Shoot(Vector3 TargetDir)
    {  
        transform.position += (TargetDir * speed);
    }

    private Vector3 GetMouseDirection(){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;
        Debug.Log((worldPosition - player.transform.position).normalized);
    
        return (worldPosition - player.transform.position).normalized;
    }


    private void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "BossPlaceholder")
        {
            Destroy(gameObject);
        }
    }
}
