using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class PlayerInfo
{
    public Vector2 lastCheckpoint;
    public SerializedDictionary<string, bool> skill;
    public float gameDuration;
    public int playerDeadCount;
   
    public PlayerInfo()
    {
        lastCheckpoint = new Vector2(41.27f, -6.12f);
        gameDuration = 0;
        playerDeadCount = 0;
        skill = new SerializedDictionary<string, bool>();
    }

    public void Dead()
    {
        playerDeadCount++;
        PlayerManager.instance.playerDeadCount = playerDeadCount;
    }

    public void Timer(float elspeTime)
    {
        gameDuration = elspeTime;
    }

    public void Checkpoint(Vector2 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
        PlayerManager.instance.lastCheckPoint = lastCheckpoint;
    }

    public override string ToString()
    {
        return $"lastCheckpoint: {lastCheckpoint} Game duration {gameDuration} Player Dead Count {playerDeadCount}";
    }
}
