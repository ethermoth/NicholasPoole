using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour {
    /*
     This script allows for ingame photographs to be saved as .png files on the user's computer!
         */

    public Camera selfRef;
    public Scanner picScanner;//the script that determines the points from an image.
    public GameObject UIText;
    public Light flash;//spotlight that lights up to show your camera's cooldown.
    public bool picReady = true;
    public float cooldownTimer = 3.0f;

    public int shotsLeft = 6;
    public Text shotCounter;
    public Image readyIndicator;

    public List<Material> photoLog;
    public Texture2D tempTex;

    // Use this for initialization
    void Awake () {
        flash.intensity = 0;
        picReady = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (UIText != null)
        {
            if (shotsLeft == 0)
            {

                UIText.SetActive(true);
                UIText.GetComponent<Text>().text = "You're out of pictures, get away from the tornado!";


            }
            shotCounter.text = shotsLeft.ToString();

            if (selfRef == null)
            {
                selfRef = Camera.main;
            }
            if (selfRef.enabled == true)
            {
                if (picReady == true && Input.GetButtonDown("Fire2") && shotsLeft > 0)
                {
                    MakePic();
                    GetComponent<AudioSource>().Play();

                }
            }

            if (picReady == false)
            {
                Debug.Log("Photo can't be taken!");
                flash.intensity = 5 * cooldownTimer;
                cooldownTimer -= Time.fixedDeltaTime;
            }

            if (cooldownTimer <= 0)
            {
                Debug.Log("Photo charge refreshed!");
                picReady = true;
                readyIndicator.color = Color.green;
                flash.intensity = 0;
                cooldownTimer = 3.0f;
            }
        }
    }

    public void MakePic ()
    {
        int temp = Mathf.Abs(shotsLeft - 6);
        
        Debug.Log("SNAP!");
        picReady = false;
        readyIndicator.color = Color.red;

        ScreenCapture.CaptureScreenshot("StormPursuit_Data\\Resources\\Photo" + temp + "result.png", 1);
        //Resources.LoadAsync("Assets\\Scripts\\Pictograph\\Resources\\Photo" + temp + "result.png");
        //tempTex = Resources.Load("Photo" + temp + "result") as Texture2D;

        RenderTexture rt = new RenderTexture(1920, 1080, 1);
        selfRef.targetTexture = rt;
        tempTex = new Texture2D(1920, 1080, TextureFormat.RGB24, false);
        selfRef.Render();
        RenderTexture.active = rt;
        tempTex.ReadPixels(new Rect(0, 0, 1920, 1080), 0, 0);
        tempTex.Apply();
        selfRef.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        photoLog[temp].mainTexture = tempTex;

        
        picScanner.ScanThis();
        shotsLeft -= 1;
    }
}
