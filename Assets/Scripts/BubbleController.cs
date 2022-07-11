using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public delegate void Behaviour();
    public Behaviour currentBehaviour;
    public float changeSize;
    public float minBubbleSize;
    Transform bubble;
    Vector3 playerPos;
    public float bubbleOffsetY ,bubbleOffsetZ;
    public bool isHold = true;

    public float farwardSpeed;
    public float maxSize;
    public float firedBubbleRange;
    public GameObject prefab;
    private void Start()
    {
        currentBehaviour = FollowPlayer;
        currentBehaviour += IncreaseBubbleSize;
    }
    bool isTouched;
    bool isShoot;
    void Update()
    {
        currentBehaviour?.Invoke();

    }
    public void IncreaseBubbleSize()
    {
        if (Input.GetMouseButton(0) && transform.localScale.x < maxSize)
        {
            transform.localScale += Vector3.one * changeSize * Time.deltaTime;
        }
    }
    public void FollowPlayer()
    {
        playerPos = PlayerMovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, (playerPos.y + transform.localScale.y / 2) + bubbleOffsetY, (playerPos.z + transform.localScale.z / 2) + bubbleOffsetZ);
    }

    public void SizeDecrement(float decrement)
    {
        if (UIManager.instance.gameState == GameState.GamePlay)
        {
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

    public void MoveFarward()
    {
        transform.position += new Vector3(0, 0, farwardSpeed * Time.deltaTime);

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    //public int myInt(){return ClampX;}
}

