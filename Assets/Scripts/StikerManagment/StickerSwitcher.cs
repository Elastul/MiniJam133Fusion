using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickerSwitcher : MonoBehaviour
{
    public static event Action<BaseStickerClass> StickerSwitch;
    public static event Action CreateSticker;
    public static event Action<InventoryView.ImageType> NewStickerAdded;
    public static event Action<int> RemoveSticker;
    public static event Action<int, int> StickerAmountChanged;
    public static event Action<int> NextElement;
    public static event Action<int> PrevElement;
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

    public void OnStickerCreation()
    {
        if(currSticker != null)
        {
            currNode.Amount--;
            StickerAmountChanged.Invoke(currNode.Index, currNode.Amount);
            if(currNode.Amount <= 0)
            {
                ListNode tempNextNode = currNode.nextNode;
                ListNode tempPrevNode = currNode.prevNode;
                RemoveSticker.Invoke(currNode.Index);
                stickerList.DeleteNode(currNode);
                CreateSticker.Invoke();
                if(tempNextNode != null && stickerList.InList(tempNextNode))
                {
                    currNode = tempNextNode;
                    currSticker = currNode.Sticker;
                    NextElement.Invoke(currNode.Index);
                }
                else if(tempPrevNode != null && stickerList.InList(tempPrevNode))
                {
                    currNode = tempPrevNode;
                    currSticker = currNode.Sticker;
                    NextElement.Invoke(currNode.Index);
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
                    NextElement.Invoke(0);
                    currNode = stickerList.head;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
                }
                else
                {
                    NextElement.Invoke(currNode.Index + 1);
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
                    PrevElement.Invoke(stickerList.tail.Index);
                    currNode = stickerList.tail;
                    currSticker = currNode.Sticker;
                    StickerSwitch.Invoke(currSticker);
                }
                else
                {
                    PrevElement.Invoke(currNode.Index - 1);
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
            StickerAmountChanged.Invoke(currNode.Index, currNode.Amount);
            NextElement.Invoke(currNode.Index);
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
            InventoryView.ImageType _imageType = InventoryView.ImageType.GRAVITY;
            switch (currNode.Name)
            {
                case "GravitySticker":
                _imageType = InventoryView.ImageType.GRAVITY;
                break;
                case "LockSticker":
                _imageType = InventoryView.ImageType.LOCK;
                break;
                case "MagnetSticker":
                _imageType = InventoryView.ImageType.MAGNET;
                break;
                case "FireSticker":
                _imageType = InventoryView.ImageType.FIRE;
                break;
            }
            NewStickerAdded.Invoke(_imageType);
        }        
    }
}
