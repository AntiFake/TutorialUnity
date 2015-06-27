using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private int level = 3;                                  //Current level number, expressed in game as "Day 1".

    //Awake is always called before any Start functions
    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        Debug.Log(GetComponent<BoardManager>() == null ? "1" : "2");
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        boardScript.SetupScene(level);
    }

    //Update is called every frame.
    void Update()
    {

    }
}