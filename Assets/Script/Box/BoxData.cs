using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxData 
{
    public static BoxData instance;
    public List<BoxInfo> boxList;

    static BoxData()
    {
        instance = new BoxData();
    }

    public BoxData()
    {
        boxList = new List<BoxInfo>();
    }

    public void AddBox(bool canPush)
    {
        var newBox = new BoxInfo(canPush);
        boxList.Add(newBox);
    }

    public void CheckList()
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            Debug.Log(boxList[i].ToString());
        }
    }
}
