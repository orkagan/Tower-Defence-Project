using UnityEngine;
public class CameraHandler : MonoBehaviour
{
    #region Variables
    public Vector2 PMgetter;

    public Transform pivot; //This name is deceptive, it goes on pivot, not cam
    public Transform mouseCam;

    private float horizontalInput; //Input.GetAxis(horizontal)

    [Header("Multipliers")]
    public float horizontalMultiplier; //increases the amount the camera will move east and west

    private float verticalInput; //Input.GetAxis(Vertical)
    public float verticalMultiplier; //increases the amount the camera will move north and south

    [Header("Leaving/Returning")]
    public float leavingTime; //(seconds) how long does it take for the camera to move TOWARDS the desired location
    public float returningTime; //(seconds) how long does it take for the camera to RETURN from its location
    public float returningInputThreshold; //the input threshold for the camera acting as returning from its location 
    //^^^ (returningInputThreshold is scuffed, if needed James will come up with a better solution)

    private float smoothTime; //both of these are for Vector3.SmoothDamp
    private Vector3 velocity = Vector3.zero;

    public float smoothTimeMouse;
    private Vector3 velocityMouse = Vector3.zero;

    public float mouseStrength;
    
    private Vector3 toMove; //used in converting the horizontal and vertical inputs into horizontal and vertical camera movements
    [Space(20f)]
    public float strength; //how far the camera moves compared to the input
    #endregion
    #region Methods
    public void MoveCam()
    {
        //creates the vector3 we'll use to move the camera, based off the player's inputs
        toMove = pivot.forward * verticalInput * verticalMultiplier + pivot.right * horizontalInput * horizontalMultiplier;

        Vector3 mouseMove = pivot.forward * PMgetter.y + pivot.right * PMgetter.x;
        if (mouseMove.magnitude >=1)
        {
            mouseMove.Normalize();
        }

        Vector3 newMouseMove = Quaternion.AngleAxis(-30, pivot.right) * mouseMove;
       
        //Rotate the Vector3 toMove by 30 degrees on a local axis, to make it move vertically,
        //the amount of degrees should always be equal to the Pivot object's x axis, multiplied by -1.
        //This hardcode will cause bad things later, but I don't want to fix it. Too bad!
        Vector3 newMove = Quaternion.AngleAxis(-30, pivot.right) * toMove; 

        //Calculating whether the camera should be / is returning or leaving
        if (Mathf.Abs(verticalInput) <= returningInputThreshold && Mathf.Abs(horizontalInput) <= returningInputThreshold)
        {
            smoothTime = returningTime; //if under threshold, returning
        }
        else
        {
            smoothTime = leavingTime; //else is leaving
        }

        //moves the camera
        pivot.transform.localPosition = Vector3.SmoothDamp(pivot.transform.localPosition, (newMove * strength), ref velocity, smoothTime);
        mouseCam.transform.localPosition = Vector3.SmoothDamp(mouseCam.transform.localPosition, newMouseMove * mouseStrength , ref velocityMouse, smoothTimeMouse);
    }

    private void MyInput() //gathers player inputs, called every tick
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    #endregion
    #region Unity Methods
    public void Update()
    {
        MyInput();        
        MoveCam(); 
    }
    #endregion
}
