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

    public void AddBox(bool canPush, Vector2 generatedPoint, int index)
    {
        var newBox = new BoxInfo(canPush, generatedPoint, index);
        boxList.Add(newBox);
    }

    //public void UpdateDistance(float distance, int index)
    //{
    //    boxList[index].Distance(distance, index);
    //}

    public Vector2 GetRepawnPosition(int index)
    {
        return boxList[index].RepawnPoint;
    }

    public void CheckList()
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            Debug.Log(boxList[i].ToString());
        }
    }
}
