using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is meant to tell the game how the player should move. When Mouse1 (Fire1) is held,
//the player's speed increases from 0 to a max of 15 over the course of 3 seconds.

public class CartMotion : MonoBehaviour {

    public Camera PersonalCam;    //the game's camera.
    public GameObject CameraNode; //the location for the camera

    public float Speed = 0;
    public float MaxSpeed = 20;

    public Vector3 velocity; //the speed the player is currently moving.
    public float currentSpeed;

    public float angularVelo;

    public bool isReady = false; //needs to be true to allow movement at all.
    public bool goLeft = false; //checks if the 'A' key is held.
    public bool goRight = false; //checks if the 'D' key is held.
    public bool brakes = false; //checks if the 'S' key is held.

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if(CameraNode == null)
        {
            CameraNode = GameObject.Find("CameraNode");
        }

        if(PersonalCam == null)
        {
            PersonalCam = Camera.current;
        }

        PersonalCam.transform.position = CameraNode.transform.position;
        PersonalCam.transform.rotation = CameraNode.transform.rotation;

        velocity = GetComponent<Rigidbody>().velocity;
        currentSpeed = Mathf.Abs(GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.z);
        angularVelo = GetComponent<Rigidbody>().angularVelocity.y;

        if (isReady == true)
        {
            //transform.Translate(0, 0, 1 * Speed * Time.deltaTime);
            if (currentSpeed < 60)
            {
                GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * Speed);
            }

            PersonalCam.fieldOfView = 60 + (0.5f * Speed);
        }

        if(transform.rotation.x < -0.1)
        {
            transform.Rotate(60 * Time.deltaTime, 0, 0);
           
        }

        if(transform.rotation.x > 0.1)
        {
            transform.Rotate(-60 * Time.deltaTime, 0, 0);
        }

		if(Input.GetButton("Fire1"))
        {
            if(Speed < MaxSpeed)
            {
                Speed += 8 * Time.deltaTime;
            }
        }

        else
        {
            if(Speed > 0)
            {
                Speed -= 5.0f * Time.deltaTime;
            }

            if (Mathf.Abs(Speed) <= 0.05)
            {
                Speed = 0;
            }
        }

        if(goLeft == true)
        {
            transform.Rotate(0, -60 * Time.deltaTime, 0);
            GetComponent<Rigidbody>().AddRelativeTorque(-1, 0, 0);
        }

        if(goRight == true)
        {
            transform.Rotate(0, 60 * Time.deltaTime, 0);
            GetComponent<Rigidbody>().AddRelativeTorque(1, 0, 0);
        }

        if(brakes == true)
        {
            //slows down the player and reduces spinning. More or less required to handle properly.
            GetComponent<Rigidbody>().angularVelocity *= 0.6f;

            if (Speed > 0)
            {
                Speed -= 15.0f * Time.deltaTime;
                GetComponent<Rigidbody>().velocity *= 0.95f;
            }
        }


        if (Input.GetButton("Left") && goRight == false)
        {
            goLeft = true;
        }
        else goLeft = false;

        if (Input.GetButton("Right") && goLeft == false)
        {
            goRight = true;
        }
        else goRight = false;

        if (Input.GetButton("Back"))
        {
            brakes = true;
        }
        else brakes = false;
    }

    public void SpinOut()
    {
        float timer = 0;
        timer += Time.deltaTime;
        if (timer < 2)
        {
            isReady = false;
            GetComponent<Rigidbody>().angularVelocity += new Vector3(0, 30, 0);
        }
        else isReady = true;
    }
}
