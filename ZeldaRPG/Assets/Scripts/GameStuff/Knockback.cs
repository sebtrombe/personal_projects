using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public FloatValue damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // breakable object
        if (collider.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collider.GetComponent<pot>().Smash();
        }

        if (collider.gameObject.CompareTag("enemy") 
            || collider.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hitObject = collider.GetComponent<Rigidbody2D>();
            if (hitObject != null)
            {
                // force direction
                Vector2 forceDirection = 
                    hitObject.transform.position - transform.position;

                // force value
                Vector2 force = 
                    forceDirection.normalized * thrust;

                // apply force to the hit object
                hitObject.AddForce(force, ForceMode2D.Impulse);

                // apply to enemy
                if (collider.gameObject.CompareTag("enemy") && collider.isTrigger)
                {
                    collider.GetComponent<Enemy>().Knock(hitObject, knockTime, damage.runtimeValue);
                }

                // apply to player
                if (collider.gameObject.CompareTag("Player"))
                {
                    if(collider.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        collider.GetComponent<PlayerMovement>().Knock(knockTime, damage.runtimeValue);
                    }
                }
            }
        }
    }
}
