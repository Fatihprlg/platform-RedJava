using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermiKontrol : MonoBehaviour
{
    GameObject enemyObject;
    EnemyControl enemy;
    Rigidbody2D fizik;

    void Start()
    {
        enemyObject = GameObject.FindGameObjectWithTag("dusman");
        Debug.Log(enemyObject.tag);
        enemy = enemyObject.GetComponent<EnemyControl>();
        fizik = GetComponent<Rigidbody2D>();
        fizik.AddForce(enemy.getYon() * 1000);
    }

}
