using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMGR : MonoBehaviour
{
    //씬매니져 싱글톤화
    // 지역, 게임, 종료씬 모두 관리
    // 또한 씬이 변경되도록 삭제되면 안된다
    public static SceneMGR instance;
    private void Awake()
    {
        //씬매니저가 존재한다면
        //새로 생성되는 씬 매니저는 삭제하고 바로 빠져나와라
        if(instance)
        {
            //Destroy를 쓰면 눈에는 사라지지만 메모리상에서 언제 사라지는지 모른다
            //가비지 콜렉터가 돌면서 때가 되면 지우기 떄문
            //DestroyImmediate를 사용하면 바로 지워진다.
            DestroyImmediate(this);
            return;
        }
        //인스턴스가 null일경우 
        instance = this;
        //씬이 변환돼도 계속 살아있다
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
