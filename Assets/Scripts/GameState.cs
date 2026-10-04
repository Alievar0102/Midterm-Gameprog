using System.Collections.Generic;
using UnityEngine;

public class GameState : Singleton<GameState>
{
    public List<GameObject> spawnedObjects = new List<GameObject>();
    private int killCount;
    private bool dead = false;

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

    public bool Dead
    {
        get
        {
            return dead;
        }
        set
        {
            dead = value;
            currentState = State.Lose;
        }
    }

    public enum State
    {
        Playing, Paused, Win, Lose
    }

    public State currentState;


    public bool IsPlaying()
    {
        return currentState == State.Playing;
    }

    public bool IsGameOver()
    {
        return currentState == State.Win || currentState == State.Lose;
    }

    public void AddListSpawn(GameObject target)
    {
        spawnedObjects.Add(target);
    }
}
