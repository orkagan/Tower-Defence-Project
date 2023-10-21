using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraHandler : MonoBehaviour
{
    #region pseudocode
    /*
   tween the camera in the direction the player is moving, when holding the input keys
    variables include: 
    the tween speed (i.e how fast it takes to get to max)
    distance: how far the camera moves in the given direction


     */
    #endregion

    #region Variables
    private float horizontalInput;
    public float horizontalMultiplier;

    private float verticalInput;
    public float verticalMultiplier;

    public float leavingTime;
    public float returningTime;
    public float returningInputThreshold; //doesnt matter for PC

    private float smoothTime;
    private Vector3 velocity = Vector3.zero;
    
    public Transform cam;

    private Vector3 toMove;
    public float strength;
    #endregion

    #region Methods

    public void MoveCam()
    {
        #region mid
        //Debug.DrawRay(cam.position, cam.forward, Color.blue);
        //Debug.DrawRay(cam.position, cam.up, Color.green);
        //Debug.DrawRay(cam.position, cam.right, Color.red);
        #endregion

        //how do i figure out if it's returning or leaving???


        toMove = cam.forward * verticalInput * verticalMultiplier + cam.right * horizontalInput * horizontalMultiplier;

        Vector3 newMove = Quaternion.AngleAxis(-30, cam.right) * toMove;           //TODO: this might be performance destroying, optimize by creating one vector3/ angle set on start
        Debug.DrawRay(cam.position, newMove, Color.magenta, 5f);
        Debug.DrawRay(cam.position, toMove, Color.white, 5f);


        if (Mathf.Abs(verticalInput) <= returningInputThreshold && Mathf.Abs(horizontalInput) <= returningInputThreshold)
        {
            smoothTime = returningTime;
        }
        else
        {
            smoothTime = leavingTime;
        }

        //cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, newMove, lerpRate);
        cam.transform.localPosition = Vector3.SmoothDamp(cam.transform.localPosition, newMove * strength, ref velocity, smoothTime);

       
    }


    private void MyInput()
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
