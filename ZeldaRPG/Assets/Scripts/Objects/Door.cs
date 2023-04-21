using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DoorType
{
    key,
    enemy,
    button
}
public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D doorBoxCollider;

    /*private void Start()
    {
        {
            doorSprite = GetComponent<SpriteRenderer>();
        }
    }*/

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerInRange && thisDoorType == DoorType.key)
            {
                if(playerInventory.numberOfKeys > 0)
                {
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }
    public void Open()
    {
        // Turn off door sprite renderer
        doorSprite.enabled = false;
        // Set Open true
        open = true;
        // Turn off door box collider
        doorBoxCollider.enabled = false;
    }

    public void Close()
    {

    }
}
