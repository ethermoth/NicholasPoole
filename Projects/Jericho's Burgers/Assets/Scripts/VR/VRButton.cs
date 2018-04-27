using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class VRButton : MonoBehaviour
{
    public bool laserActivated;

    [HideInInspector] public bool hovered;
    [HideInInspector] public bool pressed;

    [SerializeField] Material hoveredMaterial;
    [SerializeField] Material unhoveredMaterial;
    [SerializeField] Material pressedMaterial;

    private MeshRenderer meshRenderer;
    private EventSystem eventSystem;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        eventSystem = FindObjectOfType<EventSystem>();

        if(!eventSystem)
        {
            Debug.LogError("No event system found in the world.  Cannot create event systems at runtime, please add one manually into the scene.");
        }
    }

    private void Update()
    {
        if (pressed)
        {
            meshRenderer.material = pressedMaterial;

        }
        else
        {
            if (hovered)
                meshRenderer.material = hoveredMaterial;
            else
                meshRenderer.material = unhoveredMaterial;
        }
    }

    /// <summary>
    /// Handles events from the laser.
    /// </summary>
    public void LaserEvent()
    {
        eventSystem.SetSelectedGameObject(gameObject);
        eventSystem.SetSelectedGameObject(null);
    }
}
