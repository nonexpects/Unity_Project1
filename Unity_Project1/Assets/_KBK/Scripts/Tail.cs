using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    //꼬랑지가 플레이어를 따라다니려면 플레이어의 위치를 알아야 함
    public GameObject target;   //플레이어 오브젝트
    public GameObject bulletFactory;    //총알 프리팹(공장)
    private float speed = 5f;    //꼬랑지 속도

    bool followOn = false;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (followOn)
            FollowTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        followOn = true;
        StartCoroutine(Fire());
    }

    private void FollowTarget()
    {
        //벡터의 뺄셈으로 타겟 방향 구하기
        //방향 = 타겟 - 자기자신 (자기자신이 타겟을 바라보는 것)
        Vector3 dir = target.transform.position - transform.position;
        //뺄셈에는 Normalize필요
        //dir.Normalize();
        transform.Translate(dir * speed * Time.deltaTime);
    }

    IEnumerator Fire()
    {
        while(followOn)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.transform.position = transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
