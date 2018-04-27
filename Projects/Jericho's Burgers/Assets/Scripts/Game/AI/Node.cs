using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // fields
    List<Node> m_ConnectedNodes;
    [SerializeField] List<Node> m_Blacklist;
    [SerializeField] List<Node> m_Whitelist;
    [SerializeField] double m_Weight;


    // properties
    public List<Node> connectedNodes { get { return m_ConnectedNodes; } set { m_ConnectedNodes = value; } }
    public double weight { get { return m_Weight; } }

    public void Initialize(Graph aGraph)
    {
        m_ConnectedNodes = new List<Node>();

        foreach (Node n in aGraph.graphNodes)
        {
            // don't connect yourself lel
            if (n == this)
            {
                continue;
            }

            // add the connection if the node meets the distance requirements
            if (Vector3.SqrMagnitude(transform.position - n.transform.position) <= aGraph.connectionDistance * aGraph.connectionDistance)
            {
                m_ConnectedNodes.Add(n);
            }
        }

        // blacklist nodes
        foreach (Node n in m_Blacklist)
        {
            bool _removed = m_ConnectedNodes.Remove(n);
#if UNITY_EDITOR
            if (_removed)
            {
                Debug.LogWarning("Removed node (" + n + ") from node (" + this + ")'s connection list.");
            }
#endif
        }

        // whitelist nodes
        foreach (Node n in m_Whitelist)
        {
            if (!m_ConnectedNodes.Contains(n))
            {
                m_ConnectedNodes.Add(n);
                if (!n.connectedNodes.Contains(this))
                {
                    n.connectedNodes.Add(this);
                }
            }
#if UNITY_EDITOR
            Debug.LogWarning("Added node (" + n + ") to node (" + this + ")'s connection list and vice versa.");
#endif
        }
    }
}
