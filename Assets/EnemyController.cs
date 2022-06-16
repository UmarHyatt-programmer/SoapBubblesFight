using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public float speed;
    public float chaseRange;
    public Rigidbody rb;
    public Animator animator;
    public static Transform target;
    private void Start()
    {
        target = PlayerMovement.instance.transform;
    }
    private void Update()
    {
        if (Vector3.Distance(PlayerMovement.instance.transform.position, transform.position) < chaseRange)
        {
            if (target != null)
            {
                transform.position=Vector3.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
             //   rb.MovePosition(target.position);
            }
        }
    }
    public void Chasing()
    {

    }
    public void Hange()
    {

    }
}
