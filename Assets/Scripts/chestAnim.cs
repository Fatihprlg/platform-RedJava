using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Sprite[] ChestAnim;
    SpriteRenderer spriteRenderer;
    int spriteCount = 0;
    GameObject chestPaper;
    bool isEntered = false;
    void Start()
    {
        chestPaper = GameObject.Find("chest1Paper");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (isEntered) 
        { 
            ChestAnimation(); 
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player") 
        { 
            isEntered = true; 
            Debug.Log("Girdi"); 
        }
    }
    void ChestAnimation()
    {
        if (spriteCount != ChestAnim.Length)
        {
            spriteRenderer.sprite = ChestAnim[spriteCount++];
        }
        if (spriteCount == ChestAnim.Length)
        {
            chestPaper.SetActive(true);
        }
    }
}
