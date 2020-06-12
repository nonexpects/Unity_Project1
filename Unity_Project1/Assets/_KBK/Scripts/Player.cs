using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Light damageLight;

    float currHp;
    float maxHp = 10f;

    public bool invincible;
    private float currInvincibleTime;

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

    GameObject gameOver;

    public GameObject hitFx;

    void Start()
    {
        HP = maxHp;
        currColor = Color.green;
        hpBarImage.color = currColor;

        damageLight = gameObject.transform.Find("Player Body").transform.Find("DamageLight").GetComponent<Light>();

        damageLight.intensity = 5;
        damageLight.enabled = false;

        invincible = false;
        gameOver = GameObject.Find("GameOver");
        gameOver.SetActive(false);
    }
    
    void Update()
    {
        hpBarImage.fillAmount = (float)HP / maxHp;

        HpBarColor();

        if(invincible)
        {
            currInvincibleTime += Time.deltaTime;
            if(currInvincibleTime > 1f)
            {
                invincible = false;
                damageLight.enabled = false;
                currInvincibleTime = 0f;
            }
        }

        if(HP <= 0f)
        {
            HP = 0f;
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("E_Bullet"))
        {
            //총알 오브젝트는 비활성시킨다
            collision.gameObject.SetActive(false);
            collision.gameObject.transform.eulerAngles = Vector3.zero;
            //오브젝트 풀에 추가만 해준다
            Boss bs = GameObject.Find("Boss").GetComponent<Boss>();
            bs.bossBulletPool.Enqueue(collision.gameObject);

            PlayerDamaged(collision);
        }

        
    }
    
    public void PlayerDamaged(Collision coll)
    {
        if (!invincible)
        {
            invincible = true;
            StartCoroutine(Flicker());
            HP -= 1f;
        }
        GameObject fx = Instantiate(hitFx);
        fx.transform.position = coll.transform.position;
        Destroy(fx, 1f);
        currInvincibleTime = 0f;
    }

    IEnumerator Flicker()
    {
        while (invincible)
        {
            if (!damageLight.enabled) damageLight.enabled = true;
            else damageLight.enabled = false;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
