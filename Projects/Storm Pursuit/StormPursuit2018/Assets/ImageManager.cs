using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ImageManager : MonoBehaviour {
    public GameObject[] images;
    public GameObject gameManager;
	// Use this for initialization
	void Start () {
		if(gameManager == null)
        {
            gameManager = GameObject.Find("PhotoManager");
        }
        for(int i = 0; i < gameManager.GetComponent<TakePhoto>().photoLog.Count; i++)
        {
            float score;
            if (gameManager.GetComponent<TakePhoto>().photoLog[i] != null)
            {
                if(gameManager.GetComponent<Scanner>().allScores[i] < 0)
                {
                    score = 0;
                }
                else
                {
                    score = gameManager.GetComponent<Scanner>().allScores[i];
                }
                images[i].transform.GetChild(1).GetComponent<MeshRenderer>().material = gameManager.GetComponent<TakePhoto>().photoLog[i];
                images[i].transform.GetChild(0).GetComponent<Text>().text = "You Earned: $" + score.ToString();
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(3);
        }
	}
}
