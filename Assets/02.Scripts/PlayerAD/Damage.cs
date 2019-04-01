using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private float initHp = 100.0f;
    public float currHp;
    public Image hpBar;

    private void Start()
    {
        currHp = initHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == bulletTag)
        {
            Destroy(other.gameObject);

            currHp -= 5.0f;
            hpBar.fillAmount = currHp / initHp;
            if (hpBar.fillAmount < 0.2f)
            {
                hpBar.color = Color.red;
            }
            else if (hpBar.fillAmount < 0.5f)
            {
                hpBar.color = Color.yellow;
            }

            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }
    void PlayerDie()
    {
        Debug.Log("Player Die");
    }
}
