using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfo
{
    protected bool canPush;
    protected float distance;
    protected int index;
    protected Vector2 generatedPoint;
    protected Vector3 spawnsOrientation;
    public BoxInfo(bool _canPush, Vector2 _generatedPoint, int _index, Vector3 _spawnsOrientation)
    {
        canPush = _canPush;
        generatedPoint = _generatedPoint;
        index = _index;
        spawnsOrientation = _spawnsOrientation;
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

    public Vector3 RepawnOrientation
    {
        get { return spawnsOrientation; }
    }

    public override string ToString()
    {
        return $"BoxType{canPush}, Generated Point{generatedPoint}, Index{index}";
    }
}
