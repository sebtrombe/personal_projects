using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState  // Enemy state machine
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(DeathCo(gameObject));
        }
    }

    private IEnumerator DeathCo(GameObject gameObject)
    {
        //!\ this.gameObject or gameObject ?
        yield return new WaitForSeconds(.4f);
        //Debug.Log(gameObject.name + " is dead");
        gameObject.SetActive(false);

    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }


    // Knocked back
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null && currentState != EnemyState.stagger)
        {
            currentState = EnemyState.stagger;
            yield return new WaitForSeconds(knockTime);
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}