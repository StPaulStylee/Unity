using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float WalkingSpeed = 1f;
    public float JumpingSpeed = 1f;
    // Create the Rigidbody component
    private new Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Start( )
    {
        // Set the Rigidbody Component
        rigidbody = GetComponent<Rigidbody>( );
    }

    // Update is called once per frame
    private void FixedUpdate( )
    {
        WalkHandler( );
    }

    private Vector3 GetMovement()
    {
        // Input on x axis (horizontal)
        float horizontalAxis = Input.GetAxis("Horizontal");
        // Input on the z axis (moving forward or backward)
        float verticalAxis = Input.GetAxis("Vertical");
        // Movement Vector
        Vector3 movement = new Vector3(GetMovementValueForVector(horizontalAxis), 0, GetMovementValueForVector(verticalAxis));
        return movement;
    }

    private float GetMovementValueForVector(float axisValue)
    {
        return axisValue * WalkingSpeed * Time.deltaTime;
    }

    private void Jump()
    {
        // ToDo: Implement
    }

    private void Walk(Vector3 movement, Rigidbody rigidbody)
    {
        // Calculate the new position
        Vector3 newPosition = transform.position + movement;
        // Move the player via the rigidbody
        rigidbody.MovePosition(newPosition);
    }

    private void WalkHandler( )
    {
        Vector3 movement = GetMovement( );
        Walk(movement, rigidbody);
    }
}
