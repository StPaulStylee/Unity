using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // walking speed
    public float WalkingSpeed = 1f;
    public float JumpingSpeed = 1f;
    private Vector3 playerPosition;

    // Start is called before the first frame update
    private void Start( )
    {
        playerPosition = transform.position;
    }

    // Update is called once per frame
    private void Update( )
    {
        // Input on x axis (horizontal)
        float horizontalAxis = Input.GetAxis("Horizontal");
        // Input on the z axis (moving forward or backward)
        float verticalAxis = Input.GetAxis("Vertical");

        if (horizontalAxis == 0 && verticalAxis == 0)
        {
            return;
        } else if (horizontalAxis != 0)
        {
            float movement = getMovement(horizontalAxis);
            movePlayer(movement);
        } else if (verticalAxis != 0)
        {
            float movement = getMovement(verticalAxis);
            movePlayer(movement);
        }
    }

    private float getMovement(float axisValue)
    {
        return WalkingSpeed * Time.deltaTime * axisValue;
    }

    private void movePlayer(float movementValue)
    {
        // You've left off trying to mvoe the player on the horizontal or vertical axis.
        // You are stuck because your current logic doesn't account for two inputs at the same time
        // and also this movePlayer method doesn't know which vector to apply the movement to
    }
}
