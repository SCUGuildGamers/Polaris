using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera thisCamera;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        player = GameObject.Find("PlayerPlaceholder");
        thisCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        thisCamera.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);   
    }
}
