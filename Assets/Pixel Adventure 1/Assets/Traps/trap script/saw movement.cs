using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of the saw blade movement
    public float moveRange = 3f; // Range of movement left and right
    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the saw blade
        startPosition = transform.position;
    }

    void Update()
    {
        // Get input from the horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new position based on input and speed
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Clamp the position to stay within the defined range
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - moveRange, startPosition.x + moveRange);

        // Update the position of the saw blade
        transform.position = newPosition;
    }
}