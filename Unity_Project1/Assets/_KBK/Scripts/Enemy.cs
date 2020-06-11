using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //위에서 밑으로 떨어지게 한다(똥피하기 느낌)
    // 충돌처리 (에너미랑 플레이어, 에너미랑 플레이어 총알)
    GameObject fxFactory;
    GameObject player;

    public float speed = 4f;

    private void Start()
    {
        fxFactory = Resources.Load("FX/" + "ExplosionFireball") as GameObject;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //자기자신도 없애고 충돌한 오브젝트도 없앤다.
        //Destroy(gameObject, 1f); // 2번째 인자는 duration
        
        if(collision.gameObject.name.Contains("Bullet"))
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
        

        if(collision.gameObject == player)
        {
            player.GetComponent<Player>().PlayerDamaged();
        }

        EnemyDead();
    }

    public void EnemyDead()
    {
        GameManager.instance.AddScore();
        GameManager.instance.PlayExpBGM();
        GameObject fx = Instantiate(fxFactory);
        fx.transform.position = transform.position;
        Destroy(fx, 1f);
        Destroy(gameObject);
    }
}
