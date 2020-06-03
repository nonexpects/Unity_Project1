using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    //트리거 감지 후 해당 오브젝트 삭제

    private void OnTriggerEnter(Collider other)
    {
        //이곳에서 트리거에 감지된 오브젝트 제거하기 (총알, 에너미)
        Destroy(other.gameObject);
    }
}
