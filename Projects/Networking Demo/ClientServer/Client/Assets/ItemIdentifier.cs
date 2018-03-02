using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdentifier : MonoBehaviour {

    public int itemType;//set manually in the editor. Describes which item is available from this node.

    public GameObject rocketModel;
    public GameObject boostModel;
    public GameObject redFlagModel;
    public GameObject blueFlagModel;
    /*
      1:Rocket
      2:Boost
      3:Red Flag
      4:Blue Flag
    */

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (itemType > 0)
        {
            switch (itemType)
            {
                case 1:
                    rocketModel.transform.localPosition = Vector3.zero;
                    break;

                case 2:
                    boostModel.transform.localPosition = Vector3.zero;
                    break;

                case 3:
                    redFlagModel.transform.localPosition = Vector3.zero;
                    break;

                case 4:
                    blueFlagModel.transform.localPosition = Vector3.zero;
                    break;
            }
        }
    }
}
