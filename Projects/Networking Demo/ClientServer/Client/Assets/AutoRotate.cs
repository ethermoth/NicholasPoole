using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

    //Quaternion Zzero = new Quaternion(0, 0, 0, 0);

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.up * 40 * Time.deltaTime, Space.World);
    }
}
