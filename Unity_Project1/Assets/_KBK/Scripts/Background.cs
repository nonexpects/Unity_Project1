using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material material; //백그라운드 매터리얼
    public float scrollSpeed = 0.1f; //스크롤 속도
    //Vector2 offset;     
    // Start is called before the first frame update
    void Start()
    {
        //메터리얼은 렌더러 컴포넌트 안에 속성으로 있다.
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //offset.y += Time.deltaTime / 2;
        //material.mainTextureOffset = offset;
        BackgroundScroll();
    }

    //백그라운드 스크롤
    private void BackgroundScroll()
    {
        //매터리얼의 메인텍스쳐 오프셋은 vector2로 만들어져 있다.
        Vector2 offset = material.mainTextureOffset;
        //offset.y의 값만 보정해주면 된다
        offset.Set(0, offset.y + (scrollSpeed * Time.deltaTime));
        //다시 메테리얼 오프셋에 담음
        material.mainTextureOffset = offset; // << Casting 이라고 함
    }
}
