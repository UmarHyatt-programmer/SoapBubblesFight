using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //  GameObject player ;
    public static FollowPlayer instance;
    Vector3 playerPos;
    public float decrement;
    public float bubbleOffset;
    public float minBubbleSize;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        playerPos = PlayerMovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, (playerPos.y + transform.localScale.y / 2) + bubbleOffset, (playerPos.z + transform.localScale.z / 2) + bubbleOffset);
    }
    public void SizeDecrement(float decrement)
    {
        if (transform.localScale.z > minBubbleSize)
        {
            transform.localScale -= new Vector3(decrement, decrement, decrement);
        }
        else
        {
            UIManager.instance.gameOverPanel.SetActive(true);
            Debug.Log("You lose");
            Time.timeScale=0;
        }

    }
}
