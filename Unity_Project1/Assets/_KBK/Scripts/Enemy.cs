using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //위에서 밑으로 떨어지게 한다(똥피하기 느낌)
    // 충돌처리 (에너미랑 플레이어, 에너미랑 플레이어 총알)
    public GameObject fxFactory;
    
    public float speed = 4f;
    
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //자기자신도 없애고 충돌한 오브젝트도 없앤다.
        //Destroy(gameObject, 1f); // 2번째 인자는 duration
        Destroy(gameObject);
        if(collision.gameObject.name == "Bullet")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        GameManager.instance.AddScore();
        GameManager.instance.PlayExpBGM();
        ShowEffect();
    }

    private void ShowEffect()
    {
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        Destroy(fx, 1.5f);
    }
}
