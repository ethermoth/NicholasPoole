using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] GameObject m_Ingredient;
    [SerializeField] float m_SpawnInterval;
    GameObject m_CurrentIngredient;
    bool m_CanInvoke;

    private void Start()
    {
        SpawnIngredient();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.Equals(m_CurrentIngredient) && m_CanInvoke)
        {
            Invoke("SpawnIngredient", m_SpawnInterval);
            m_CanInvoke = false;
        }
    }

    private void SpawnIngredient()
    {
        m_CurrentIngredient = Instantiate(m_Ingredient, transform.position, Quaternion.identity);
        m_CanInvoke = true;

        GetComponentInChildren<ParticleSystem>().Play();
    }
}
