using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // fields
    [SerializeField] Graph m_Graph;
    [SerializeField] Material m_PathMat;
    [SerializeField] Material m_BaseMat;

    [Header("Testing")]
    [SerializeField]
    Node m_Start;
    [SerializeField] Node m_Goal;

    public Graph graph { get { return m_Graph; } }

    /// <summary>
    /// Computes the hueristic of two locations in three dimensions.
    /// </summary>
    /// <param name="a">Location 1</param>
    /// <param name="b">Location 2</param>
    public static double Hueristic3(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b);
    }

    public List<Node> AStarSearch(Graph aGraph, Node aStart, Node aGoal)
    {
        // throw error if the nodes do not exist in the graph
        if (!(aGraph.graphNodes.Contains(aStart) && aGraph.graphNodes.Contains(aGoal)))
        {
            Debug.LogError("Start or end node is not present in the current graph.");
            return null;
        }


        // setup path vars
        Dictionary<Node, Node> _cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, double> _costSoFar = new Dictionary<Node, double>();

        // setup frontier
        PriorityQueue<Node> _frontier = new PriorityQueue<Node>();

        // enqueue the start pos
        _frontier.Enqueue(aStart, 0);

        // setup the pathing variables
        _cameFrom[aStart] = aStart;
        _costSoFar[aStart] = 0;

        // A*
        while (_frontier.Count > 0)
        {
            Node _current = _frontier.Dequeue();

            // early out
            if (_current.Equals(aGoal))
            {
                break;
            }

            foreach (Node n in aGraph.Neighbors(_current))
            {
                double _newCost = _costSoFar[_current] + n.weight;

                if (!_costSoFar.ContainsKey(n) || _newCost < _costSoFar[n])
                {
                    _costSoFar[n] = _newCost;

                    double _priority = _newCost + Hueristic3(n.transform.position, aGoal.transform.position);
                    _frontier.Enqueue(n, _priority);

                    _cameFrom[n] = _current;
                }
            }
        }

        // Logs
        //foreach(KeyValuePair<Node, Node> kv in _cameFrom)
        //{
        //    Debug.Log(kv.Key + " is mapped to " + kv.Value);
        //}

        Node _next = _cameFrom[aGoal];

        List<Node> _path = new List<Node>();
        _path.Add(aGoal);
        _path.Add(_next);

        // rebuild the path
        while (!_cameFrom[_next].Equals(aStart))
        {
            _path.Add(_cameFrom[_next]);
            _next = _cameFrom[_next];
        }
        _path.Add(aStart);
        _path.Reverse();

        return _path;
    }

    private void Update()
    {
       //  DEBUG DISPLAY FOR PATHING
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (Node n in m_Graph.graphNodes)
            {
                n.GetComponent<Renderer>().material = m_BaseMat;
            }

            List<Node> astar = AStarSearch(m_Graph, m_Start, m_Goal);
            // testing from start of graph to end of graph
            foreach (Node n in astar)
            {
                n.GetComponent<Renderer>().material = m_PathMat;
            }
        }
    }
}
