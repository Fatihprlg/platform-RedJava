using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinAnimation : MonoBehaviour
{
    public Sprite[] coins;
    SpriteRenderer spriteRenderer;
    Rigidbody2D phys;
    public Animator animator;
    public bool gameOver = false;
    int coinCount = 0;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        phys = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        coinAnim();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            phys.gravityScale = 1;
            animator.SetTrigger("gameOver");
            gameOver = true;
        }
    }

    void coinAnim()
    {
        spriteRenderer.sprite = coins[coinCount++];
        if (coinCount == coins.Length)
        {
            coinCount = 0;
        }
    }
}
