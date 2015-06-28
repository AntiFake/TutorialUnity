using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                  //Current level number, expressed in game as "Day 1".
    public static GameManager instance;
    public int playerFoodPoints;
    [HideInInspector]
    public bool playersTurn = true;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(instance);

        DontDestroyOnLoad(instance);
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    public void GameOver() 
    {
        this.enabled = false;
    }

    //Update is called every frame.
    void Update()
    {

    }
}