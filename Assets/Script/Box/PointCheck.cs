using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCheck : MonoBehaviour
{
    public Vector3 wayMove { get; private set; }
    void Start()
    {
        
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = mousePosition - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
                wayMove = Vector3.right;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                wayMove = Vector3.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                wayMove = Vector3.up;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                wayMove = Vector3.down;
            }
        }
    }
}
