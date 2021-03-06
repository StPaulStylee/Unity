﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // score of the player
    public int Score = 0;
    // High Score
    public int HighScore = 0;
    // Player Health
    public int Health;
    // Max Health
    public int MaxHealth = 3;
    //Current Level
    public int CurrentLevel = 1;
    // How many levels are there?
    public int TotalLevels = 2;

    // static instance of Game Manager that can be accessed anywhere
    public static GameManager Instance;
    // Our HUD Instance - remember, you could make this a Singleton in the HudManager class if desired
    //private HudManager hud;
    // Called when the script is loaded
    private void Awake( )
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        // Make sure that it is equal to the current object
        else if(Instance != this)
        {
            // Destroy the current game object because we only need one and we already have it
            Destroy(gameObject);
        }
        // Don't destroy the gameObject when scene is changed - this is done by default
        DontDestroyOnLoad(gameObject);
        //hud = FindObjectOfType<HudManager>( );
    }

    public void DecreaseHealth(int amount)
    {
        Health -= amount;
        HudManager.Instance.UpdateHealth( );
    }

    public void IncreaseScore(int amount)
    {
        // Increase the score by the amount
        Score += amount;
        HudManager.Instance.UpdateScore( );

        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }

    public void ResetGame( )
    {
        Score = 0;
        CurrentLevel = 1;
        Health = MaxHealth;
        // Load the CurrentLevel Scene
        SceneManager.LoadScene("Level1");
        HudManager.Instance.UpdateHUD( );
    }

    public void GoToNextLevel()
     {
        if (CurrentLevel < TotalLevels)
        {
            CurrentLevel++;
            SceneManager.LoadScene("Level" + CurrentLevel);
        }
        else
        {
            GameOver( );
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
