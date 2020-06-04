using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        target = GameObject.Find("Player");
       // StartCoroutine("Fire");
    }
    
    void Update()
    {
        AutoFire1();
        AutoFire2();
    }
    //플레이어 향해서 총알 발사
    private void AutoFire1()
    {
        if (!target) return;
        currTime += Time.deltaTime;
        if(currTime > fireTime)
        {
            //총알 생성
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = transform.position;
            //플레이어를 향하는 방향 구하기 (벡터 뺄셈)
            Vector3 dir = transform.position - target.transform.position;
            dir.Normalize();
            //총구의 방향도 맞춰준다(이게 중요) -> 총알이 Up으로 되어있으니 이걸 바꿔줘야함
            bullet.transform.up = -dir;
            //타이머 초기화
            currTime = 0f;
        }
    }
    //회전 총알
    private void AutoFire2()
    {
        currTime1 += Time.deltaTime;
        if(currTime1 > fireTime1)
        {
            for (int i = 0; i < bulletMax; i++)
            {
                GameObject bullet = Instantiate(bulletFactory);
                bullet.transform.position = transform.position;
                //360도 방향으로 총알 발사
                float angle = 360f / bulletMax;
                //총구 방향 정해줌
                bullet.transform.eulerAngles = new Vector3(0, 0, i * angle);
                
            }
            currTime1 = 0f;
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
