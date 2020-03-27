using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
            //Bullet destroys itself after 1 seconds
            Destroy (gameObject, 1.0f);
 
            // Push the bullet in the direction it is facing
            GetComponent<Rigidbody2D>()
                .AddForce(transform.up * 400);
    }
}
