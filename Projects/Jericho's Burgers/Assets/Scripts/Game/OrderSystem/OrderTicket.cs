using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderTicket : MonoBehaviour {

    public Text orderTitle;
    public Text orderContents;

    private Animator anim;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver () {
        // Play mouseover anim
        anim.SetTrigger("MouseOver");
    }

    public void CompleteOrder () {
        GameManager.Instance.orderSystem.curNumOrders--;
        Destroy(gameObject, 1f);
    }
}
