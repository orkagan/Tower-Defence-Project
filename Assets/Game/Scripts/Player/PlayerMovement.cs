using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{


    #region Variables

    public Vector3 playerVelocity;

    public float accelRate;
    public float acceleration;
    public float deceleration;
    private float accelAmount;
    private float decelAmount;
    
    public float maxSpeed;
    


    public Transform orientation;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;
    public Vector3 wantedDir;

    public Rigidbody rb;
    public CapsuleCollider col;


    public enum MovementState
    {
        freeze,
        walking

    }

    public MovementState movementState;





    #endregion

    #region Unity Methods


    private void Awake()
    {
        accelAmount = (50 * acceleration) / maxSpeed;
        decelAmount = (50 * deceleration) / maxSpeed;

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {

        MyInput();
        SpeedControl();
        StateHandler();


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #endregion


    /// <summary>
    /// checks horizontal and vertical input    
    /// </summary>
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; //this is magic to me

        //moveDirection is the desired input


        //Debug.DrawRay(rb.position, moveDirection);
        //Debug.DrawRay(rb.position, orientation.forward * verticalInput, Color.blue);
        //Debug.DrawRay(rb.position, orientation.right * horizontalInput, Color.red);

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        var skewedMoveDirection = matrix.MultiplyPoint3x4(moveDirection.normalized);
        //var skewedMoveDirection = moveDirection;

        wantedDir = skewedMoveDirection * maxSpeed;

        Vector3 velocityDifference = wantedDir - rb.velocity;
        Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 1, rb.position.z), skewedMoveDirection.normalized, Color.white, 0.1f);
        Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 2, rb.position.z), wantedDir, Color.red, 0.1f);
        Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 3, rb.position.z), velocityDifference, Color.blue, 0.1f);
        Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 4, rb.position.z), rb.velocity, Color.black, 0.1f);

        if (velocityDifference.magnitude > rb.velocity.magnitude + 10)
        {
            Debug.Log("Difference Check. Not sure if this is useful.");
        }

        if (wantedDir.magnitude > 0.1f)
        {
            accelRate = accelAmount;
        }
        else if (wantedDir.magnitude < 0.1f)
        { 
        accelRate = decelAmount;
        }

       // Debug.Log("veldif: " + velocityDifference * accelRate);
        rb.AddForce(velocityDifference * accelRate, ForceMode.Acceleration);

        Vector3 grungus = new Vector3 (rb.velocity.x + (Time.fixedDeltaTime * velocityDifference.x * accelRate), rb.velocity.y, rb.velocity.z + (Time.fixedDeltaTime * velocityDifference.z * accelRate));

        Debug.Log("Speed added from accel in one tick " + grungus.magnitude);
        if (grungus.magnitude > maxSpeed)
        {
            Debug.LogWarning("Speed added from accel is higher than max speed, CATASTROPHIC, CALL JAME");
        }
        
        
        //if (velocityDifference.magnitude * accelRate > maxSpeed)
        //{
        //    Debug.Log("CATASTROPHIC?::: ADDED FORCE GREATER THAN MAXSPEED, SHIT YOUR PANTS?");
        //}

        //Debug.Log("veldif: " + velocityDifference * 10f * accelRate);
        //rb.AddForce(velocityDifference * 10f * accelRate, ForceMode.Acceleration);
        //rb.AddForce(skewedMoveDirection.normalized * 10f * accelRate, ForceMode.Acceleration);
        //rb.AddForce(skewedMoveDirection.normalized * 10f, ForceMode.Acceleration);

        //Debug.Log("total magnitude of added force is: " + )
    }

    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //horizontal velocity
        if (flatVel.magnitude > maxSpeed)
        {
            Debug.Log("Speedcheck: limited velocity");
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        if (flatVel.magnitude < 0.01f)
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void StateHandler()
    {
        //empty for now

    }

}
