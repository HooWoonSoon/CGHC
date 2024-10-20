using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfo
{
    protected bool canPush;
    public BoxInfo(bool _canPush)
    {
        canPush = _canPush;
    }

    public override string ToString()
    {
        return $"BoxType{canPush}";
    }
}
