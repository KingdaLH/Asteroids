using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    
    [SerializeField] float rotationSpeed = 100.0f;
    [SerializeField] float thrustForce = 3f;

    public AudioClip crash;
    public AudioClip shoot;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        // Reference the game controller and script
        GameObject gameControllerObject =
            GameObject.FindWithTag("GameController");
        
        gameController =
            gameControllerObject.GetComponent<GameController>();
    }

    private void FixedUpdate()
    {
        // Rotate the ship
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
        
        // Thrust the ship
        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Input.GetAxis("Vertical"));
        
        // Shoots bullet
        if (Input.GetButtonDown("Jump"))
        {
            ShootBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        // Anything except a bullet is an asteroid
        if (c.gameObject.tag != "Bullet")
        {
            AudioSource.PlayClipAtPoint(crash, Camera.main.transform.position);
            
            // Move the ship to the center of the screen
            transform.position = new Vector3(0, 0, 0);
            
            // Remove all velocity from the ship
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            
            gameController.DecreaseLives();
        }
    }

    private void ShootBullet()
    {
        // Spawn a bullet object from the prefabs
        Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0),
            transform.rotation);
        
        // Play a shoot sound
        AudioSource.PlayClipAtPoint(shoot, Camera.main.transform.position);
    }
}
