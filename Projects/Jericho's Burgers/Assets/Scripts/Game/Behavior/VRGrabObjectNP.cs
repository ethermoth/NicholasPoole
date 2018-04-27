using UnityEngine;

public class VRGrabObjectNP : MonoBehaviour
{
    // fields
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;

    //gun logic
    public bool usingGun = false;

    /// <summary>
    /// Steam VR Setup
    /// </summary>
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        // init the tracked object
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        // grab objects when the player clicks the trigger down
        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
                if(objectInHand.GetComponent<ShootPellets>())
                {
                    objectInHand.GetComponent<ShootPellets>().isHeld = true;
                    objectInHand.GetComponent<ShootPellets>().Controller = Controller;
                    usingGun = true;
                }
            }
        }

        // release objects when the player lets go of the trigger
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand == true && usingGun == false)
            {
                //objectInHand.GetComponent<ShootPellets>().isHeld = false;
                //objectInHand.GetComponent<ShootPellets>().Controller = null;
                ReleaseObject();
            }
        }

        // release the gun
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip) || Input.GetKeyDown(KeyCode.E))
        {
            if (objectInHand == true && usingGun == true)
            {//Tells gun to not fire anymore, and releases object.
                objectInHand.GetComponent<ShootPellets>().isHeld = false;
                objectInHand.GetComponent<ShootPellets>().Controller = null;
                ReleaseObject();
            }
        }
    }

    /// <summary>
    /// Sets the colliding object up for VR controllers.
    /// </summary>
    /// <param name="other">Collider of the other object.</param>
    private void SetCollidingObject(Collider other)
    {
        if (collidingObject || !other.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = other.gameObject;
    }

    /// <summary>
    /// Called when the VR controller enters another object.
    /// </summary>
    /// <param name="other">Collider of the object it entered.</param>
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    /// <summary>
    /// Calls same logic as OnTiggerEnter but on a stay event in case of dropped frames.
    /// </summary>
    /// <param name="other">Collider of the object it entered.</param>
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    /// <summary>
    /// Called when the VR controller is egressing the object it was overlapping.
    /// </summary>
    /// <param name="other">Collider of the object it is leaving.</param>
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    /// <summary>
    /// Adds a fixed joint to the VR controller.
    /// </summary>
    /// <returns>A fixed joint for attaching a gameObject.</returns>
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    /// <summary>
    /// Grabs an object in the scene by attaching it to the VR controller.
    /// </summary>
    private void GrabObject()
    {
        if (collidingObject.layer == (int)GameManager.VRLayer.Grab)
        {
            objectInHand = collidingObject;
            collidingObject = null;

            FixedJoint joint = AddFixedJoint();
            joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
        }
    }

    /// <summary>
    /// Drop logic for objects that were attached to the VR controller.
    /// </summary>
    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }
}
