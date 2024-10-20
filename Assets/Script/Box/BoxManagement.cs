using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManagement : MonoBehaviour
{
    [SerializeField] private List<BoxController> boxs;
    void Start()
    {
        for (int i = 0; i < boxs.Count; i++)
        {
            BoxData.instance.AddBox(boxs[i].canPush);
            boxs[i].SetBoxIndex(i);
        }
    }

    public int GetBoxIndex(BoxController boxController)
    {
        for (int i = 0; i < boxs.Count; i++)
        {
            if (boxController == boxs[i]) return i;
        }
        return -1;
    }

}
