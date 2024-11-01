using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfo
{
    protected bool canPush;
    protected float distance;
    protected int index;
    protected Vector2 generatedPoint;
    public BoxInfo(bool _canPush, Vector2 _generatedPoint, int _index)
    {
        canPush = _canPush;
        generatedPoint = _generatedPoint;
        index = _index;
    }

    //public void Distance(float _distance, int Index)
    //{
    //    distance = _distance;
    //    index = Index;
    //}
    public Vector2 RepawnPoint
    {
        get { return generatedPoint; }
    }

    public override string ToString()
    {
        return $"BoxType{canPush}, Generated Point{generatedPoint}, Index{index}";
    }
}
