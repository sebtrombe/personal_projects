using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialogString;

    // chest content
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public bool isActive = true;
    public SignalSender giveItem;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }

    public void OpenChest()
    {
        // dialog window
        dialogBox.SetActive(true);
        dialogText.text = contents.itemDescription;
        // add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        Debug.Log("Key Received");
        // Raise signal to animate player
        giveItem.Raise();
        // set chest to opened
        anim.SetBool("isOpen", true);
        isOpen = true;
        // raise context clue
        context.Raise();
    }

    public void ChestAlreadyOpen()
    {
        if (isActive)
        {
            // turn dialog off
            dialogBox.SetActive(false);
            // set current item to empty
            playerInventory.currentItem = null;
            // set chest to opened
            isOpen = true;
            // raise signal to player to stop animating
            giveItem.Raise();
            // raise context clue
            //context.Raise();
            // set chest active to false
            isActive = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger && isActive)
        {
            playerInRange = true;
            context.Raise();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && !collider.isTrigger && isActive)
        {
            playerInRange = false;
            context.Raise();
        }
    }
}