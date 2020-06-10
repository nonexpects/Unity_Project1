using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float currHp;
    float maxHp = 10f;
    public float HP
    {
        get { return currHp; }
        set {
            Mathf.Clamp(currHp, 0f, maxHp);
            currHp = value;
        }
    }
    

    private Color currColor;

    public Image hpBarImage;

    void Start()
    {
        HP = maxHp;
        currColor = Color.green;
        hpBarImage.color = currColor;
    }
    
    void Update()
    {
        hpBarImage.fillAmount = (float)HP / maxHp;

        HpBarColor();
    }

    private void HpBarColor()
    {
        if (hpBarImage.fillAmount <= .6f && hpBarImage.fillAmount >= .3f)
        {
            currColor = Color.yellow;
            hpBarImage.color = currColor;
        }
        else if(hpBarImage.fillAmount < .3f)
        {
            currColor = Color.red;
            hpBarImage.color = currColor;
        }
        else
        {
            currColor = Color.green;
            hpBarImage.color = currColor;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ENEMY" || collision.gameObject.tag == "BOSS")
        {
            HP -= 1f;
        }
    }
}
