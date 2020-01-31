using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // public variables
    public float speed;
    
    // private variables
    GameObject player;
    SceneController sceneController;


    void Start()
    {
        // set player to the GameObject with the tag "mainCamera"
        player = Camera.main.gameObject;

        // add a reference to the sceneController component. Finds a GameObject in the scene named "ScriptHolder" and looks for the attached "SceneController" component script
        sceneController = GameObject.Find("ScriptHolder").GetComponent<SceneController>();
    }

    void Update()
    {
        // step = amount we want the enemy to step every frame
        float step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);

        // if the enemy reaches the player
        if(Vector3.Distance(transform.position, player.transform.position) < 0.01)
        {
            Kill();
            sceneController.GameOver();
        }
    }

    // delete the enemy GameObject
    public void Kill()
    {
        Destroy(gameObject);
    }
}
