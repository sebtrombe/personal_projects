using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public enum PlayerState    // Player state
{
    idle,
    walk, 
    attack, 
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public Vector2Value playerStartingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = playerStartingPosition.initialValue;
        currentState = PlayerState.idle;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentState == PlayerState.interact)
        {
            return;
        }

        if(currentState != PlayerState.stagger && currentState != PlayerState.attack)
        {
            // get movement input from user
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");

            // attack if "attack" pressed
            if(Input.GetButtonDown("attack"))
            {
                StartCoroutine(AttackCo());
            }

            // walk if input not 0
            else if(change != Vector3.zero)
            {
                currentState = PlayerState.walk;
                UpdateAnimationAndMove();
            }

            // idle otherwise
            else
            {
                animator.SetBool("moving", false);
                currentState = PlayerState.idle;
            }
        }
    }

    public void ReceiveItem()
    {
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.interact;
            Debug.Log("Player in interact state");
            animator.SetBool("receiveItem", true);
            receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
        }
        else
        {
            currentState = PlayerState.idle;
            Debug.Log("Player in idle state");
            animator.SetBool("receiveItem", false);
            receivedItemSprite.sprite = null;
        }
        
    }

    
    void UpdateAnimationAndMove() // moving animation
    {
        MoveCharacter();
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
        animator.SetBool("moving", true);
    }

    void MoveCharacter() // move player from input
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)  // knockback 
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        //Debug.Log(currentHealth.runtimeValue);
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            StartCoroutine(DeathCo(gameObject));
        }
    }

    private IEnumerator AttackCo() // attack coroutine 
    {
        currentState = PlayerState.attack;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.4f); // comment = spam attack available
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator DeathCo(GameObject gameObject) // death coroutine
    {
        yield return new WaitForSeconds(.4f);
        //Debug.Log(gameObject.name + " is dead");
        myRigidbody.velocity = Vector2.zero;
        gameObject.SetActive(false); // Uncomment : player disactive on death

    }

    private IEnumerator KnockCo(float knockTime) // knockback coroutine
    {
        if (myRigidbody != null )
        {
            currentState = PlayerState.stagger;
            yield return new WaitForSeconds(knockTime);
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
