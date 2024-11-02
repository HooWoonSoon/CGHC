using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour, IsSaveManager
{
    public static GameManager instance;
    public PlayerInfo character {  get; private set; }
    private Player player;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    void Start()
    {
        player = PlayerManager.instance.player;
        character = new PlayerInfo(player.transform.position);
        Debug.Log(character.ToString());
    }

    public void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        character.Checkpoint(newCheckpoint);
        Debug.Log(character.ToString());
    }

    public void UpdateDead()
    {
        character.Dead();
        //player.transform.position = character.lastCheckpoint;
    }

    public void LoadData(PlayerInfo _data)
    {
        character = _data;
    }

    public void SaveData(ref PlayerInfo _data)
    {
        _data = character;
    }
}
