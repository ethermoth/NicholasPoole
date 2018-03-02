using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoundsDetection : MonoBehaviour {
    [SerializeField] GameObject outOfBoundsText;
    [SerializeField] GameObject deathCanvas;
    private int countdown = 5;
    private bool inTrigger;
    public GameObject deathSound;
     bool isDead;
    private bool gameOver;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y > 30)
        {
            // Start death coroutine
            if (!isDead)
            {
                Invoke("DeathByTornado", 2);
                isDead = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bounds")
        {
            StartCoroutine(OutOfBounds());
            inTrigger = true;
            outOfBoundsText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bounds")
        {
            inTrigger = false;
            outOfBoundsText.SetActive(false);
        }
    }
    private void DeathByTornado()
    {
        // Display the death canvas
        deathCanvas.SetActive(true);
        deathSound.GetComponent<AudioSource>().Play();
        // Disable the player movement
        gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>().enabled = false;
        gameObject.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().enabled = false;
        gameObject.GetComponent<AudioSource>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    IEnumerator OutOfBounds()
    {
        for(int i = 0; i <= countdown; i++)
        {
            if (!inTrigger) break;
            outOfBoundsText.GetComponent<Text>().text = "Leaving Area In: " + (countdown - i);
            yield return new WaitForSeconds(1);
            if(i == 5)
            {
                gameOver = true;

                // Load the results scene
                SceneManager.LoadScene(2);
            }
        }

    }
}
