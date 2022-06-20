using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    // public enum EnemyType
    // { 
    //     normal,storng,gaint
    // }
    // EnemyType type;
    public float dicreamentSize;
    public float speed, upwardSpeed = 5f;
    public GameObject bubble;
    Vector3 movepos;
    public float chaseRange;
    public Rigidbody rb;
    public Animator animator;
    public static Transform target;
    bool isCollided = false, isRunning = true , isFirstCollision = true;
    private void Start()
    {
        target = PlayerMovement.instance.transform;
    }
    private void Update()
    {
        if (Vector3.Distance(PlayerMovement.instance.transform.position, transform.position) < chaseRange)
        {
            if (target != null & isRunning == true)
            { //Debug.Log ("running");
                GetComponent<Animator>().SetBool("isRunning", true);
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                transform.LookAt(target.position);
                //   rb.MovePosition(target.position);
            }

            if (transform.position.z < target.position.z - 200)
            {
                Debug.Log(gameObject.name + " Destroyed");
                gameObject.SetActive(false);
            }
        }
        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }


        if (isCollided == true)
        {
            //Debug.Log("collide");
            bubble.SetActive(true);
            rb.useGravity = false;
            transform.position += new Vector3(0, upwardSpeed * Time.deltaTime, 0);
            GetComponent<Animator>().SetBool("isFalling", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (isFirstCollision == true)
            {
                isFirstCollision = false;
                FollowPlayer.instance.SizeDecrement(dicreamentSize);
                isRunning = false;
                isCollided = true;
                // target = null;
            }
        }
    }
}



