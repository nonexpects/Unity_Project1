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

    //오브젝트 풀링에 사용할 최대 총알 갯수(사이즈)
    int poolSize = 20;
    //int fireIndex;
    //오브젝트 풀링
    //1. 배열
    //GameObject[] bulletPool;
    //2. 리스트
    //public List<GameObject> bulletPool;
    //3. 큐
    public Queue<GameObject> bulletPool;

    void Start()
    {
        //라인 렌더러 컴포넌트 추가
        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.2f;
        //중요 !!!
        //게임 오브젝트는 활성화 비활성화 => SetActive() 함수 사용
        //컴포넌트는 enabled 속성 사용

        audio = GetComponent<AudioSource>();

        //오브젝트 풀링 초기화
        InitObjectPooling();
    }
    //오브젝트 풀링 초기화
    private void InitObjectPooling()
    {
        //1. 배열
        //bulletPool = new GameObject[poolSize];
        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool[i] = bullet;
        //}

        //2. 리스트
        //bulletPool = new List<GameObject>();
        //for (int i = 0; i < poolSize; i++)
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool.Add(bullet);
        //}

        //3. 큐
        bulletPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
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
        if(Input.GetButtonDown("Fire1"))
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
        //1. 배열 오브젝트풀링으로 총알발사
        //bulletPool[fireIndex].SetActive(true);
        //bulletPool[fireIndex].transform.position = firePoint.transform.position;
        //bulletPool[fireIndex].transform.up = firePoint.transform.up;    //방향이 잘못됐을 수도 있기 때문에
        //fireIndex++;
        //
        //if (fireIndex >= poolSize)
        //{
        //    fireIndex = 0;
        //}

        //2. 리스트 오브젝트 풀링으로 총알발사
        //if(bulletPool.Count > 0)
        //{
        //    GameObject bullet = bulletPool[0];
        //    bullet.SetActive(true);
        //    bullet.transform.position = firePoint.transform.position;
        //    bullet.transform.up = firePoint.transform.up;
        //    //오브젝트 풀에서 빼준다
        //    bulletPool.Remove(bullet);
        //}
        //else // 오브젝트 풀링 비어서 총알이 하나도 없으니 풀 크기를 눌려준다
        //{
        //    GameObject bullet = Instantiate(bulletFactory);
        //    bullet.SetActive(false);
        //    bulletPool.Add(bullet);
        //}

        //4. 큐 오브젝트에서 풀링
        if(bulletPool.Count>0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            bullet.transform.position = firePoint.transform.position;
            bullet.transform.up = firePoint.transform.up;
        }
        else
        {
            //총알 오브젝트 생성
            GameObject bullet = Instantiate(bulletFactory);
            bullet.SetActive(false);
            //생성된 총알 오브젝트를 풀에 넣는다
            bulletPool.Enqueue(bullet);
        }

        // 총알 게임 오브젝트 생성
        //GameObject bullet = Instantiate(bulletFactory);
        //총알 오브젝트 위치 지정
        //bullet.transform.position = firePoint.transform.position; // PlayerFire 스크립트가 붙어있는 오브젝트의 transform위치
    }
}
