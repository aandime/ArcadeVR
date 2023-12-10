using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

public class DartboardMover : MonoBehaviour
{
    public float speed = 1f; // Movement speed
    public float range = 2f; // Range of movement from the starting position

    private float minX; // Minimum x-axis value
    private float maxX; // Maximum x-axis value
    private float direction = 1f; // Current movement direction

    void Start()
    {
        // Set the movement bounds based on the current position and range
        float startX = transform.position.x;
        minX = startX - range;
        maxX = startX + range;
    }

    void Update()
    {
        // Calculate new position
        float newX = transform.position.x + direction * speed * Time.deltaTime;

        // Check if the new position is out of bounds
        if (newX > maxX || newX < minX)
        {
            direction *= -1f; // Change direction
        }

        // Apply the position within the bounds
        transform.position = new Vector3(
            Mathf.Clamp(newX, minX, maxX), 
            transform.position.y, 
            transform.position.z);
    }
}
