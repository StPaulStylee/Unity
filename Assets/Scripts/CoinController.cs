using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float RotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Angle of rotation; v = d / t --> d = v * t
        // Here we are setting the angle for the rotation for this update call by multiplying
        // our rotation speed by the amount of time that has passed since the last update call
        // This will ensure that our rotation speeds are constant between all CPUs
        float angleRotation = RotationSpeed * Time.deltaTime;

        // rotate the coin
        // The Rotate Method takes a Vector3 and a Scoping param - the scoping
        // param is defaulted to local but it can be changed to global... This is a GOTCHA
        // for many newbies so beware that this can happen
        transform.Rotate(Vector3.up * angleRotation, Space.World);
    }
}
