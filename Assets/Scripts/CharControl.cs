using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharControl : MonoBehaviour {


    public Sprite[] idleAnim;
    public Sprite[] runAnim;
    public Sprite[] jumpAnim;

    public Text healthText;

    public Image blackBackground;

    SpriteRenderer spriteRender;
    Rigidbody2D phys;
    coinAnimation coinAnim;

    int idleAnimCount = 0;
    int runAnimCount = 0;
    int health = 100;
    
    bool oneJump = true;

    float horizontal = 0;
    float idleAnimTime = 0;
    float runAnimTime = 0;
    float blackBackgroundCount = 0;
    float sceneOverTime = 0;

    Vector3 vec;
    Vector3 lastCamPos;
    Vector3 firstCamPos;

    GameObject cam;

    void Start() 
    {
        spriteRender = GetComponent<SpriteRenderer>();
        phys = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        firstCamPos = cam.transform.position - transform.position;
        coinAnim = GameObject.FindGameObjectWithTag("coin").GetComponent<coinAnimation>();
        healthText.text = "CAN  " + health;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (oneJump)
            {
                phys.AddForce(new Vector2(0, 500));
                oneJump = false;
            }
        }
    }
    void FixedUpdate() 
    {
        charMove();
        Animation();
        if (health <= 0)
        {

            Time.timeScale = 0.4f;
            healthText.enabled = false;
            blackBackgroundCount += 0.03f;
            blackBackground.color = new Color(0, 0, 0, blackBackgroundCount);
            sceneOverTime += Time.deltaTime;
            if (sceneOverTime > 1)
            {
                SceneManager.LoadScene("mainMenu");
            }
            
        }
    }

    void LateUpdate()
    {
        if (!coinAnim.gameOver)
        {
            camControl();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        oneJump = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
         if(col.gameObject.tag == "mermi")
        {
            health--;
            healthText.text = "CAN  " + health;
        }
        if (col.gameObject.tag == "dusman")
        {
            health -= 10;
            healthText.text = "CAN  " + health;
        }
        
    }

    void charMove()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal * 10, phys.velocity.y, 0);
        phys.velocity = vec;
    }

    void camControl()
    {
        lastCamPos = firstCamPos + transform.position;
        cam.transform.position = Vector3.Lerp(cam.transform.position, lastCamPos, 0.1f);
    }

    void Animation()
    {

        if (oneJump)
        {
            if (horizontal == 0)
            {
                idleAnimTime += Time.deltaTime;

                if (idleAnimTime > 0.05f)
                {
                    spriteRender.sprite = idleAnim[idleAnimCount++];
                    if (idleAnimCount == idleAnim.Length)
                    {
                        idleAnimCount = 0;
                    }
                    idleAnimTime = 0;
                }
            }
            else if (horizontal > 0)
            {
                runAnimTime += Time.deltaTime;

                if (runAnimTime > 0.01f)
                {
                    spriteRender.sprite = runAnim[runAnimCount++];
                    if (runAnimCount == runAnim.Length)
                    {
                        runAnimCount = 0;
                    }
                    runAnimTime = 0;
                }
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                runAnimTime += Time.deltaTime;

                if (runAnimTime > 0.01f)
                {
                    spriteRender.sprite = runAnim[runAnimCount++];
                    if (runAnimCount == runAnim.Length)
                    {
                        runAnimCount = 0;
                    }
                    runAnimTime = 0;
                }
                transform.localScale = new Vector3(-1, 1, 1);
            }
        } else {
            if (phys.velocity.y > 0)
            {
                spriteRender.sprite = jumpAnim[0];
            } 
            else 
            {
                spriteRender.sprite = jumpAnim[1];
            }
            if (horizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

        }
    }

   
    
}
