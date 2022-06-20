using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float changeSize;
    public Transform bubble;

    // Update is called once per frame
    public float maxSize;
    public bool a = false,b = false;
    void Update()
    {   //changing player bubble size
        if(bubble.localScale.x<maxSize)
        {
        bubble.localScale += Vector3.one*changeSize * Time.deltaTime;
        }
    }
}

