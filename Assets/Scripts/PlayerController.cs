using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float WalkingSpeed = 1f;
    public float JumpingForce = 1f;
    public int MaxiumDamage = 3;
    public float CameraDistanceZ = 6f;

    public AudioSource CoinSound;

    private int CurrentDamage = 0;
    private float horizontalAxis;
    private float verticalAxis;

    // Create the Rigidbody component
    private new Rigidbody rigidbody;
    // Create the Collider Component
    private new Collider collider;
    private bool isJumping = false;
    private Vector3 playerSize;

    // Start is called before the first frame update
    private void Start( )
    {
        // Set the Rigidbody Component
        rigidbody = GetComponent<Rigidbody>( );
        // Set the Collider Component
        collider = GetComponent<Collider>( );
        // Get the size of our player
        playerSize = collider.bounds.size;
        // Set the Camera Position
        CameraFollowPlayer( );
    }

    // Update is called once per frame
    private void FixedUpdate( )
    {
        WalkHandler( );
        JumpHandler( );
        CameraFollowPlayer( );
        if(!IsOnMap())
        {
            GameManager.Instance.GameOver( );
        }
    }

    private void CameraFollowPlayer( )
    {
        // Grab the player object - remember that using Camera.main can be dangerous as if the tag of the camera
        // is changed this 'get' may not work or return you the wrong camera
        Vector3 cameraPosition = Camera.main.transform.position;
        // modify it's position according to the CameraDistanceZ by getting the player's z position 
        // and putting the camera behind them
        cameraPosition.z = transform.position.z - CameraDistanceZ;
        // Set the camera position
        Camera.main.transform.position = cameraPosition;
    }


    private bool IsGrounded( )
    {
        // Calculate the location of all 4 bottom corners
        // We add the '0.01f to the Y value so that the Raycaster doesn't get confused... it needs a little bit of room to work with
        // when running
        Vector3 corner1 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner3 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);

        // Check if the player is grounded
        bool ground1 = Physics.Raycast(corner1, -Vector3.up, 0.02f);
        bool ground2 = Physics.Raycast(corner2, -Vector3.up, 0.02f);
        bool ground3 = Physics.Raycast(corner3, -Vector3.up, 0.02f);
        bool ground4 = Physics.Raycast(corner4, -Vector3.up, 0.02f);

        // If any of these are positive, it will return true
        return (ground1 || ground2 || ground3 || ground4);
    }

    private bool IsOnMap()
    {
        return transform.position.y > -50;
    }

    private Vector3 GetMovement()
    {
        // Input on x axis (horizontal)
        horizontalAxis = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal: " + horizontalAxis);
        // Input on the z axis (moving forward or backward)
        verticalAxis = Input.GetAxis("Vertical");
        Debug.Log("Vertical: " + verticalAxis);
        // Movement Vector
        Vector3 movement = new Vector3(GetMovementValueForVector(horizontalAxis), 0, GetMovementValueForVector(verticalAxis));
        return movement;
    }

    private float GetMovementValueForVector(float axisValue)
    {
        return axisValue * WalkingSpeed * Time.deltaTime;
    }

    private void JumpHandler()
    {
        // Input on the Jump (Y) axis
        float jumpAxis = Input.GetAxis("Jump");
        // Once jump is pressed
        if (jumpAxis > 0)
        {
            bool isGrounded = IsGrounded( );
            // Check if we're not already jumping
            if(!isJumping && isGrounded)
            {
                isJumping = true;
                // jumping vector
                Vector3 jumpVector = new Vector3(0, JumpingForce * jumpAxis, 0);
                // Apply force
                rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        } else
        {
            isJumping = false;
        }
    }

    // The collider of the other object which this game object is touching
    private void OnTriggerEnter(Collider other)
    {
        // Two way's you can do this using tags... The second is a little more efficient
        //if (other.tag == "Coin") ;
        if(other.CompareTag("Coin"))
        {
            // Increase the score
            GameManager.Instance.IncreaseScore(1);
            // Play the coin sound
            CoinSound.Play( );

            // Destory the coin
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Enemy"))
        {
            CurrentDamage++;
            GameManager.Instance.DecreaseHealth(1);
            if (CurrentDamage >= MaxiumDamage)
            {
                GameManager.Instance.GameOver( );
            }
        }
        else if(other.CompareTag("Goal"))
        {
            GameManager.Instance.GoToNextLevel( );
        }
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
        // Logic for facing player in direction of walk
        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            // Option 1: Modify the transform - but this is typically bad because we don't want to 
            // directly mutate the transform of a Rigid Body (unless it's a kinematic rigid body, which it isn't)
            Debug.Log(horizontalAxis);
            Debug.Log(verticalAxis);
            Vector3 direction = new Vector3(horizontalAxis, 0, verticalAxis);
            //Debug.Log(direction);
            //transform.forward = direction;

            // Option 2
            rigidbody.rotation = Quaternion.LookRotation(direction);
        }
    }
}
