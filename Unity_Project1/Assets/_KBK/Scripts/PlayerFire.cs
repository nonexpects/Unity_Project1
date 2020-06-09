using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //레이저를 발사하기 위해서는 라인렌더러 필요
    //선은 최소 2개의 점 필요(시작점, 끝점)
    LineRenderer lr;
    public GameObject bulletFactory;    //총알 프리팹(공장)
    public GameObject firePoint;        //총알 발사 위치
    RaycastHit hit;
    float currTime;
    float rayMaxTime = 1f;

    //사운드 재생
    private AudioSource audio;

    void Start()
    {
        //라인 렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.2f;
        //중요 !!!
        //게임 오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용

        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //Fire();
        FireRay();
    }

    //일정시간이 지나면 사라져야함
    public void FireRay()
    {
        //마우스 왼쪽 버튼 or 왼쪽 컨트롤
        if (Input.GetButtonDown("Fire1") && lr.enabled == false)
        {
            //사운드 재생 
            audio.Play();

            if (Physics.Raycast(transform.position, Vector3.up, out hit, 10f, ~(1 << 9)))
            {

                //라인렌더러 컴포넌트 활성화
                lr.enabled = true;
                //라인 시작점, 끝점
                lr.SetPosition(0, transform.position); //시작점Idx : 0
                lr.SetPosition(1, hit.point); //시작점Idx : 0
            }

        }


        if (lr.enabled) currTime += Time.deltaTime;

        if (currTime > rayMaxTime)
        {
            lr.enabled = false;
            currTime = 0;
        }

    }

    public void Fire()
    {
        //마우스 왼쪽 버튼 or 왼쪽 컨트롤
        //if(Input.GetButtonDown("Fire1"))
        {
            // 총알 게임 오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            //총알 오브젝트 위치 지정
            bullet.transform.position = firePoint.transform.position; // PlayerFire 스크립트가 붙어있는 오브젝트의 transform위치
        }
        //총알 공장(총알 프리팹)에서 총알 무한대로 찍어낼 수 있다
        //Instantiate() 함수로 프리팹 파일을 게임 오브젝트로 만든다
    }

    //레이저버튼 클릭시 
    public void OnLaserButtonClick()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 10f, ~(1 << 9)))
        {
            //라인렌더러 컴포넌트 활성화
            lr.enabled = true;
            //라인 시작점, 끝점
            lr.SetPosition(0, transform.position); //시작점Idx : 0
            lr.SetPosition(1, hit.point); //시작점Idx : 0
        }

        if (lr.enabled) currTime += Time.deltaTime;

        if (currTime > rayMaxTime)
        {
            lr.enabled = false;
            currTime = 0;
        }
    }

    //파이어버튼 클릭시 
    public void OnFireButtonClick()
    {
        // 총알 게임 오브젝트 생성
        GameObject bullet = Instantiate(bulletFactory);
        //총알 오브젝트 위치 지정
        bullet.transform.position = firePoint.transform.position; // PlayerFire 스크립트가 붙어있는 오브젝트의 transform위치
    }
}
