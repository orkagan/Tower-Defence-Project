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
       




        toMove = cam.forward * verticalInput + cam.right * horizontalInput;

        Vector3 newMove = Quaternion.AngleAxis(-30, cam.right) * toMove;
        Debug.DrawRay(cam.position, newMove, Color.magenta, 5f);


       
        Debug.DrawRay(cam.position, toMove, Color.white, 5f);
        Debug.DrawRay(cam.position, cam.forward, Color.blue);
        Debug.DrawRay(cam.position, cam.up, Color.green);

        //var matrix = Matrix4x4.Rotate(Quaternion.Euler(30, 0, 0)); //offsets the input vector by 45degrees, for iso 
        //var isoForward = matrix.MultiplyPoint3x4(cam.forward); //this is in world space, not local

        //Debug.DrawRay(cam.position, isoForward, Color.cyan);

        Debug.DrawRay(cam.position, cam.right, Color.red);

        //cam.localPosition = cam.localPosition + toMove;


       
        //Debug.DrawRay(cam.position, isoMove, Color.red, 5f);

        //cam.localPosition = cam.localPosition + isoMove;

        //float camPosX = cam.transform.position.x;
        //float camPosY = cam.transform.position.y;

        //Vector3 lerpVector = new Vector3(Mathf.Lerp(camPosX, horizontalInput, lerpVal), camPosY, cam.transform.position.z);
        //Debug.DrawRay(cam.transform.position, lerpVector);



        //Vector3 rotLerpVector = new Vector3(Mathf.Lerp(camPosX, horizontalInput, lerpVal), camPosY, cam.transform.position.z) + cam.transform.forward;
        //Debug.DrawRay(cam.transform.position, rotLerpVector, Color.red);

        #region iso rotation 

        //var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)); //offsets the input vector by 45degrees, for iso

        //var isoLerpVector = matrix.MultiplyPoint3x4(lerpVector);

        //Debug.DrawRay(cam.transform.position, isoLerpVector, Color.red);
        #endregion
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
