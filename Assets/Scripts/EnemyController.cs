using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Speed of the enemy
    public float Speed = 3f;

    public float SpeedBoostFactor = 1.2f;

    // Range of movement
    public float RangeY = 2f;

    private Vector3 initialPosition;

    private int direction = 1;

    void Start()
    {
        // Save inital position on start
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // How much are we moving?
        float movementY = getMovement( );

        // new position y
        float currentY= transform.position.y + movementY;
         
        // Check whether or not we have left our range
        // We use absolute value here because our rangeY (range of movement) will always be a positive value
        // but our object will have negative values that fall beyond the our desired range.
        if(Mathf.Abs(currentY - initialPosition.y) > RangeY)
        {
            direction *= -1;
        } else
        {
            // If we are within our range then we create a new Vector3 and only move it along the X Axis
            transform.position += new Vector3(0, movementY, 0);
        }
    }

    private float getMovement( )
    {
        if (direction == 1)
        {
            return Speed * Time.deltaTime * direction;
        } else
        {
            return SpeedBoostFactor * Speed * Time.deltaTime * direction;
        }
    }
}
