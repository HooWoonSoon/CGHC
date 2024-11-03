using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;

    private PlayerInfo playerInfo;
    private List<IsSaveManager> saveManagers;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else 
            instance = this;
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        saveManagers = FindAllSaveManagers();

        LoadGame();
    }

    public void NewGame()
    {
        playerInfo = new PlayerInfo();
    }

    public void LoadGame()
    {
        playerInfo = dataHandler.Load();
        Debug.Log("Your file path: " + Application.persistentDataPath + "\n" + fileName);

        if (this.playerInfo == null)
        {
            Debug.Log("No saved data found");
            NewGame();
        }

        foreach (IsSaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(playerInfo);
        }

        Debug.Log("Load dead time" + playerInfo);
    }

    public void SaveGame()
    {
        foreach (IsSaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref playerInfo);
        }

        dataHandler.Save(playerInfo); 
        Debug.Log("Game was saved" + playerInfo);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IsSaveManager> FindAllSaveManagers()
    {
        IEnumerable<IsSaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<IsSaveManager>();

        return new List<IsSaveManager>(saveManagers);
    }
}
