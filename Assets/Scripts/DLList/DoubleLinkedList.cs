public class DoubleLinkedList
{
    public ListNode head;
    public ListNode tail;
    public int count = 0;

    public void AddNode(ListNode node)
    {
        if(head == null)
        {
            head = node;
            tail = node;
        }
        else
        {
            node.prevNode = tail;
            tail.nextNode = node;
            tail = node;
        }
        count++;
    }

    public void DeleteNode(ListNode node)
    {
        if(count > 0 && InList(node))
        {            
            ListNode prevNode = node.prevNode;
            ListNode nextNode = node.nextNode;
            node.nextNode = null;
            node.prevNode = null;
            if(head == node && tail == node)
            {
                head = null;
                tail = null;
            }
            else if(head == node)
            {
                nextNode.prevNode = null;
                head = nextNode;
            }
            else if(tail == node)
            {
                prevNode.nextNode = null;
                tail = prevNode;
            }
            else
            {
                prevNode.nextNode = nextNode;
                nextNode.prevNode = prevNode;
            }            
            count--;
        }
        
    }

    public ListNode FindNodeByIndex(int ind)
    {
        ListNode currNode = null;
        
        if(ind < count && count != 0)
        {
            bool fromHead = ind > count % 2 ? false : true;

            if(fromHead)
            {
                currNode = head;
                for (int i = 0; i < ind; i++)
                {
                    currNode = currNode.nextNode;
                }
            }
            else
            {
                currNode = tail;
                for (int i = count-1; i > ind; i--)
                {
                    currNode = currNode.prevNode;
                }
            }
            
            return currNode;
        }

        return null;
    }

    public bool InList(ListNode node)
    {
        if(count > 0)
        {
            ListNode currNode = head;
            for (int i = 0; i < count; i++)
            {
                if(node == currNode)
                {
                    return true;
                }
                if(currNode.nextNode != null)
                {
                    currNode = currNode.nextNode;
                }
            }
        }

        return false;
    }

    public ListNode FindNodeByName(string name, out bool found)
    {
        found = false;
        
        if(count != 0)
        {
            ListNode currNode = head;
            for (int i = 0; i < count; i++)
            {
                if(currNode.Name == name)
                {
                    found = true;
                    return currNode;
                }
                else
                {
                    currNode = currNode.nextNode;
                }
            }
        }

        return null;
    }
}
