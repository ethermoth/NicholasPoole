using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            // Quit the application
            Application.Quit();
        }
        else if(Input.GetKeyDown("r"))
        {
            // Reload the main game scene
            SceneManager.LoadScene(3);
        }
    }
}
