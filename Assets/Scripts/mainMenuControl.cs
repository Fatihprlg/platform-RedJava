using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuControl : MonoBehaviour
{
    GameObject level1, level2, level3;

    void Start()
    {
        level1 = GameObject.Find("level1");
        level2 = GameObject.Find("level2");
        level3 = GameObject.Find("level3");

        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
    }

    public void buttonActions(int buttonID)
    {
        if (buttonID == 1)
        {
            SceneManager.LoadScene("sampleScene");
        }
        else if (buttonID == 2)
        {
            level1.SetActive(true);
            level2.SetActive(true);
            level3.SetActive(true);
        }
        else if (buttonID == 3)
        {
            Application.Quit();
        }
    }
}
