using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private float initHp = 500.0f;
    private int socre = 100;
    private bool clear = false;
    public float currHp;
    public Image targetHpBar;
    public GameObject success;

    private void Start()
    {
        currHp = initHp;
        success.SetActive(false);
    }
    private void Update()
    {
        //targetHpBar.transform.LookAt(Camera.main.transform.forward);   
    }
    public void TargetDamage()
    {
        currHp -= Random.Range(5, 16);
        targetHpBar.fillAmount = currHp / initHp;

        if (targetHpBar.fillAmount < 0.2f)
        {
            targetHpBar.color = Color.red;
        } else if (targetHpBar.fillAmount < 0.5f)
        {
            targetHpBar.color = Color.yellow;
        }

        if(currHp <= 0f)
        {
            clear = true;
            GameManager.instance.AddScore(socre);
            GameManager.instance.GameClear(clear);
            Destroy(gameObject);
            success.SetActive(true);
        }

    }
}
