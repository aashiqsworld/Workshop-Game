using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    // public variables
    public GameObject enemyPrefab;
    public GameObject gameOverScreen;
    public int score;
    public int spawnRate;

    // private variables
    GameObject player;
    float counter;

    void Start()
    {
        // make sure the game over screen is deactivated
        gameOverScreen.SetActive(false);

        // set the player to the GameObject with the tag "mainCamera"
        player = Camera.main.gameObject;
    }

    void Update()
    {
        // if counter is higher than the current spawn rate, spawn an enemy and set it back to 0 and 
        if(counter > spawnRate)
        {
            SpawnEnemy();
            counter = 0f;
        }

        // if the user touches the screen, call Shoot()
        if(Input.touchCount > 0)
        {
            Shoot();
        }

        counter += Time.deltaTime;
    }


    void SpawnEnemy()
    {
        // generate a random spawn position on the outside of a sphere of radius 5
        Vector3 spawnPosition = Random.onUnitSphere * 5;

        // halve the possible x, y, and z values to make it easier for the player to find the enemies.
        if (spawnPosition.x < player.transform.position.x)
        {
            spawnPosition.x *= -1f;
        }

        if (spawnPosition.y < player.transform.position.y)
        {
            spawnPosition.y *= -1f;
        }

        if (spawnPosition.z < player.transform.position.z)
        {
            spawnPosition.z *= -1f;
        }


        // instantiate the enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void Shoot()
    {
        RaycastHit hit;

        // if the raycast hits a Gameobject, assume it is an enemy and Kill() it
        if(Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out hit, 6))
        {
            GameObject killedEnemy = hit.collider.gameObject;
            killedEnemy.GetComponent<EnemyController>().Kill();
        }
    }

    public void IncrementScore()
    {
        score++;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }


    // called by the Try Again button on the game over screen
    public void TryAgain()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        gameOverScreen.SetActive(false);
        score = 0;
        spawnRate = 5;

    }
}
