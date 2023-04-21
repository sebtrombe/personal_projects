using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReceived : MonoBehaviour
{
    public GameObject receivedItem;
    public PlayerState currentState;
    public SpriteRenderer receivedItemSprite;
    //public bool receivedItemActive = false;
    

    // player --> interact
    // display dialog box + item
    // put in raised hand sprite


    // player back to 
        
        /*if (receivedItemActive)
        {
            receivedItemClue.SetActive(true);
            currentState = PlayerState.interact;
        }
        else
        {
            receivedItemClue.SetActive(false);
        }
    }*/

    /*public void ReceiveItem()
    {
        if (currentState != PlayerState.interact)
        {
            animator.SetBool("receiveItem", true);
            currentState = PlayerState.interact;
            receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            animator.SetBool("receiveItem", false);
            currentState = PlayerState.idle;
            receivedItemSprite.sprite = null;
        }
    }*/
}
