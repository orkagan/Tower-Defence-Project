using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    #region DEBUG DELETE

    public Text uiText;

    #endregion

    #region Looking
    [Header("MouseLook")]
    public Camera cam;
    private Vector2 _mousePosition;
    private Vector2 _playerScreenPos;

    public Transform tempRotHandler; //DELETE THIS ASAP
    public float tempAngle; //ALSO DELETE THIS ASAP

    #endregion

    #region Moving
    private float accelRate; //The multiplier used on the final vector
    [Space(20f)]
    [Header("Acceleration")]
    [Tooltip("DO NOT SET HIGHER THAN MAXSPEED OR LOWER THAN 1, GAME WILL BREAK")]
    public float acceleration;
    [Tooltip("DO NOT SET HIGHER THAN MAXSPEED OR LOWER THAN 1, GAME WILL BREAK")]
    public float deceleration;
    
    private float accelAmount;
    private float decelAmount;
    
    public float maxSpeed; //maximum speed
    


    public Transform orientation; //tbh i don't even remember what this does but it's set to the capsule and it works so
    public float horizontalInput; //input system for A and D keys
    public float verticalInput; //INput system for W and S keys
    
    public Vector3 moveDirection;
    public Vector3 wantedDir;

    public Rigidbody rb;
    public CapsuleCollider col;

    #endregion

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
        orientation = GetComponent<Transform>();
        rb.freezeRotation = true;
    }

    private void Update()
    {

        MyInput();
        SpeedControl();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        DoLookage();
        Debug.Log(tempAngle);
    }

    #endregion

    #region Methods
    /// <summary>
    /// checks horizontal and vertical input    
    /// </summary>
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // _mousePosition = cam.ScreenToWorldPoint(Input.mousePosition); //mouse position is a world point currently
        _mousePosition = Input.mousePosition; 
        _playerScreenPos = cam.WorldToScreenPoint(rb.position);
    }

    private void DoLookage() //for the love of god please change this name later james
    {
        

        Vector2 lookDirection = _mousePosition - _playerScreenPos;
        uiText.text = $"lookDirection x = {lookDirection.x}, lookDirection y = {lookDirection.y}";

        tempAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        tempRotHandler.rotation = Quaternion.AngleAxis(tempAngle, orientation.up);
        Debug.DrawRay(tempRotHandler.position, tempRotHandler.forward, Color.yellow);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; 

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); //offsets the input vector by 45degrees, for iso 
        var skewedMoveDirection = matrix.MultiplyPoint3x4(moveDirection.normalized);

        wantedDir = skewedMoveDirection * maxSpeed;

        Vector3 velocityDifference = wantedDir - rb.velocity;
        
        #region debug rays
        //Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 1, rb.position.z), skewedMoveDirection.normalized, Color.white, 0.1f);
        //Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 2, rb.position.z), wantedDir, Color.red, 0.1f);
        //Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 3, rb.position.z), velocityDifference, Color.blue, 0.1f);
        //Debug.DrawRay( new Vector3(rb.position.x, rb.position.y + 4, rb.position.z), rb.velocity, Color.black, 0.1f);
        #endregion
        
        

        if (wantedDir.magnitude > 0.1f)
        {
            accelRate = accelAmount;
        }
        else if (wantedDir.magnitude < 0.1f)
        { 
        accelRate = decelAmount;
        }

     
        rb.AddForce(velocityDifference * accelRate, ForceMode.Acceleration);

        Vector3 singleAddVelocity = new Vector3 (rb.velocity.x + (Time.fixedDeltaTime * velocityDifference.x * accelRate), rb.velocity.y, rb.velocity.z + (Time.fixedDeltaTime * velocityDifference.z * accelRate));

        //Debug.Log("Speed added from accel in one tick " + singleAddVelocity.magnitude);
        if (singleAddVelocity.magnitude > maxSpeed)
        {
            Debug.LogWarning("Speed added from accel is higher than max speed, CATASTROPHIC, CALL JAME");
        }
        
        
       
    }

    /// <summary>
    /// Handles max and min speed
    /// </summary>
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
    #endregion 

}
