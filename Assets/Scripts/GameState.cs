using UnityEngine;

public class GameState : Singleton<GameState>
{
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
            if (killCount == 1 && currentState == State.Playing)
            {
                currentState = State.Win;
            }
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
}
