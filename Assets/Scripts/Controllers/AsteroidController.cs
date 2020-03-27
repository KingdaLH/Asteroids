using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public AudioClip destroy;
    public GameObject smallAsteroid;

    private GameController gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        // References the game manager object and and script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");

        gameController =
            gameControllerObject.GetComponent<GameController>();
        
        // Push the asteroid in the direction it is facing
        GetComponent<Rigidbody2D>()
            .AddForce(transform.up * Random.Range(-50.0f, 150.0f));
        
        // Give a random angular velocity/rotation
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag.Equals("Bullet"))
        {
            // If large asteroid spawn new ones
            if (tag.Equals("Large Asteroid"))
            {
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x -.5f,
                        transform.position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 90));
                
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + .5f,
                        transform.position.y + .0f, 0),
                    Quaternion.Euler(0, 0, 0));
                
                // Spawn small asteroids
                Instantiate(smallAsteroid,
                    new Vector3(transform.position.x + -.5f,
                        transform.position.y - .5f, 0),
                    Quaternion.Euler(0, 0, 270));
            }

            gameController.SplitAsteroid(); // +2
        }
        else
        {
            // Destroy just a small asteroid
            gameController.DecreaseAsteroids();
        }
        
        // Play a sound
        AudioSource.PlayClipAtPoint(
            destroy, Camera.main.transform.position);
        
        // Add to score
        gameController.IncreaseScore();
        
        // Destroy the current asteroid
        Destroy(gameObject);
    }
}
