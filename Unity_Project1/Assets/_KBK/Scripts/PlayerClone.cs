using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : MonoBehaviour
{
    //아이템 먹어서 보조 비행기가 생기도록 해야 한다
    //보조 비행기는 일정시간마다 자동으로 총알발사한다

    public GameObject clone;
    public GameObject bulletFactory;

    float curTime;
    float fireTime = 3f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        CreateClone();
        AutoFire();
    }
    
    private void CreateClone()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            clone.SetActive(true);
        }
    }

    private void AutoFire()
    {
        if(clone.activeSelf == true) //클론이 액티브 상태일 때 자동 발사하기
        {
            curTime += Time.deltaTime;
            if(curTime > fireTime)
            {
                //당연히 curTime 0으로 초기화
                curTime = 0f;
                //GameObject bullet1 = Instantiate(bulletFactory);
                //bullet1.transform.position = GameObject.Find("LeftCube").transform.position;
                //아래는 clone의 transform에서 find함수 쓰면 자식 오브젝트 다 찾을 수 있음
                //bullet1.transform.position = clone.transform.Find("LeftCube").transform.position;
                //bullet1.transform.position = clone.transform.GetChild(0).position; //인덱스값으로 찾음

                GameObject[] bullet = new GameObject[clone.transform.childCount];
                for (int i = 0; i < clone.transform.childCount; i++)
                {
                    bullet[i] = Instantiate(bulletFactory);
                    bullet[i].transform.position = clone.transform.GetChild(i).position;
                }
            }
        }
    }
}
