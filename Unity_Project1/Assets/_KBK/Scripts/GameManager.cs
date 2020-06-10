using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro; //텍스트메쉬프로 쓸 시 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    PlayerPrefs scoreData;
    public Text scoreText;
    public Text highScoreText;
    //public TextMeshProGUI textTxt; //텍스트메쉬프로 텍스트

    private string keyString = "HighScore";
    int score;
    int highscore;

    public GameObject player;

    AudioSource audio;
    public AudioClip expfx;



    private void Awake()
    {
        if (!instance)
            instance = this;
        highscore = PlayerPrefs.GetInt(keyString, 0);
    }

    void Start()
    {
        highScoreText.text = "HIGHEST : " + highscore.ToString("000000");
        scoreText.text = "SCORE : " + score.ToString("000000");
        audio = GetComponent<AudioSource>();

    }

    public void AddScore(int num = 1)
    {
        score += num;
        scoreText.text = "SCORE : " + score.ToString("000000");
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= highscore)
        {
            highscore = score;
            highScoreText.text = "HIGHSCORE : " + highscore.ToString("000000");
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(keyString, highscore);
    }

    public void PlayExpBGM()
    {
        audio.clip = expfx;
        audio.PlayOneShot(expfx);
    }

}
