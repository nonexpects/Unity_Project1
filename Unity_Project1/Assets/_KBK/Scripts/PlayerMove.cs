using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // 플레이어 이동속도

    public Vector2 margin; // 뷰포트 좌표는 0f ~ 1f 사이의 값

    void Start()
    {
        margin = new Vector2(0.08f, 0.05f);
    }
    
    void Update()
    {
        Move();
       
    }

    //플레이어 이동 
    private void Move()
    {
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        
        //======================= 이 동 방 법 들 ============================//]
        transform.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0f);

        // 위에건 노말라이즈가 안됨
        // 벡터의 뺄셈가지고 할 땐(방향) Normalize 필수이나 걍 이동할 때는 오히려 Normalize하면 이상해짐
        // 덧셈가지고도 방향 구하기 가능
        //Vector3 dir = Vector3.right * h + Vector3.up * v;
        //Vector3 dir = new Vector3(h, v, 0);
        //dir.Normalize();
        //transform.Translate(dir * speed * Time.deltaTime);

        //위치 = 현재위치 + (방향 * 시간)
        //P = P0 + vt
        //transform.position = transform.position + (dir * speed * Time.deltaTime);
        //transform.position += dir * speed * Time.deltaTime;
        //=================================================================//
        MoveInScreen();
    }

    private void MoveInScreen()
    {
        //방법 크게 3가지
        //1. 화면밖의 공간에 큐브 4개 만들어서 배치
        // 리지드 바디 충돌체로 이동 못하게 막기

        //2. 플레이어 포지션으로 이동처리
        //transform.position.x = 100 << 값은 들고올 수 있지만 변경은 안됨
        //Vector3를 선언후 그 값에 현재 위치값 대입해서 다시 transfor.position에 대입할 수 있음
        //==> 이것을 캐스팅(Casting)이라고 함
        //Vector3 position = transform.position;
        //position.x = Mathf.Clamp(position.x, -2.5f, 2.5f);
        //position.y = Mathf.Clamp(position.y, -3.5f, 5.5f);
        //transform.position = position;

        //3. 메인카메라 뷰포트 가져와서 처리(우린 이걸 사용)
        //스크린좌표 : 왼쪽 하단(0,0), 우측 상단이(maxX, maxY)
        //뷰포트좌표 : 왼쪽 하단(0,0), 우측 상단이(1.0f, 1.0f) < 뷰포트는 0~1사이 값으로 처리됨
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position); // 0f~1f사이 값으로 가지고 옴
        position.x = Mathf.Clamp(position.x, 0f + margin.x, 1f - margin.x);
        position.y = Mathf.Clamp(position.y, 0f + margin.y, 1f - margin.y);
        transform.position = Camera.main.ViewportToWorldPoint(position);
    }
}
