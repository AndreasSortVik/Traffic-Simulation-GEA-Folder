using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using Vector3 = UnityEngine.Vector3;

public class CarMovement : MonoBehaviour
{
    // Player input
    [SerializeField] private string driveInputName;
    [SerializeField] private string turnInputName;
    private float _driveInput;
    private float _turnInput;

    // Rotating vehicle
    [SerializeField] private AnimationCurve turnCurve; // Turn radius
    [SerializeField] private float turnSpeed;
    
    // Player component
    private Rigidbody _rb;

    // Vehicle movement
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    private float topSpeed;
    [SerializeField] private float forwardTopSpeed;
    [SerializeField] private float reverseTopSpeed;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        VehicleMovement();
    }

    private void VehicleMovement()
    {
        // Gets input values
        _driveInput = Input.GetAxis(driveInputName);
        _turnInput = Input.GetAxis(turnInputName);

        Vector3 forward = transform.forward;
        Vector3 movement = forward * _driveInput;
        
        // Checks if vehicle is moving forwards
        bool movingForward = Vector3.Angle(_rb.velocity, forward) <= 90;
        
        // Checks if player is braking
        if (_driveInput < 0 && movingForward)
            movement *= deceleration;

        // Rotates car proportional to car velocity
        float y = turnCurve.Evaluate(_rb.velocity.magnitude);
        
        // If movingForward is true it sets direction to 1, else -1
        float direction = movingForward ? 1f : -1f;
        
        // Turning vehicle
        transform.transform.Rotate(Vector3.up, _turnInput * y * direction * Time.fixedDeltaTime * turnSpeed);
        
        // Changing the top speed depending on if car is driving forwards or reversing
        topSpeed = forwardTopSpeed;
        if (!movingForward)
            topSpeed = reverseTopSpeed;
        
        // Once the top speed is reached the vehicle cannot go faster
        if (_rb.velocity.magnitude >= topSpeed && _driveInput > 0)
            movement = Vector3.zero;
        
        // Accelerates or decelerates vehicle
        _rb.AddForce(movement * acceleration);
        
        HandleAsymetricFriction();
    }

    // Makes Sure vehicle cannot go sideways
    private void HandleAsymetricFriction()
    {
        Vector3 v = Vector3.Project(_rb.velocity, transform.right);
        _rb.AddForce(-v * 10f);
    }
}
