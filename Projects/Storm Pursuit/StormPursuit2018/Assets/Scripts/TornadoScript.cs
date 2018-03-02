using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour {
    [SerializeField] private int spineDistance;
    [SerializeField] private float diskRadius;
    [SerializeField] private int spineHeight;
    [SerializeField] private float tornadoSpeed;
    [SerializeField] private float maxSpeed;

    [SerializeField] private GameObject particlePiece;
    private GameObject[] spineList;
    public List<GameObject> tempList;
    int heightCheck = - 1;
    int listLength;
    private float gustSpeed;
    private Vector3 randomDirection;
    private int waitTime;
    // Use this for initialization
    void Start () {
        StartCoroutine(WindCo());
        spineList = new GameObject[spineHeight];
		for(int i = 0; i < spineHeight; i++)
        {
            spineList[i] = Instantiate(particlePiece, new Vector3(transform.position.x, transform.position.y + (i * spineDistance), transform.position.z), Quaternion.identity);
            spineList[i].GetComponent<ParticleScript>().moving = true;
            spineList[i].GetComponent<ParticleScript>().magnitude = (spineHeight - i + 1) * 1.3f;
            spineList[i].transform.SetParent(gameObject.transform);

            if (i % 10 == 0)
            {
                heightCheck += 1;
            }
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    if(x != y){
                        GameObject block = Instantiate(particlePiece, new Vector3(transform.position.x + (x * diskRadius * Mathf.Pow(i, 1.2f)), transform.position.y + (i * spineDistance), transform.position.z + (y * diskRadius * Mathf.Pow(i, 1.2f))), Quaternion.identity);
                        block.transform.SetParent(spineList[i].transform);
                        tempList.Add(block);
                        listLength = tempList.Count;


                    }

                }
            }
            for (int p = 0; p < heightCheck; p++)
            {
                for (int z = 0; z < listLength - 1; z++)
                {
                    Vector3 direction = (tempList[z].transform.position - tempList[z + 1].transform.position).normalized;
                    float distance = Vector3.Distance(tempList[z].transform.position, tempList[z + 1].transform.position);
                    Vector3 midPoint = tempList[z + 1].transform.position + (direction * (distance / 2));
                    GameObject block2 = Instantiate(particlePiece, midPoint, Quaternion.identity);
                    block2.transform.SetParent(spineList[i].transform);

                    tempList.Add(block2);
                }
            }
            tempList = new List<GameObject>();
 
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.GetComponent<Rigidbody>().velocity.z < maxSpeed)
        {
            transform.GetComponent<Rigidbody>().velocity += transform.forward * tornadoSpeed;

        }
        if(transform.GetComponent<Rigidbody>().velocity.x > maxSpeed)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(maxSpeed, 0, maxSpeed);
        }
        else if(transform.GetComponent<Rigidbody>().velocity.x < -maxSpeed)
        {
            transform.GetComponent<Rigidbody>().velocity = new Vector3(-maxSpeed, 0, maxSpeed);
        }
        else
        {
            transform.GetComponent<Rigidbody>().velocity += randomDirection * tornadoSpeed;
        }

    }
    IEnumerator WindCo()
    {
        while (true)
        {
            waitTime = Random.Range(1, 3);
            gustSpeed = Random.Range(1f, 2f);
            randomDirection = new Vector3(Random.Range(-2f, 2f), 0, 0);
            yield return new WaitForSeconds(waitTime);
        }

    }
}
