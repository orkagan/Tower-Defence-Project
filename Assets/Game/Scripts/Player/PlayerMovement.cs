using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    public bool mobileMovement;
    public Player player;
    public InputMaster controls;
    public LayerMask groundMask;
    public GameObject raycastHitpoint;
    public CameraHandler camHandler;

    private Plane groundPlane;
    private Plane projectilePlane;

    #region Looking
    [Header("MouseLook")]
    public Camera cam;
    public Transform playerTransform;
    public Transform rotHandler;

    private Vector2 lookInputPosition;
    private Vector2 lookInputPositionMobile;
    private Vector2 _playerScreenPos;
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
        controls = new InputMaster();

        accelAmount = (50 * acceleration) / maxSpeed;
        decelAmount = (50 * deceleration) / maxSpeed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        orientation = GetComponent<Transform>();
        rb.freezeRotation = true;
        groundPlane = new Plane(Vector3.up, Vector3.zero);
        projectilePlane = new Plane(Vector3.up, rb.position);
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (mobileMovement == false)
        {
            DoLookage();
        }

        else 
        {
            DoLookageMobile();
        }
        

        
        player.orientation = rotHandler.forward;
    }
    #endregion
    #region Methods   
    private void MyInput()
    {
        horizontalInput = controls.Player.Movement.ReadValue<Vector2>().x;     
        verticalInput = controls.Player.Movement.ReadValue<Vector2>().y;
       
        lookInputPosition = controls.Player.AimPosition.ReadValue<Vector2>();
        lookInputPositionMobile = controls.Player.AimPositionMobile.ReadValue<Vector2>();
        _playerScreenPos = cam.WorldToScreenPoint(rb.position);        
    }

    private void DoLookageMobile()
    {


        Vector2 lookDirection = lookInputPositionMobile;


        float tempAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 135; //-45 is the magic number that makes this work but in opposite direction

        rotHandler.rotation = Quaternion.AngleAxis(-tempAngle, orientation.up);

        Vector2 lookDirection1 = lookInputPosition - _playerScreenPos;

        camHandler.PMgetter = cam.ScreenToViewportPoint(lookDirection1);
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = cam.ScreenPointToRay(lookInputPosition);

        if (projectilePlane.Raycast(ray, out var hitInfo))
        {
            Vector3 hitPoint = ray.GetPoint(hitInfo);
            raycastHitpoint.transform.position = hitPoint;
            return (success: true, position: hitPoint);
        }
        else
        {
            Debug.LogWarning("Aiming Raycast failed");
            return (success: false, position: Vector3.zero);
        }      
    }

    private void DoLookage() 
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            position.y = playerTransform.position.y;
            var direction = position - playerTransform.position;
            //Debug.DrawRay(playerTransform.position, direction, Color.black);
            //Debug.DrawRay(playerTransform.position + direction, playerTransform.up * 5f);
            
            //Debug.DrawRay(playerTransform.position, direction, Color.white);

            rotHandler.forward = direction;
        }

        Vector2 lookDirection = lookInputPosition - _playerScreenPos;
      
        camHandler.PMgetter = cam.ScreenToViewportPoint(lookDirection); 
    }
    
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); //offsets the input vector by 45degrees, for iso 
       
        var skewedMoveDirection = matrix.MultiplyPoint3x4(moveDirection.normalized);

        wantedDir = skewedMoveDirection * maxSpeed;

        Vector3 velocityDifference = wantedDir - rb.velocity;

        if (wantedDir.magnitude > 0.1f)
        {
            accelRate = accelAmount;
        }
        else if (wantedDir.magnitude < 0.1f)
        {
            accelRate = decelAmount;
        }

        rb.AddForce(velocityDifference * accelRate, ForceMode.Acceleration);

        Vector3 singleAddVelocity = new Vector3(rb.velocity.x + (Time.fixedDeltaTime * velocityDifference.x * accelRate), rb.velocity.y, rb.velocity.z + (Time.fixedDeltaTime * velocityDifference.z * accelRate));
        
        if (singleAddVelocity.magnitude > maxSpeed)
        {
            Debug.LogWarning("Speed added from accel is higher than max speed, tell James");
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