using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IsSaveManager 
{
    void LoadData(PlayerInfo _data);
    void SaveData(ref PlayerInfo _data);
}
