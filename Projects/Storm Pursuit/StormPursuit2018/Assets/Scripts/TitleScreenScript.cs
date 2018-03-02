using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    [SerializeField]
    GameObject howToPlayText, titleText, playButton, howToPlayButton;

    public void PlayButton()
    {
        // Load the game scene on hit
        SceneManager.LoadScene(3);
    }

    public void HTPButton()
    {
        // Toggle the how to play screen
        if (howToPlayText.activeSelf == false)
        {
            howToPlayText.SetActive(true);
            titleText.SetActive(false);
            playButton.SetActive(false);

            howToPlayButton.GetComponentInChildren<Text>().text = "Back";
        }
        else
        {
            howToPlayText.SetActive(false);
            titleText.SetActive(true);
            playButton.SetActive(true);

            howToPlayButton.GetComponentInChildren<Text>().text = "How To Play";
        }
    }
}
