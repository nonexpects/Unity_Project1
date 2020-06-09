using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            BgmMgr.instance.PlayBGM("BGM1");
        }
        if (Input.GetKeyDown("2"))
        {
            BgmMgr.instance.PlayBGM("BGM2");
        }
        if (Input.GetKeyDown("3"))
        {
            BgmMgr.instance.CrossFadeBGM("BGM1", 3f);
        }
        if (Input.GetKeyDown("4"))
        {
            BgmMgr.instance.CrossFadeBGM("BGM2", 3f);
        }
    }
}
