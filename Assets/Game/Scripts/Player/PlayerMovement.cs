using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{


    #region Variables

    public Vector3 playerVelocity;
    public float walkSpeed;
    public float moveSpeed;
    public float groundDrag;
    public Transform orientation;
    public float horizontalInput;
    public float verticalInput;
    public Vector3 moveDirection;
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
        Debug.DrawRay(rb.position, moveDirection);
        Debug.DrawRay(rb.position, orientation.forward * verticalInput, Color.blue);
        Debug.DrawRay(rb.position, orientation.right * horizontalInput, Color.red);

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        var skewedMoveDirection = matrix.MultiplyPoint3x4(moveDirection);

        rb.AddForce(skewedMoveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z); //horizontal velocity
        if (flatVel.magnitude > moveSpeed)
        {
            Debug.Log("Speedcheck: limited velocity");
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void StateHandler()
    {
        //empty for now

    }

}
