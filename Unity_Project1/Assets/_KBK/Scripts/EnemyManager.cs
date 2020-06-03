using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // 에너미 매니저 역할?
    // 에너미 프리팹을 공장에서 찍어낸다
    // 에너미 스폰타임, 스폰 위치

    public GameObject enemyFactory;     //에너미 공장(프리팹)  
    public GameObject[] spawnPoints;       //스폰위치
    float spawnTime = 1f;               //스폰타임(몇초에 한번씩)
    float curTime;                      //누적타임
    
    void Update()
    {
        //에너미 생성
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        //몇초에 한 번씩 이벤트 발동
        //시간 누적타임으로 계산
        // 게임에서 정말 자주 사용
        curTime += Time.deltaTime;
        if(curTime > spawnTime)
        {
            //누적된 현재 시간을 0.0초로 초기화(반드시 해줘야 한다)
            curTime = 0f;
            //스폰타임을 랜덤으로
            spawnTime = Random.Range(.5f, 2f);

            //for (int i = 0; i < spawnPoints.Length; i++)
            //{
            //    GameObject enemy = Instantiate(enemyFactory);
            //    enemy.transform.position = spawnPoints[i].transform.position;
            //}
            GameObject enemy = Instantiate(enemyFactory);
            int idx = Random.Range(0, spawnPoints.Length);
            enemy.transform.position = spawnPoints[idx].transform.position;
        }
    }
}
