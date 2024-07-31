using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    public float acceleration = 500f;
    public float brakingForce = 300f;
    public float groundCheckDistance = 0.5f; // Distance to check for ground

    private float currentAcceleration = 0f;
    private float currentBrakeForce = 0f;

    private bool isGrounded;

    private void FixedUpdate()
    {
        CheckGrounded();

        if (isGrounded)
        {
            // Get vertical input axis (make sure it's correctly set in Input settings)
            currentAcceleration = acceleration * Input.GetAxis("Vertical");

            // Set brake force based on whether space is pressed
            if (Input.GetKey(KeyCode.Space))
                currentBrakeForce = brakingForce;
            else
                currentBrakeForce = 0f;

            // Apply motor torque
            frontRight.motorTorque = currentAcceleration;
            frontLeft.motorTorque = currentAcceleration;

            // Apply brake torque
            frontRight.brakeTorque = currentBrakeForce;
            frontLeft.brakeTorque = currentBrakeForce;
            backRight.brakeTorque = currentBrakeForce;
            backLeft.brakeTorque = currentBrakeForce;
        }
        else
        {
            // Optional: Set motor torque and brake torque to zero when not grounded
            frontRight.motorTorque = 0f;
            frontLeft.motorTorque = 0f;
            frontRight.brakeTorque = 0f;
            frontLeft.brakeTorque = 0f;
            backRight.brakeTorque = 0f;
            backLeft.brakeTorque = 0f;
        }
    }

    private void CheckGrounded()
    {
        // Cast a ray from the center of the car downwards to check for ground
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance);

        // Optionally, visualize the raycast in the editor
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
