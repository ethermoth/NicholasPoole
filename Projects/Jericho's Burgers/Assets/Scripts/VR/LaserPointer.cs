using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab;

    private GameObject laser;
    private Vector3 hitPoint;

    private Material oldMat;
    private RaycastHit lastHit;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start()
    {
        laser = Instantiate(laserPrefab);
    }

    private void LateUpdate()
    {
        // grip buttons show the laser
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            RaycastHit hit;

            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                CheckObjectInteration(hit);
            }
            else
            {
                laser.SetActive(false);
            }

            // store the last hit object
            if (hit.transform)
                lastHit = hit;
        }
        else
        {
            laser.SetActive(false);

            // reset the last hit button state on focus lost
            if(lastHit.transform)
            {
                VRButton _oldButton = lastHit.transform.GetComponent<VRButton>();
                if (_oldButton)
                {
                    _oldButton.hovered = false;
                    _oldButton.pressed = false;
                }
            }
        }
    }

    /// <summary>
    /// Shows the laser.
    /// </summary>
    /// <param name="hit">Hit to stop laser at.</param>
    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        laser.transform.LookAt(hitPoint);
        laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y,
            hit.distance);
    }

    /// <summary>
    /// Takes a hit and sees what to do with it.
    /// </summary>
    /// <param name="hit">Hopefully a VR-usable object.</param>
    private void CheckObjectInteration(RaycastHit hit)
    {
        VRButton _button = hit.transform.GetComponent<VRButton>();
        VRButton _oldButton = null;

        // see if the last object hit was a vr button
        if (lastHit.transform)
        {
            _oldButton = lastHit.transform.GetComponent<VRButton>();

            // reset it if it was
            if (_oldButton)
                _oldButton.hovered = false;
        }

        // handle events for laser buttons
        if (_button)
        {
            if (_button.laserActivated)
            {
                _button.hovered = true;

                if (Controller.GetHairTriggerDown())
                {
                    if (_oldButton)
                    {
                        _oldButton.LaserEvent();
                    }
                }

                if(Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
                {
                    if(_oldButton)
                        _oldButton.pressed = true;
                }
                else
                {
                    if (_oldButton)
                        _oldButton.pressed = false;
                }
            }
        }
    }
}
