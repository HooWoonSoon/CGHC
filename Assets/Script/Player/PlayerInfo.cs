using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public Vector2 lastCheckpoint { get; private set; }
    public float gameDuration { get; private set; }
    public int playerDeadCount { get; private set; }
   
    public PlayerInfo(Vector2 checkpoint)
    {
        lastCheckpoint = checkpoint;
        gameDuration = 0;
        playerDeadCount = 0;
    }

    public void Dead()
    {
        playerDeadCount++;
    }

    public void Timer(float elspeTime)
    {
        gameDuration = elspeTime;
    }

    public void Checkpoint(Vector2 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }

    public override string ToString()
    {
        return $"lastCheckpoint: {lastCheckpoint} Game duration {gameDuration} Player Dead Count {playerDeadCount}";
    }
}
