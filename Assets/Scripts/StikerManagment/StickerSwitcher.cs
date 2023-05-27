using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickerSwitcher : MonoBehaviour
{
    public static event Action<BaseStickerClass> StickerSwitch;
    public static event Action CreateSticker;
    public BaseStickerClass currSticker;
    public DoubleLinkedList stickerList;
    public ListNode currNode;

    public static StickerSwitcher Instance;

    private void Awake()
    {
        Instance = this;
        stickerList = new DoubleLinkedList();
        InputController.QButton += PrevSticker;
        InputController.EButton += NextSticker;
        InteractionInput.StickerSwitchRC += OnStickerCreation;
    }

    private void OnDisable() 
    {
        InputController.QButton -= PrevSticker;
        InputController.EButton -= NextSticker;        
        InteractionInput.StickerSwitchRC -= OnStickerCreation;
    }

    void Update()
    {
        // if (currNode != null) Debug.Log("Текущая нода: " + currNode.Name + " " + currNode.Amount);
        // if (currSticker != null) Debug.Log("Текущий стикер: " + currSticker);
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
                CreateSticker.Invoke();
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
                StickerSwitch.Invoke(currSticker);
            }
            else
            {
                CreateSticker.Invoke();
            }
        }
    }

    public void NextSticker()
    {
        if(currNode != null)
        {
            if(!(currNode == stickerList.head && currNode == stickerList.tail))
            {
                if(currNode == stickerList.tail)
                {
                    currNode = stickerList.head;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
                }
                else
                {
                    currNode = currNode.nextNode;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
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
            if(!(currNode == stickerList.head && currNode == stickerList.tail))
            {
                if(currNode == stickerList.head)
                {
                    currNode = stickerList.tail;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
                }
                else
                {
                    currNode = currNode.prevNode;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
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
            StickerSwitch.Invoke(currSticker);
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
            StickerSwitch.Invoke(currSticker);
        }        
    }
}
