using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour {
    private static List<Order> orders;

    [Header("Debugging")]
    [SerializeField] [Range(1f, 10f)] float minOrderTime;
    [SerializeField] [Range(1f, 10f)] float maxOrderTime;
    [SerializeField] float curOrderTime;
    private float orderTimeLeft;

    [SerializeField] GameObject orderTicketPrefab;
    [SerializeField] GameObject orderTicketUI;
    [SerializeField] int maxNumOrders = 4;
    public int curNumOrders = 0;

    private void Start () {
        orders = new List<Order>();
        SetOrderTime();
    }

    public static bool AddOrder (Order m_Order) {
        orders.Add(m_Order);
        return orders.Contains(m_Order);
    }

    public static bool RemoveOrder (Order m_Order) {
        return orders.Remove(m_Order);
    }

    public static Order GetOrder (int m_OrderIndex) {
        return orders[m_OrderIndex];
    }

    public static int GetOrderIndex (Order m_Order) {
        return orders.IndexOf(m_Order);
    }

    public static List<Order> GetOrders () {
        return orders;
    }



    // Debugging - Order creation and assignment
    private void Update () {
        orderTimeLeft -= Time.deltaTime;
        if (orderTimeLeft <= 0f) {
            GenerateOrder();
        }
    }

    private void SetOrderTime () {
        curOrderTime = Random.Range(minOrderTime, maxOrderTime);
        orderTimeLeft = curOrderTime;
    }

    private void GenerateOrder () {
        if (curNumOrders == maxNumOrders)
            return;

        Order o = new Order();
        int numItemsToAdd = Random.Range(1, 3);
        for (int i = 0; i <= numItemsToAdd; i++) {
            o.AddItem(GameManager.Instance.menuItems[Random.Range(0, GameManager.Instance.menuItems.Count)]);
        }
        AddOrder(o);
        string orderContents = "";
        int itemCount = 1;
        foreach (BaseMenuItem item in o.GetItems()) {
            orderContents += string.Format("({0}) {1}\n", itemCount, item.GetName());
            foreach (BaseItemComponent comp in item.GetItemComponents()) {
                orderContents += string.Format("\t  {0}\n", comp.componentName);
            }
            itemCount++;
        }
        CreateOrderTicket(string.Format("Order #{0}", GetOrderIndex(o) + 1), orderContents);
        SetOrderTime();
        curNumOrders++;
    }

    private void CreateOrderTicket (string aTitle, string aContents) {
        GameObject order = Instantiate(orderTicketPrefab, orderTicketUI.transform);
        order.GetComponent<OrderTicket>().orderTitle.text = aTitle;
        order.GetComponent<OrderTicket>().orderContents.text = aContents;
    }
}
