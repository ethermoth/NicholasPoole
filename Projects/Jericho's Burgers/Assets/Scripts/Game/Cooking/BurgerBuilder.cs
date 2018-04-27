using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BurgerBuilder : MonoBehaviour
{
    List<GameObject> m_BurgerComponents;
    string m_Tag = "Ingredient";

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == m_Tag)
        {
            FixedJoint _joint = AddFixedJoint();
            _joint.connectedBody = other.GetComponent<Rigidbody>();
        }
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint _fx = gameObject.AddComponent<FixedJoint>();
        _fx.breakForce = 20000f;
        _fx.breakTorque = 20000f;
        return _fx;
    }
}
