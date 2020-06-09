using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmMgr : MonoBehaviour
{
    //BgmMgr 싱글톤화
    //모든 씬에서 사용가능해야 하니 BgmMgr을 삭제하면 안됨
    public static BgmMgr instance;
    private void Awake()
    {
        if(instance)
        {
            DestroyImmediate(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    Dictionary<string, AudioClip> bgmTable; //딕셔너리는 Table이라고 많이씀

    //BGM에도 crossFade를 줌(씬 전환시 뚝 끊기므로) => 2개의 BGM이 필요하다 (점점 작아지는 음악 / 점점 커지는 음악)

    AudioSource audioMain;      //메인 오디오
    AudioSource audioSub;       //서브 오디오 (BGM 교체용) => 2개를 switching하면서 처리

    //어트리뷰트
    [Range(0, 1f)]  // 0~1사이값만 얻을 수 있게 조정 (스크롤바 형태로 Inspector창에 노출시킴)
    public float masterVolume = 1f;
    float volumeMain = 0f;
    float volumeSub = 0f;
    float crossFameTime = 5f;       //2개의 BGM을 섞는 시간

    void Start()
    {
        //BGM테이블 생성
        bgmTable = new Dictionary<string, AudioClip>();
        //오디오 소스 코드로 추가
        audioMain = gameObject.AddComponent<AudioSource>();
        audioSub = gameObject.AddComponent<AudioSource>();
        //AddComponent <= Inspector 창에 있는 AddComponent와 같은 기능
        //오디오 소스 볼륨 0으로 초기화
        audioMain.volume = 0f;
        audioSub.volume = 0f;
    }

    private void Update()
    {
        if(audioMain.isPlaying)
        {
            if (volumeMain < 1f)
            {
                volumeMain += Time.deltaTime / crossFameTime;
                if (volumeMain >= 1f) volumeMain = 1f;
            }
            //서브오디오 볼륨 내리기
            if(volumeSub > 0f)
            {
                volumeSub -= Time.deltaTime / crossFameTime;
                if(volumeSub <= 0f)
                {
                    volumeSub = 0f;
                    //서브오디오 정지
                    audioSub.Stop();
                }
            }
        }

        //볼륨 조정
        audioMain.volume = volumeMain * masterVolume;
        audioSub.volume = volumeSub * masterVolume;
    }

    public void PlayBGM(string bgmName)
    {
        if (bgmTable.ContainsKey(bgmName) == false)
        {
            //유니티 엔진에서 특별한 기능의 Resources 폴더가 존재함
            //어디에서든 파일을 로드할 수 있다.
            //단, 스펠링 주의

            // Resources/BGM/폴더 안에서 오디오클립찾는다.
            //AudioClip bgm = (AudioClip)Resources.Load("BGM" + bgmName);
            AudioClip bgm = Resources.Load("BGM/" + bgmName) as AudioClip;

            //리소스 폴더에 bgm이 없다면 딕셔너리에 추가하지말고 빠져나와라
            //오디오 클립이 없으니 세팅할 수 없다
            if (bgm == null) return;

            //딕셔너리에 bgmTable의 키값으로 bgm을 추가한다
            bgmTable.Add(bgmName, bgm);
        }

        //메인오디오의 클립에 새로운 오디오 클립을 변경
        audioMain.clip = bgmTable[bgmName];
        //메인오디오 플레이하기
        audioMain.Play();

        //볼륨값 세팅
        volumeMain = 1f;
        volumeSub = 0f;
    }

    //bgm 크로스페이드 플레이
    public void CrossFadeBGM(string bgmName, float cfTime = 1f)
    {
        if (bgmTable.ContainsKey(bgmName) == false)
        {
            //유니티 엔진에서 특별한 기능의 Resources 폴더가 존재함
            //어디에서든 파일을 로드할 수 있다.
            //단, 스펠링 주의

            // Resources/BGM/폴더 안에서 오디오클립찾는다.
            //AudioClip bgm = (AudioClip)Resources.Load("BGM" + bgmName);
            AudioClip bgm = Resources.Load("BGM/" + bgmName) as AudioClip;

            //리소스 폴더에 bgm이 없다면 딕셔너리에 추가하지말고 빠져나와라
            //오디오 클립이 없으니 세팅할 수 없다
            if (bgm == null) return;

            //딕셔너리에 bgmTable의 키값으로 bgm을 추가한다
            bgmTable.Add(bgmName, bgm);
        }

        //크로스페이드 타임
        crossFameTime = cfTime;

        //메인오디오에서 플레이 되고 있는걸 서브오디오로 변경 (switching)
        AudioSource temp = audioMain;
        audioMain = audioSub;
        audioSub = temp;

        //볼륨값도 스위칭
        float tempVolume = volumeMain;
        volumeMain = volumeSub;
        volumeSub = tempVolume;

        //메인오디오의 클립에 새로운 오디오 클립을 연결한다
        audioMain.clip = bgmTable[bgmName];
        //메인오디오 플레이하기
        audioMain.Play();
    }

    //일시정지
    public void PauseBGM()
    {
        audioMain.Pause();
    }
    //다시재생
    public void ResumeBGM()
    {
        audioMain.Play();
    }

}
