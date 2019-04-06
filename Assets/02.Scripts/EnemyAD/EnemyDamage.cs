﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    private float hp = 1000.0f;
    public GameObject bloodEffect;
    public Image enemyHP;
    private int score = 5;
    EnemyAI enemyAI;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    void EnemyHitDamage(object[] _params)
    {
        CreateBloodEffect((Vector3)_params[0]);
        hp -= (int)_params[1];

        enemyHP.fillAmount = hp * 0.01f;

        if(hp<= 0)
        {
            enemyAI.state = EnemyAI.State.DIE;
            GameManager.instance.AddScore(score);
        }
    }

    private void FixedUpdate()
    {
        enemyHP.transform.LookAt(Camera.main.transform.forward);
    }
    void CreateBloodEffect(Vector3 position)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, position, Quaternion.identity);
        Destroy(blood1, 1.0f);
    }
}
