using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private float initHp = 500.0f;
    public float currHp;
    public Image targetHpBar;
    public Text success;

    private void Start()
    {
        currHp = initHp;
        success.enabled = false;
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
            Destroy(gameObject);
            success.enabled = true;
        }

    }
}
