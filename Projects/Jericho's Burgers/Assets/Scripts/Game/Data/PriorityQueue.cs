using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private class PQElement
    {
        public T element;
        public double priority;

        public PQElement(T aElement, double aPriority)
        {
            element = aElement;
            priority = aPriority;
        }
    }

    LinkedList<PQElement> queue;
    public int Count { get { return queue.Count; } }

    public void Enqueue(T aElement, double aPriority)
    {
        PQElement _pqe = new PQElement(aElement, aPriority);

        if (queue != null && queue.First != null)
        {

            LinkedListNode<PQElement> _node = queue.First;
            do
            {
                // if it blongs before this element add it in
                if (_pqe.priority < _node.Value.priority)
                {
                    queue.AddBefore(_node, new LinkedListNode<PQElement>(_pqe));
                    return;
                }
                // otehrwise keep going until the end of the list and add it there if it never found its place
                else if (_node.Next == null)
                {
                    queue.AddLast(new LinkedListNode<PQElement>(_pqe));
                    return;
                }
                _node = _node.Next;
            } while (_node.Next != null);
        }
        else
        {
            // queue is empty so add new element directly to it
            queue = new LinkedList<PQElement>();
            queue.AddFirst(_pqe);
        }
    }

    public T Dequeue()
    {
        if(queue != null)
        {
            T _element = queue.First.Value.element;
            queue.RemoveFirst();
            return _element;
        }
        else
        {
            return default(T);
        }
    }
}