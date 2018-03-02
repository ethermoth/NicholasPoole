using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour {
    [SerializeField] private GameObject tornado;
    [SerializeField] private float windSpeed;
    [SerializeField] private int windDistance;
    [SerializeField] private int pickUpDistance;
    [SerializeField] private int pickUpSpeed;
    [SerializeField] private bool permanent;
    private float gustSpeed;
    bool rotating;
    private Vector3 randomDirection;
    private int waitTime;
    // Use this for initialization
    void Awake () {
        StartCoroutine(WindCo());
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, tornado.transform.position);

        if (distance < windDistance)
        {
            Vector3 direction = -(gameObject.transform.position - tornado.transform.position).normalized;
            
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + (gustSpeed * windSpeed * direction * ((1/ Mathf.Pow(distance, 1/3))));
            if(distance < pickUpDistance)
            {
                /*
                if (!rotating)
                {
                    rotating = true;
                    StartCoroutine(RotateAround());
                }
                 */
                if (permanent)
                {
                    transform.parent = tornado.transform.GetChild(20);
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3(0, Random.Range(1f, 100f), 0);
                    transform.Rotate(tornado.transform.position, 20 * Time.deltaTime);
                    transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                }
                else
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity + new Vector3(0, pickUpSpeed, 0);

                }
            }
            else
            {
                rotating = false;
            }
        }
        
	}
    IEnumerator WindCo()
    {
        while (true) {
            waitTime = Random.Range(1, 3);
            gustSpeed = Random.Range(1f, 2f);
            randomDirection = new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2));
            yield return new WaitForSeconds(waitTime);
        }

    }
    IEnumerator RotateAround()
    {
        while (rotating)
        {
            transform.Rotate(tornado.transform.position, 20 * Time.deltaTime);
            yield return new WaitForSeconds(.1f);
        }

    }
}
