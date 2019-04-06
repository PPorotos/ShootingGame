using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    private float currHp;
    private float initHp = 1000.0f;
    public GameObject bloodEffect;
    public GameObject lookCam;
    public Image enemyHP;
    private int score = 5;
    EnemyAI enemyAI;


    private void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        currHp = initHp;
    }
    void EnemyHitDamage(object[] _params)
    {
        CreateBloodEffect((Vector3)_params[0]);
        currHp -= (int)_params[1];

        enemyHP.fillAmount = currHp /initHp;

        if(currHp <= 0)
        {
            enemyAI.state = EnemyAI.State.DIE;
            GameManager.instance.AddScore(score);
        }
    }

    private void FixedUpdate()
    {
        lookCam.transform.LookAt(Camera.main.transform);
    }
    void CreateBloodEffect(Vector3 position)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, position, Quaternion.identity);
        Destroy(blood1, 1.0f);
    }
}
