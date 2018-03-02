using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public Camera selfRef;
    public Camera otherCamRef;
    public GameObject reticle;
    public float mSpeed = 2.0f;//The speed the camera will move.

    //The camera's rotation.
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public float xLeftLim;
    public float xRightLim;

	// Use this for initialization
	void Awake () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        xLeftLim = otherCamRef.transform.rotation.eulerAngles.y - 75;
        xRightLim = otherCamRef.transform.rotation.eulerAngles.y + 75;
    }
	
	// Update is called once per frame
	void Update () {

        {
            //Debug.Log(otherCamRef.transform.rotation.eulerAngles);
            xLeftLim = otherCamRef.gameObject.transform.rotation.eulerAngles.y - 75;
            xRightLim = otherCamRef.gameObject.transform.rotation.eulerAngles.y + 75;
        }

        if (selfRef.enabled == true)
        {
            reticle.SetActive(true);

            if (Input.GetButtonUp("Cancel"))
            {//purely for debug stuff
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }

            xRotation += mSpeed * Input.GetAxis("Mouse X");
            
            yRotation -= mSpeed * Input.GetAxis("Mouse Y");
            //yRotation -= otherCamRef.transform.rotation.x;

            //the camera rotates according to the movement of the player's mouse.
            if (yRotation <= 70 && yRotation >= -70)
            {
                if (xRotation <= xRightLim && xRotation >= xLeftLim)
                    transform.eulerAngles = new Vector3(yRotation, xRotation, 0.0f);

                else if (xRotation > xRightLim)
                {
                    transform.eulerAngles = new Vector3(yRotation, xRightLim, 0.0f);
                    xRotation = xRightLim;
                }

                else if (xRotation < xLeftLim)
                {
                    transform.eulerAngles = new Vector3(yRotation, xLeftLim, 0.0f);
                    xRotation = xLeftLim;
                }
            }

            //The two statements below cap the vertical rotation of the camera.
            else if (yRotation > 70)
            {
                if (xRotation <= xRightLim && xRotation >= xLeftLim)
                {
                    transform.eulerAngles = new Vector3(70, xRotation, 0.0f);
                    yRotation = 70;
                }

                else if (xRotation > xRightLim)
                {
                    transform.eulerAngles = new Vector3(70, xRightLim, 0.0f);
                    yRotation = 70;
                    xRotation = xRightLim;
                }

                else if (xRotation < xLeftLim)
                {
                    transform.eulerAngles = new Vector3(70, xLeftLim, 0.0f);
                    yRotation = 70;
                    xRotation = xLeftLim;
                }
            }

            else if (yRotation < -70)
            {
               
                if (xRotation <= xRightLim && xRotation >= xLeftLim)
                {
                    transform.eulerAngles = new Vector3(-70, xRotation, 0.0f);
                    yRotation = -70;
                }

                else if (xRotation > xRightLim)
                {
                    transform.eulerAngles = new Vector3(-70, xRightLim, 0.0f);
                    xRotation = xRightLim;
                    yRotation = -70;
                }

                else if (xRotation < xLeftLim)
                {
                    transform.eulerAngles = new Vector3(-70, xLeftLim, 0.0f);
                    xRotation = xLeftLim;
                    yRotation = -70;
                }
            }
        }
        else
        {
            reticle.SetActive(false);

        }
    }
}
