using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    //여기에서 싱글톤으로 접근
    public void StartButtonClick()
    {
        SceneMGR.instance.LoadScene("GameScene");
    }

    public void OnMenuButtonClick()
    {

    }
    public void OnOptionButtonClick()
    {

    }
    
}
