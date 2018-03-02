using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public bool isPlayer = true;
    public bool Highlight = false;
    public int PlayerCount = 0;
    public int MyNumber = 0;

    public GameObject Client;//the client network

  

    // Update is called once per frame
    void Awake()
    {
        MyNumber = PlayerCount;
    }
    
    void Update()
    {
        if (Client == null)
        {
            Client = GameObject.Find("ExampleClient");
        }
    }
  
}
