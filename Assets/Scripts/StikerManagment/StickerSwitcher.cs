using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StickerRequestEvent : UnityEvent<BaseStickerClass>
{
}
public class StickerSwitcher : MonoBehaviour
{
    public StickerRequestEvent requestEvent;
    public BaseStickerClass currSticker;
    public DoubleLinkedList stickerList;
    public ListNode currNode;

    private void OnEnable() 
    {
        InputController.QButton += PrevSticker;
        InputController.EButton += NextSticker;
    }

    public StickerSwitcher()
    {
        stickerList = new DoubleLinkedList();
    }

    private void OnDisable() 
    {
        InputController.QButton -= PrevSticker;
        InputController.EButton -= NextSticker;        
    }

    private void Update() 
    {
        //Debug.Log("Текущая нода: " + currNode);
    }

    public void OnStickerCreation()
    {
        if(currSticker != null)
        {
            currNode.Amount--;
            if(currNode.Amount <= 0)
            {
                ListNode tempNextNode = currNode.nextNode;
                ListNode tempPrevNode = currNode.prevNode;
                stickerList.DeleteNode(currNode);
                if(tempNextNode != null && stickerList.InList(tempNextNode))
                {
                    currNode = tempNextNode;
                    currSticker = currNode.Sticker;
                }
                else if(tempPrevNode != null && stickerList.InList(tempPrevNode))
                {
                    currNode = tempPrevNode;
                    currSticker = currNode.Sticker;
                }
                else
                {
                    currNode = null;
                    currSticker = null;
                }
                requestEvent.Invoke(currSticker);
            }
        }
    }

    public void NextSticker()
    {
        if(currNode != null)
        {
            if(currNode != stickerList.head && currNode != stickerList.tail)
            {
                if(currNode == stickerList.tail)
                {
                    currNode = stickerList.head;
                }
                else
                {
                    currNode = currNode.nextNode;
                    currSticker = currNode.Sticker;
                    requestEvent.Invoke(currSticker);
                    Debug.Log("Текущий стикер: " + currSticker);
                    Debug.Log("Текущая нода: " + currNode.Name + " " + currNode.Amount);
                }
            }
        }
        else
        {
            currSticker = null;
        }
    }

    public void PrevSticker()
    {
        if(currNode != null)
        {
            if(currNode != stickerList.head && currNode != stickerList.tail)
            {
                if(currNode == stickerList.head)
                {
                    currNode = stickerList.tail;
                }
                else
                {
                    currNode = currNode.prevNode;
                    currSticker = currNode.Sticker;
                    requestEvent.Invoke(currSticker);
                    Debug.Log("Текущий стикер: " + currSticker);
                    Debug.Log("Текущая нода: " + currNode.Name + " " + currNode.Amount);
                }
            }
        }
        else
        {
            currSticker = null;
        }
    }

    public void OnStickerPickup(BaseStickerClass sticker)
    {
        bool found = false;
        ListNode foundNode = null;

        if(stickerList.count != 0)
        {
            foundNode = stickerList.FindNodeByName(sticker.GetType().ToString(), out found);
        }

        if(found)
        {
            foundNode.Amount++;
            currNode = foundNode;
            currSticker = sticker;
            requestEvent.Invoke(currSticker);
            Debug.Log("Текущий стикер: " + currSticker);
            Debug.Log("Текущая нода: " + currNode.Name + " " + currNode.Amount);
        }
        else
        {
            ListNode node = new ListNode();
            node.Amount++;
            node.Name = sticker.GetType().ToString();
            node.Sticker = sticker;
            stickerList.AddNode(node);
            currNode = node;
            currSticker = sticker;
            requestEvent.Invoke(currSticker);
            Debug.Log("Текущий стикер: " + currSticker);
            Debug.Log("Текущая нода: " + currNode.Name + " " + currNode.Amount);
        }        
    }
}
