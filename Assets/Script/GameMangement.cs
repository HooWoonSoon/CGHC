using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangement : MonoBehaviour
{
    public PlayerInfo character {  get; private set; }
    private Player player;
    void Start()
    {
        player = FindAnyObjectByType<Player>();
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
        player.transform.position = character.lastCheckpoint;
    }
}
