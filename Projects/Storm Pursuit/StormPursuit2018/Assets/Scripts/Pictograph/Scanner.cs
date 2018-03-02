using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanner : MonoBehaviour {

    public Dictionary<Material, float> photoStorage;
    public List<float> allScores;

    public float currentDist;

    public float Score;

    /*
        These are parameters for the scoring system.
        -focus object is close to the center of the screen
        -focus object is the proper distance away
        -Hazardous objects are in the frame
        -camera is angled (vertically) more than -25 degrees.
         */
    public GameObject theCam;
    public GameObject picTarget; //the specific entity you are tasked with taking photos of.
                                 //This would be placed around the midway point of the tornado.

    public GameObject centerNode;//Placed to show distance between the center of the screen and the target obj

    public GameObject topNode;
    public GameObject botNode;

    public float offset;         //The distance between centerNode (after setup) and the target obj

    public GameObject stormTop;
    public GameObject stormBase;

    public float critDistance;   //The most beneficial distance between the player and picTarget.

    public float pitch;         //The vertical angle of the camera

    public bool airBonus = false; //More points if you were airborne when taking the shot.

    public bool debrisBonus = false; //If a debris-type object is in the frame, add points (currently unimplemented)
    // Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(picTarget != null)
        {
            currentDist = Vector3.Distance(theCam.transform.position, picTarget.transform.position);

            if (theCam.transform.eulerAngles.x > 180) { pitch = theCam.transform.eulerAngles.x - 360; }

            else
                pitch = theCam.transform.eulerAngles.x;
        }

	}

    public void ScanThis()
    {
        centerNode.transform.position = theCam.transform.position;
        topNode.transform.position = new Vector3(theCam.transform.position.x, 0.4f + theCam.transform.position.y, theCam.transform.position.z);
        botNode.transform.position = new Vector3(theCam.transform.position.x, -4.24f + theCam.transform.position.y, theCam.transform.position.z);

        Score += (800 - 2 * Mathf.Abs(critDistance - currentDist));

        if (pitch > -20)
        {
            Score += (400);
        }
        //The hard part: checking what is in the frame.

        //Move CenterNode to be [currentDist] units away from the camera, at the center of the screen.
        centerNode.transform.Translate(Vector3.forward * currentDist);
        topNode.transform.Translate(Vector3.forward * (currentDist * Mathf.Sqrt(2)));
        botNode.transform.Translate(Vector3.forward * (currentDist * Mathf.Sqrt(2)));

        offset = Vector3.Distance(picTarget.transform.position, centerNode.transform.position);
        //how close is the camera's center to the storm's center?
        Score += 1500 - (45 * offset);

        //is the top of the tornado in the frame? If not, how much did you miss?
        if(topNode.transform.position.y > stormTop.transform.position.y)
            Score += 500;
        
        else
        {
            Score += 400 - 2 * Mathf.Abs(topNode.transform.position.y - stormTop.transform.position.y);
        }

        //is the base of the storm in the frame?
        if (botNode.transform.position.y < stormBase.transform.position.y)
            Score += 400;

        else
        {
            Score += 300 - 1 * Mathf.Abs(botNode.transform.position.y - stormBase.transform.position.y);
        }

        //Save score to the log, 
        allScores.Add(Score);
        //photoStorage.Add(GetComponent<TakePhoto>().photoLog[Mathf.Abs(GetComponent<TakePhoto>().shotsLeft - 6)], Score);
        Score = 0;
        centerNode.transform.position = theCam.transform.position;
        topNode.transform.position = new Vector3(theCam.transform.position.x, 0.4f + theCam.transform.position.y, theCam.transform.position.z);
        botNode.transform.position = new Vector3(theCam.transform.position.x, -4.24f + theCam.transform.position.y, theCam.transform.position.z);
    }
}
