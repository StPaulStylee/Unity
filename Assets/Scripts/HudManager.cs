using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Note since this doesn't really need to persist across all scenes this doesn't need to be a Singleton,
// it very easily could be if you decided you wanted to go that route - we are because going into
// a route where you try and use a new instance in every scene is a mess
public class HudManager : MonoBehaviour
{
    // Score text label
    public Text ScoreLabel;

    public static HudManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHud( );
    }

    private void Awake( )
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
        }
        // Make sure that it is equal to the current object
        else if (Instance != this)
        {
            // Destroy the current game object because we only need one and we already have it
            Destroy(gameObject);
        }
        // Don't destroy the gameObject when scene is changed - this is done by default
        DontDestroyOnLoad(gameObject);
    }

    // Show up to date stats of the player
    public void UpdateHud()
    {
        ScoreLabel.text = "Score: " + GameManager.Instance.Score;
    }
}
