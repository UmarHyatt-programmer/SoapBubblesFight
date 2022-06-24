using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float changeSize;
    public float minBubbleSize;
    Transform bubble;
    Vector3 playerPos;
    public float bubbleOffset;
    public bool isHold = true;

    // Update is called once per frame
    public float maxSize;
    public static BubbleController instance;
    private void Awake()
    {
        if(instance == null){instance=this;}


        bubble = transform;
    }
    void Update()
    {
        if (UIManager.instance.gameState != GameState.GamePlay)
            return;
        if (isHold == true)
        {
            FollowPlayer();
            IncreaseBubbleSize();
        }
        //changing player bubble size
    }
    public void IncreaseBubbleSize()
    {
        if (bubble.localScale.x < maxSize)
        {
            bubble.localScale += Vector3.one * changeSize * Time.deltaTime;
        }
    }
    public void FollowPlayer()
    {
        playerPos = PlayerMovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, (playerPos.y + transform.localScale.y / 2) + bubbleOffset, (playerPos.z + transform.localScale.z / 2) + bubbleOffset);
    }

        public void SizeDecrement(float decrement)
    {
        if (UIManager.instance.gameState != GameState.GamePlay)
            return;

        if (transform.localScale.z > minBubbleSize)
        {
            transform.localScale -= new Vector3(decrement, decrement, decrement);
        }
        else
        {
            UIManager.instance.gameOverPanel.SetActive(true);
            Debug.Log("You lose");
            UIManager.instance.gameState = GameState.LevelFail;
            //Time.timeScale=0;
        }

    }
}

