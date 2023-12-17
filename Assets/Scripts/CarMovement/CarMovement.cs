using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CarMovement : MonoBehaviour
{
    // Scripts
    private ManageScene manageSceneScript;
    
    // Player input
    [SerializeField] private string driveInputName;
    [SerializeField] private string turnInputName;
    private float _driveInput;
    private float _turnInput;

    // Rotating vehicle
    [SerializeField] private AnimationCurve turnCurve; // Turn radius
    [SerializeField] private float turnSpeed;
    
    // Cameras
    [SerializeField] private List<Camera> cameras;
    private float cameraTime;
    
    // Object component
    private Rigidbody _rb;

    // Vehicle movement
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float forwardTopSpeed;
    [SerializeField] private float reverseTopSpeed;
    private float _topSpeed;
    private bool isGrounded;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
        // Initialises scene object script
        GameObject sceneObject = GameObject.Find("SceneManager");
        if (sceneObject != null)
            manageSceneScript = sceneObject.GetComponent<ManageScene>();
        
        EnableCamera(0);
    }

    private void Update()
    {
        float maxGroundAngle = 90f;
        
        //Checks if car is touching the ground
        if (Physics.Raycast(transform.position + Vector3.up * 0.05f, Vector3.down, out RaycastHit hit, 1f))
        {
            // Checks if car is upside-down
            float groundAngle = Vector3.Angle(transform.up, hit.normal);
            if (groundAngle >= maxGroundAngle)
            {
                isGrounded = false;
                
                // Resets scene if car is flipped
                manageSceneScript.SetResetVariables(true, "You flipped you car.");
            }
            else // Car is not flipped, and is grounded
            {
                isGrounded = true;
            }
        }
        else // Car is not on the ground
        {
            isGrounded = false;
        }

        // Changes camera depending on button press
        // if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //     EnableCamera(0);
        // else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //     EnableCamera(1);
    }

    private void FixedUpdate()
    {
        // If car is touching ground we can move it
        if (isGrounded && !manageSceneScript.disablePlayerInput)
            VehicleMovement();
    }

    // Enables selected camera and disables the others
    private void EnableCamera(int i)
    {
        foreach (var cam in cameras)
        {
            cam.enabled = false;
        }

        cameras[i].enabled = true;
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
        _topSpeed = forwardTopSpeed;
        EnableCamera(0);
        if (!movingForward)
        {
            _topSpeed = reverseTopSpeed;
            
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                cameraTime += Time.deltaTime;
                
                if (cameraTime >= 0.5f)
                    EnableCamera(1);
            }
            else
            {
                cameraTime = 0;
            }
        }

        // Once the top speed is reached the vehicle cannot go faster
        //if (_rb.velocity.magnitude >= _topSpeed && _driveInput > 0)
        if (_rb.velocity.magnitude >= _topSpeed)
            movement = Vector3.zero;
        
        // Accelerates or decelerates vehicle
        _rb.AddForce(movement * acceleration);
        
        HandleAsymetricFriction();
    }

    // Makes sure vehicle cannot go sideways
    private void HandleAsymetricFriction()
    {
        Vector3 v = Vector3.Project(_rb.velocity, transform.right);
        _rb.AddForce(-v * 10f);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Checks if other is of tag AI
        // This is done to make sure game does restart when player collides with AI car
        if (other.collider.CompareTag("AI"))
        {
            manageSceneScript.SetResetVariables(true, "You crashed with another car.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Makes sure only triggers with road objects
        if (other.CompareTag("Road") || other.CompareTag("Parked"))
        {
            manageSceneScript.SetResetVariables(true, "You crashed the car.");
        }
    }
}
