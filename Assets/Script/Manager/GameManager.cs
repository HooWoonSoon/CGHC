using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour, IsSaveManager
{
    public static GameManager instance;
    public PlayerInfo character {  get; private set; }
    private List<BoxController> boxs = new List<BoxController>();
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
        character = new PlayerInfo();
        Debug.Log(character.ToString());


        foreach (BoxController box in Resources.FindObjectsOfTypeAll<BoxController>())
        {
            if (box != null && !boxs.Contains(box))
            {
                int index = boxs.Count;
                boxs.Add(box);

                BoxData.instance.AddBox(box.canPush, box.transform.position, index, box.orientation);
                box.SetBoxIndex(index);

            }
        }
        //BoxData.instance.CheckList();
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
