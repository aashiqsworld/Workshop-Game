using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlaneController : MonoBehaviour
{
    // public variables
    public GameObject cylinder;

    // private variables
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        // set player to the GameObject with the tag "mainCamera"
        player = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // if # touches on screen > 0
        if (Input.touchCount > 0)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        
        RaycastHit hit;

        // shoot raycast 6 meters in front of the camera
        if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out hit, 6))
        {
            // if the raycast hits a collider tagged "plane", instantiate a cylinder
            if(hit.collider.tag == "plane")
            {
                Instantiate(cylinder, hit.point, Quaternion.identity);
            }
        }
    }
}
