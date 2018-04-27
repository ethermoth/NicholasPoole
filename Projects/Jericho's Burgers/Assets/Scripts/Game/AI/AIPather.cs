using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPather : MonoBehaviour
{
    // fields
    Graph m_Graph;
    List<Node> m_Path;
    bool m_IsPathing;
    int m_CurrentPathPos;

    [SerializeField] float m_MoveSpeed;
    [SerializeField] float m_NodeTouchThreshold;

    // for testing only
    [SerializeField] Node m_Goal;

    [SerializeField] Animator anim;

    private void Start()
    {
        m_Graph = GameManager.Instance.pathfinder.graph;
        m_IsPathing = false;
        m_CurrentPathPos = 0;

        if (!anim)
            anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_IsPathing)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward) * 10f);
            Debug.DrawLine(transform.position, m_Path[m_CurrentPathPos].transform.position, Color.red);
            transform.LookAt(m_Path[m_CurrentPathPos].transform.position);
            //transform.rotation = Quaternion.Euler(transform.position - m_Path[m_CurrentPathPos].transform.position);
            //transform.rotation = Quaternion.LookRotation(transform.position - m_Path[m_CurrentPathPos].transform.position);
            //SnapLookAt(m_Path[m_CurrentPathPos].transform.position);
            // move to the next node in the path
            if (MoveTo(m_Path[m_CurrentPathPos])) {
                m_CurrentPathPos++;
            }

            // stop once you reach the goal
            if (m_Path.Count == m_CurrentPathPos)
                m_IsPathing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // for testing only
            UpdateGoal(m_Goal);
        }

        anim.SetBool("Walk", m_IsPathing);
    }

    /// <summary>
    /// Tell the AI what their new goal node is.
    /// </summary>
    /// <param name="aGoal">New goal node to path toward.</param>
    public void UpdateGoal(Node aGoal)
    {
        m_Path = GameManager.Instance.pathfinder.AStarSearch(m_Graph, FindClosestNode(), aGoal);
        m_CurrentPathPos = 0;
        m_IsPathing = true;
        SnapLookAt(m_Path[0].transform.position);
    }

    /// <summary>
    /// Tells the AI to move toward a node location.
    /// </summary>
    /// <param name="aNode">Node to move the AI toward.</param>
    /// <returns></returns>
    private bool MoveTo(Node aNode)
    {
        transform.Translate(transform.TransformDirection(transform.forward) * m_MoveSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(aNode.transform.position - transform.position) <= m_NodeTouchThreshold)
        {
            return true;
        }

        return false;
    }

    private Node FindClosestNode()
    {
        Node _closest = m_Graph.graphNodes[0];
        for (int i = 1; i < m_Graph.graphNodes.Count; i++)
        {
            // check to see which one is closer
            if (Vector3.SqrMagnitude(m_Graph.graphNodes[i].transform.position - transform.position) < Vector3.SqrMagnitude(_closest.transform.position - transform.position))
            {
                _closest = m_Graph.graphNodes[i];
            }
        }
        Debug.Log(_closest);
        return _closest;
    }

    private void SnapLookAt(Vector3 aPosition)
    {
        Vector3 _forward = transform.TransformDirection(transform.forward);

        float _angle = Mathf.Acos(((_forward.x * aPosition.x) + (_forward.y * aPosition.y) + (_forward.z * aPosition.z)) / (_forward.magnitude * aPosition.magnitude));
        transform.Rotate(transform.up, _angle * Mathf.Rad2Deg);
    }
}
