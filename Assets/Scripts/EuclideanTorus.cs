﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EuclideanTorus : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // teleport the game object to the other side
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }
        else if (transform.position.y > 6)
        {
            transform.position = new Vector3(transform.position.x, -6, 0);
        }
        else if (transform.position.y < -6)
        {
            transform.position = new Vector3(transform.position.x, 6, 0);
        }
    }
}