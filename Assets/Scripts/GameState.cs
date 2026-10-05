using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState>
{
    public List<GameObject> spawnedObjects = new List<GameObject>();
    private int killCount;

    AudioManager audioManager;

    public int KillCount
    {
        get
        {
            return killCount;
        }
        set
        {
            killCount = value;
            /*if (killCount == 1 && currentState == State.Playing)
            {
                currentState = State.Win;
            }*/
        }
    }

    //public bool Dead
    //{
    //    get
    //    {
    //        return dead;
    //    }
    //    set
    //    {
    //        dead = value;
    //        currentState = State.Lose;
    //    }
    //}

    public enum State
    {
        Playing, Paused, Win, Lose, MainMenu
    }

    public State currentState;


    public bool IsPlaying()
    {
        return currentState == State.Playing;
    }

    //public bool IsGameOver()
    //{
    //    return currentState == State.Win || currentState == State.Lose;
    //}

    public bool HasNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        return currentIndex + 1 < SceneManager.sceneCountInBuildSettings;
    }

    public void LevelStart()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        GameState.Instance.KillCount = 0;
        GameState.Instance.currentState = GameState.State.Playing;
        Time.timeScale = 1;

        GameSettings.Instance.PlayCombatMusic(audioManager.combatMusic);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GamePause()
    {
        GameState.Instance.currentState = GameState.State.Paused;
        Time.timeScale = 0;
    }

    public void GameResume() // after game over, restart
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        GameSettings.Instance.PlayCombatMusic(audioManager.combatMusic);
        GameState.Instance.currentState = GameState.State.Playing;
        Time.timeScale = 1;
    }

    public void GameWin()
    {
        GameState.Instance.currentState = GameState.State.Win;
        Time.timeScale = 0;
    }

    public void GameLose()
    {
        GameState.Instance.currentState = GameState.State.Lose;
        Time.timeScale = 0;
    }

    public void GameContinue()
    {
        if (HasNextLevel())
        {
            LevelStart();
        }
        else ReturnToMainMenu();
    }

    void ReturnToMainMenu()
    {
        GameState.Instance.currentState = GameState.State.MainMenu;
        Time.timeScale = 0;

        SceneManager.LoadScene(0);
    }

    public void AddListSpawn(GameObject target)
    {
        spawnedObjects.Add(target);
    }
}
