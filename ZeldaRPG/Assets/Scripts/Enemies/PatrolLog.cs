using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance = 0.2f;

    public override void CheckDistance()
    {
        if (currentState != EnemyState.stagger) // if log not staggered
        {
            // if distance with player between chaseRadius & attackRadius 
            //--> move enemy + change its state to moving
            if (Vector3.Distance(
            target.position, transform.position) <= chaseRadius
            && Vector3.Distance(
                target.position, transform.position) > attackRadius)
            {
                if (currentState == EnemyState.idle
                    || currentState == EnemyState.walk)
                {
                    Vector3 moveDirection = Vector3.MoveTowards(
                        transform.position,
                        target.position,
                        moveSpeed * Time.deltaTime);
                    ChangeAnim(moveDirection - transform.position);
                    myRigidbody.MovePosition(moveDirection);
                    //ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                }

            }
            else if (Vector3.Distance(
            target.position, transform.position) > chaseRadius)
            {
                if(Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
                {
                    Vector3 moveDirection = Vector3.MoveTowards(
                        transform.position,
                        path[currentPoint].position,
                        moveSpeed * Time.deltaTime);
                    ChangeAnim(moveDirection - transform.position);
                    myRigidbody.MovePosition(moveDirection);
                }
                else
                {
                    ChangeGoal();
                }
                
            }
        }
    }

    private void ChangeGoal() // set next goal point
    {
        if(currentPoint == path.Length - 1) // if last point of path list
        {
            currentPoint = 0; // set goal point to first
            currentGoal = path[0];
        }
        else // set goal point ot next point
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
