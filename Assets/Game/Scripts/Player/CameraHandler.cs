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
    public float horizontalInput;
    public float verticalInput;

    public float lerpVal;

    //public Camera cam;

    public Transform cam;


    public Vector3 toMove;
    #endregion

    #region Methods

    public void MoveCam()
    {
        #region mid
        //Debug.DrawRay(cam.position, cam.forward, Color.blue);
        //Debug.DrawRay(cam.position, cam.up, Color.green);
        //Debug.DrawRay(cam.position, cam.right, Color.red);
        #endregion




        toMove = cam.forward * verticalInput + cam.right * horizontalInput;

        Vector3 newMove = Quaternion.AngleAxis(-30, cam.right) * toMove;           //TODO: this might be performance destroying, optimize by creating one vector3/ angle set on start
        Debug.DrawRay(cam.position, newMove, Color.magenta, 5f);
        Debug.DrawRay(cam.position, toMove, Color.white, 5f);


        cam.transform.position = Vector3.Lerp(cam.transform.position,  newMove, lerpVal);

       
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
