using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPellets : MonoBehaviour {

    public bool isHeld = false; //makes sure gun is being held.
    public bool testFire = false; //purely for debug reasons, auto-fires gun.

    private float cooldown = 0.8f; //fire rate of gun.
    public float pelletSpeed = 750.0f; //force the bullet is fired at

    public GameObject pelletSpawn; //empty GO where pellets appear.
    public GameObject Pellet; //Shotgun Shell. Instantiates when the gun is fired.
    public SteamVR_Controller.Device Controller; //the hand holding the gun.

    // Use this for initialization
    void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isHeld == true && Controller != null)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localPosition = new Vector3(0, 0.071f, 0.265f);

            if(cooldown == 0.8f && (Controller.GetHairTriggerDown() || Input.GetButtonDown("Fire1")))
            {
                GunShoot();
            }
        }

        //debug
        if (testFire == true && cooldown == 0.8f)
            GunShoot();

        //ensuring the cooldown works.
        if(cooldown < 0.8f)
        {
            cooldown += Time.deltaTime;
        }

        if(cooldown > 0.8f)
        {
            cooldown = 0.8f;
        }
	}

    void GunShoot()
    {
        Instantiate(Pellet, pelletSpawn.transform.position, transform.localRotation);
        Instantiate(Pellet, pelletSpawn.transform.position + new Vector3(0, .05f, 0), 
                    (transform.localRotation * Quaternion.Euler(-3, 0, 0)));
        Instantiate(Pellet, pelletSpawn.transform.position + new Vector3(0, -0.05f, 0),
                    (transform.localRotation * Quaternion.Euler(3, 0, 0)));
        Instantiate(Pellet, pelletSpawn.transform.position + new Vector3(0.05f, 0, 0),
                    (transform.localRotation * Quaternion.Euler(0, -3, 0)));
        Instantiate(Pellet, pelletSpawn.transform.position + new Vector3(-0.05f, 0, 0),
                    (transform.localRotation * Quaternion.Euler(0, 3, 0)));

        cooldown = 0;
    }
}
