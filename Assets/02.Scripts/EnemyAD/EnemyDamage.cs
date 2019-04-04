using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float hp = 1000.0f;
    public GameObject bloodEffect;
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
        if(hp<= 0)
        {
            GameManager.instance.AddScore(score);
            enemyAI.state = EnemyAI.State.DIE;
        }
    }

    void CreateBloodEffect(Vector3 position)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, position, Quaternion.identity);
        Destroy(blood1, 1.0f);
    }
}
