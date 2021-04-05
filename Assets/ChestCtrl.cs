using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestCtrl : MonoBehaviour
{
    public Sprite[] ChestAnim;
    SpriteRenderer spriteRenderer;
    SpriteRenderer paperSprite;
    public Image paperImg;
    int spriteCount = 0;
    GameObject chestPaper;
    AudioSource audioSource;
    bool isEntered = false;
    void Start()
    {
        switch (this.name)
        {
            case "Chest":
                chestPaper = GameObject.FindGameObjectWithTag("paper1");
                break;
            case "Chest2":
                chestPaper = GameObject.FindGameObjectWithTag("paper2");
                break;
            case "Chest3":
                chestPaper = GameObject.FindGameObjectWithTag("paper3");
                audioSource = GetComponent<AudioSource>();
                break;
            default:
                break;
        }
        paperSprite = chestPaper.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (isEntered)
        {
            ChestAnimation();
            if (Input.GetKeyDown(KeyCode.B))
            {
                paperImg.enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                paperImg.enabled = false;
                if (this.name == "Chest3")
                {
                    audioSource.Play();
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isEntered = true;
            Debug.Log("Girdi");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isEntered = false;
            Debug.Log("Çıktı");
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
            paperSprite.enabled = true;
        }
    }
}
