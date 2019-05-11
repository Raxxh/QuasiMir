using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StartScreen,
    Tutorial,
    Running
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState = GameState.StartScreen;

    public GameManager()
    {
        Debug.Assert(Instance == null);
        Instance = this;
    }

    public void Update()
    {
        if (GameState == GameState.StartScreen && Input.anyKey)
            GameState = GameState.Tutorial;
    }
}
