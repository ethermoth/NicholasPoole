using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    // fields
    List<Node> m_GraphNodes;

    [SerializeField] List<Node> m_Blacklist;
    [SerializeField] List<Node> m_Whitelist;
    [SerializeField] float m_ConnectionDistance;

    // properties
    public List<Node> graphNodes { get { return m_GraphNodes; } }
    public float connectionDistance { get { return m_ConnectionDistance; } }

    private void Start()
    {
        m_GraphNodes = new List<Node>();

        // fill up the graph
        foreach (Transform t in transform)
        {
            Node _node = t.GetComponent<Node>();
            if (_node)
            {
                m_GraphNodes.Add(_node);
            }
        }

        // handle blacklisting
        BlacklistNodes();

        // handle whitelisting second in case it needs to overwrite blacklist
        WhitelistNodes();

        foreach (Node n in m_GraphNodes)
        {
            n.Initialize(this);
        }
    }

    /// <summary>
    /// Blacklists nodes from the entire graph.
    /// </summary>
    void BlacklistNodes()
    {
        foreach (Node n in m_Blacklist)
        {
            bool _removed = m_GraphNodes.Remove(n);
#if UNITY_EDITOR
            if (_removed)
            {
                Debug.LogWarning("Removed node (" + n + ") from graph (" + this + ").");
            }
#endif
        }
    }

    /// <summary>
    /// Whitelists nodes to the entire graph.
    /// </summary>
    void WhitelistNodes()
    {
        foreach (Node n in m_Whitelist)
        {
            if (!m_GraphNodes.Contains(n))
            {
                m_GraphNodes.Add(n);
            }
#if UNITY_EDITOR
            Debug.LogWarning("Added node (" + n + ") to graph (" + this + ").");
#endif
        }
    }

    /// <summary>
    /// Draw connections between the graph nodes.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (m_GraphNodes != null)
        {
            foreach (Node n in m_GraphNodes)
            {
                if (n)
                {
                    //Gizmos.DrawSphere(n.transform.position, 1f);
                    foreach (Node o in n.connectedNodes)
                    {
                        Gizmos.DrawLine(n.transform.position, o.transform.position);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Quick access to list of neighboring nodes.
    /// </summary>
    /// <param name="aNode">Node whose neighbors need to be discovered.</param>
    /// <returns></returns>
    public List<Node> Neighbors(Node aNode)
    {
        return aNode.connectedNodes;
    }
}
