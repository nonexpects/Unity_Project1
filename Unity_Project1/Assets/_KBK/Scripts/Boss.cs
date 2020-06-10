using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    //보스 총알 발사
    //1. 플레이어를 향해서 총알 발사
    //2. 회전총알 발사

    GameObject target;
    public GameObject bulletFactory;    //에너미 총알은 따로 만드는게 맞음(충돌처리때문)
    public float fireTime = 1f;              //1초에 한번씩 발사
    float currTime = 0f;
    public float fireTime1 = 1.5f;
    float currTime1 = 0f;
    public int bulletMax = 10;

    bool BossAppear = false;

    float currHp;
    float maxHp = 50f;

    WaitForSeconds wfs;
    WaitForSeconds wfs1;
    WaitForSeconds wfs2;

    Vector3 spawnPoint = new Vector3(0, 2.6f, 0);

    public GameObject bossHpUI;
    Image bossHPBar;

    public Queue<GameObject> bossBulletPool;
    GameObject poolParent;

    private void Awake()
    {
        poolParent = new GameObject("BossBullets");
    }

    void Start()
    {
        target = GameObject.Find("Player");
        // StartCoroutine("Fire");
        transform.position = new Vector3(0, 6.5f, 0);
        wfs = new WaitForSeconds(1f);
        wfs1 = new WaitForSeconds(0.5f);
        wfs2 = new WaitForSeconds(2f);
        bossHPBar = bossHpUI.GetComponentInChildren<Image>();
        bossHpUI.SetActive(false);

        bossBulletPool = new Queue<GameObject>();
        for (int i = 0; i < 50; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.parent = poolParent.transform;
            bullet.SetActive(false);
            bossBulletPool.Enqueue(bullet);
        }
    }
    
    void Update()
    {
        if(Time.time > 10f)
        {
            StartCoroutine(BossAppearance());
        }

        if(transform.position.y <= spawnPoint.y && !BossAppear)
        {
            BossAppear = true;
            currHp = maxHp;
            bossHpUI.SetActive(true);
            StartCoroutine(AutoFire1());
            StartCoroutine(AutoFire2());
        }
    }
    //플레이어 향해서 총알 발사
    IEnumerator AutoFire1()
    {

        while(currHp != 0)
        {
            if (bossBulletPool.Count > 0)
            {
                GameObject bullet = bossBulletPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = transform.position;
                //플레이어를 향하는 방향 구하기 (벡터 뺄셈)
                Vector3 dir = transform.position - target.transform.position;
                dir.Normalize();
                //총구의 방향도 맞춰준다(이게 중요) -> 총알이 Up으로 되어있으니 이걸 바꿔줘야함
                bullet.transform.up = -dir;
            }
            //else
            //{
            //    //총알 오브젝트 생성
            //    GameObject bullet = Instantiate(bulletFactory);
            //    bullet.SetActive(false);
            //    //생성된 총알 오브젝트를 풀에 넣는다
            //    bossBulletPool.Enqueue(bullet);
            //}
            yield return wfs1;
        }
    }
    //회전 총알
    IEnumerator AutoFire2()
    {
        while (currHp != 0)
        {
            for (int i = 0; i < bulletMax; i++)
            {
                GameObject bullet = bossBulletPool.Dequeue();
                bullet.transform.position = transform.position;
                //360도 방향으로 총알 발사
                float angle = 360f / bulletMax;
                //총구 방향 정해줌
                bullet.transform.eulerAngles = new Vector3(0, 0, i * angle);
            }

            yield return wfs2;
        }
               
    }

    IEnumerator BossAppearance()
    {
        while (transform.position.y > spawnPoint.y)
        {
            Vector3 pos = transform.position;
            pos.y -= Time.deltaTime;
            transform.position = pos;
            
            yield return wfs;
        }

        
    }

    public void BossHpLoss()
    {
        if (!BossAppear) return;
        currHp--;

        bossHPBar.fillAmount = (float)currHp / maxHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            collision.gameObject.SetActive(false);
            BossHpLoss();
        }
    }

    //IEnumerator Fire()
    //{
    //    while(target)
    //    {
    //        GameObject bullet = Instantiate(bulletFactory);
    //        Vector3 dir = transform.position - target.transform.position;
    //        bullet.transform.position = transform.position;
    //        bullet.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);
    //        bullet.transform.Translate(Vector3.down * Time.deltaTime * 2f);
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
