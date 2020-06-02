using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //총알클래스 하는 일
    // 플레이어가 발사버튼 루므녀
    // 총알이 생성된 후 발사하고 싶은 방향(위쪽)으로 움직인다

    public float speed = 10f;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        
    }

    //카메라 화면밖으로 나가서 보이지 않게 되면 호출되는 이벤트 함수
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
