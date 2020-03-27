using System.Collections;
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


    // Loads the hiscore
    void Start()
    {
        hiscore = PlayerPrefs.GetInt("hiscore, 0");
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Stops when the player presses escape
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    // Sets all variables and text objects
    void BeginGame()
    {
        score = 0;
        lives = 4;
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
