using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public static BubbleSpawner instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public GameObject bubblePrefab;

    public void SpawnBubble()
    {
        var obj = Instantiate(bubblePrefab, Vector3.zero, Quaternion.identity);
        PlayerMovement.instance.handBubble = obj.GetComponent<BubbleController>();
    }
    public void Update()
    {
         if (UIManager.instance.gameState != GameState.GamePlay)
            return;
            
        if(Input.GetMouseButton(0))
        {

        }
        if(Input.GetMouseButtonUp(0))
        {
            //PlayerMovement.instance.handBubble.currentBehaviour=null;
            PlayerMovement.instance.handBubble.currentBehaviour+=PlayerMovement.instance.handBubble.MoveFarward;
            PlayerMovement.instance.handBubble.currentBehaviour-=PlayerMovement.instance.handBubble.FollowPlayer;
            SpawnBubble();
        }
    }
}
