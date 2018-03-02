using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    private bool isPlayerDead = false;
    [SerializeField] public GameObject deathCanvas;
    public GameObject deathSound;

    public void OnCollisionEnter(Collision aCol)
    {
        if(aCol.transform.tag == "Player")
        {
            if(aCol.relativeVelocity.magnitude > 10)
            {
                // Bring up the loss screen for the player
                deathCanvas.SetActive(true);
                deathSound.GetComponent<AudioSource>().Play();
                // Disable player movement
                aCol.gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = false;
                aCol.gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = false;
               // aCol.gameObject.GetComponent<AudioSource>().enabled = false;
            }
        }
    }
}
