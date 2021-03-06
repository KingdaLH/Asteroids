﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Singleton
    
    public static GameController instance;
    
    #endregion
    
    public GameObject asteroid;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;


    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increasewaves = 4;

    //Keycodes worden gekoppeld aan de acties van de speler en kunnen daarna door andere scripts gelezen worden.
    public KeyCode jump {get; set;}
    public KeyCode forward {get; set;}
    public KeyCode backward {get; set;}
    public KeyCode left {get; set;}
    public KeyCode right {get; set;}

    void Awake()
    {
        //Singleton pattern
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }	
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        /*Koppelt elke keycode als de game start
         * Laad data van PlayerPrefs zodat als de speler afsluit, instellingen worden bewaard
         */
        jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

    }
    
    // Loads the hiscore
    void Start()
    {
        hiscore = PlayerPrefs.GetInt("hiscore, 0");
        BeginGame();
    }

    // Sets all variables and text objects
    void BeginGame()
    {
        score = 0;
        lives = 3;
        wave = 1;

        //HUD
        scoreText.text = "SCORE:" + score;
        hiscoreText.text = "HISCORE:" + hiscore;
        livesText.text = "LIVES:" + lives;
        waveText.text = "WAVE:" + wave;

        SpawnAsteroids();
    }

    void SpawnAsteroids()
    {
        DestroyExistingAsteroids();
        //decide amount of asteroids to spawn
        //minus the leftovers from previous wave
        asteroidsRemaining = (wave * increasewaves);

        for (int i = 0; i < asteroidsRemaining; i++)
        {
            //spawn asteroid
            Instantiate(asteroid,
                new Vector3(Random.Range(-9.0f, 9.0f),
                    Random.Range(-6.0f, 6.0f), 0),
                Quaternion.Euler(0, 0, Random.Range(-0.0f, 359.0f)));
        }

        //update wave text
        waveText.text = "WAVE: " + wave;
    }

    public void IncreaseScore()
    {
        score++;

        scoreText.text = "SCORE:" + score;

        if (score > hiscore)
        {
            // Save the new hiscore
            PlayerPrefs.SetInt("hiscore", hiscore);
        }

        // All asteroids destroyed?
        if (asteroidsRemaining < 1)
        {
            // Start next wave
            wave++;
            SpawnAsteroids();
        }
    }

    public void DecreaseLives()
    {
        lives--;
        livesText.text = "LIVES: " + lives;
        
        // Has player run out of lives?
        if (lives < 1)
        {
            //restart the game
            BeginGame();
        }
    }

    public void DecreaseAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        // Two + asteroids
        // - big 
        // + 3 little
        // = 2
        asteroidsRemaining += 2;
    }

    // Finds the asteroid and destroys them
    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in asteroids)
        {
            Destroy(current);
        }

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in asteroids2)
        {
            Destroy(current);
        }
    }
}
