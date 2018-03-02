﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotBreak : MonoBehaviour {
    public static GameManager instance = null;
	// Use this for initialization
	void Awake () {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this.GetComponent<GameManager>();

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
       
    }
}