using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfo
{
    protected bool canPush;
    protected float distance;
    protected int index;
    public BoxInfo(bool _canPush)
    {
        canPush = _canPush;
    }

    //public void Distance(float _distance, int Index)
    //{
    //    distance = _distance;
    //    index = Index;
    //}

    public override string ToString()
    {
        return $"BoxType{canPush}";
    }
}
