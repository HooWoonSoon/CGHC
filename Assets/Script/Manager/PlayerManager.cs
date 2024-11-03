using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IsSaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int playerDeadCount;
    public Vector2 lastCheckPoint;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    public void LoadData(PlayerInfo _data)
    {
        playerDeadCount = _data.playerDeadCount;
        lastCheckPoint = _data.lastCheckpoint;
    }

    public void SaveData(ref PlayerInfo _data)
    {
        _data.playerDeadCount = this.playerDeadCount;
        _data.lastCheckpoint = this.lastCheckPoint;
    }

}
