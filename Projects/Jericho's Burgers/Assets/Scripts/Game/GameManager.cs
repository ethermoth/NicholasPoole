using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OrderSystem))]
public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    public static GameManager Instance { get { return instance; } }
    public OrderSystem orderSystem;

    [Header("Debug Test for Order System")]
    public List<BaseMenuItem> menuItems;

    public int playerMoney = 100;

    [Header("AI Settings")]
    [SerializeField] Pathfinding m_PathFinder;

    public Pathfinding pathfinder { get { return m_PathFinder; } }

    public enum VRLayer
    {
        Grab = 8,
        Press = 9
    }

    private void Awake()
    {
        if (instance != null || instance != this)
        {
            // Destroy(gameObject);
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        if (!orderSystem)
            orderSystem = GetComponent<OrderSystem>();
    }

    private void Start () {
        // debug test for order system
        // Order testOrder = new Order();
        // testOrder.AddItem(menuItems[0]);
        // Debug.Log(testOrder.GetItem(0).GetItemDetails());
    }

    public void AddMoney (int m_Amount) { playerMoney += m_Amount; }
    public void RemoveMoney (int m_Amount) { AddMoney(-m_Amount); }
}
