using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    public Player player;

    #region DEBUG DELETE

    public LayerMask groundMask;

    public GameObject raycastHitpoint;

    public CameraHandler camHandler;
    public Text uiText;
    public Text angleText;
    public Text playerAngleText;
    public GameObject dirMarker;
    public GameObject playerMarker;
    public GameObject boxItself;
    public GameObject boxPosMarker;
    public GameObject boxDirMarker;
    public Text differenceText;
    public RectTransform rt;
    public GameObject marker;
    public Vector2 boxPosition;
    #endregion

    #region Looking
    [Header("MouseLook")]
    public Camera cam;
    private Vector2 lookInputPosition;
    private Vector2 _playerScreenPos;

    public Transform playerTransform;
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

    #region New Input System
    [Header("New Input System")]
    public InputMaster controls;


    #endregion





    public Vector3 moveDirection;
    public Vector3 wantedDir;

    public Rigidbody rb;
    public CapsuleCollider col;

    #endregion

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Awake()
    {
        controls = new InputMaster();


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
        //Debug.Log(tempAngle);
        player.orientation = tempRotHandler.forward;
    }

    #endregion

    #region Methods
    
    
    
    
    
    
    /// <summary>
    /// checks horizontal and vertical input    
    /// </summary>
    private void MyInput()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = controls.Player.Movement.ReadValue<Vector2>().x;
        verticalInput = controls.Player.Movement.ReadValue<Vector2>().y;

        // lookInputPosition = cam.ScreenToWorldPoint(Input.mousePosition); //mouse position is a world point currently
        lookInputPosition = Input.mousePosition;//or this is 
        _playerScreenPos = cam.WorldToScreenPoint(rb.position);
        boxPosition = cam.WorldToScreenPoint(boxItself.transform.position);
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(lookInputPosition);
        
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            Debug.DrawRay(cam.transform.position, hitInfo.point);
            raycastHitpoint.transform.position = hitInfo.point;
            return (success: true, position: hitInfo.point);
        }
        else
        {
            Debug.LogWarning("Aiming Raycast failed");
            return (success: false, position: Vector3.zero);
        }
    }

    private void DoLookage() //THIS IS THE VERSION THAT LOOKS AROUND THE PLAYER!!! THIS ONE IS WAY BETTER
    {
        
        
        var (success, position) = GetMousePosition();
        if (success)
        {
            position.y = playerTransform.position.y;
            var direction = position - playerTransform.position;
            Debug.DrawRay(playerTransform.position, direction, Color.black);
            Debug.DrawRay(playerTransform.position + direction, playerTransform.up * 5f);
            //direction.y = tempRotHandler.forward.y;
            Debug.DrawRay(playerTransform.position, direction, Color.white);
            
           
            tempRotHandler.forward = direction;

        }

        #region old code (stinky)
        marker.transform.position = lookInputPosition;
        playerMarker.transform.position = _playerScreenPos;
        boxPosMarker.transform.position = boxPosition;

        Vector2 lookDirection = lookInputPosition - _playerScreenPos;
        Vector2 boxDirection = boxPosition - _playerScreenPos;




        float debugAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        float boxAngle = Mathf.Atan2(boxDirection.y, boxDirection.x) * Mathf.Rad2Deg - 90f;
        angleText.text = $"{Mathf.Round(debugAngle) + 90f}*";
        dirMarker.transform.rotation = Quaternion.AngleAxis(debugAngle, orientation.forward);
        boxDirMarker.transform.rotation = Quaternion.AngleAxis(boxAngle, orientation.forward);

        uiText.text = $"lookDirection x = {lookDirection.x}, lookDirection y = {lookDirection.y}";

        camHandler.PMgetter = cam.ScreenToViewportPoint(lookDirection); //DELETE THIS JAMES!!!!!!!!!!!!!!!!!!!!!!!!

        tempAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 135; //-135
        //tempAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 135; //-135


        //tempRotHandler.rotation = Quaternion.AngleAxis(-tempAngle, orientation.up);
        //Debug.DrawRay(tempRotHandler.position, tempRotHandler.forward, Color.yellow);
        playerAngleText.text = $"{Mathf.Round(tempRotHandler.rotation.eulerAngles.y)}*";
        playerAngleText.text = $"{Mathf.Round(boxAngle) + 90f}";
        differenceText.text = $"{Mathf.Abs((Mathf.Round(boxAngle) + 90f) - (Mathf.Round(debugAngle) + 90f))}";
        if (Mathf.Abs((Mathf.Round(boxAngle) + 90f) - (Mathf.Round(debugAngle) + 90f))> 20 && Mathf.Abs((Mathf.Round(boxAngle) + 90f) - (Mathf.Round(debugAngle) + 90f)) < 50)
        {
            Debug.Log("fuck");
            //Debug.Break();
        }
        #endregion

    }

    #region center dolookage // this sucks
    //private void DoLookage() //THIS IS THE VERSION THAT ROTATES AROUND THE CENTER!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //{
    //    Vector2 centerView = new Vector2(0.5f, 0.5f);
    //    Vector2 centerScreenSpace = cam.ViewportToScreenPoint(centerView);

    //    Vector2 lookDirection = lookInputPosition - centerScreenSpace;
    //    uiText.text = $"lookDirection x = {lookDirection.x}, lookDirection y = {lookDirection.y}";

    //    tempAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 135; //-45 is the magic number that makes this work but in opposite direction

    //    tempRotHandler.rotation = Quaternion.AngleAxis(-tempAngle, orientation.up);
    //    Debug.DrawRay(tempRotHandler.position, tempRotHandler.forward, Color.yellow);
    //}
    #endregion 
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); //offsets the input vector by 45degrees, for iso 
        //var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, 0));
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
