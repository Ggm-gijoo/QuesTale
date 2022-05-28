using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    enum BgmType
    {
        FIELD = 0,
        BATTLE,
        WIN
    }
    [SerializeField]
    AudioSource[] bgms;

    int nowEvent;

    public void BgmEvent() //상황에 따라 nowEvent를 바꾸고 bgms[nowEvent]를 플레이하면 스위치문 안 써도 되는데
    {
        switch(nowEvent)
        {
            case (int)BgmType.FIELD:
                GetComponent<AudioSource>().Stop();
                bgms[0].Play();
                break;
            case (int)BgmType.BATTLE:
                GetComponent<AudioSource>().Stop();
                bgms[1].Play();
                break;
            case (int)BgmType.WIN:
                GetComponent<AudioSource>().Stop();
                bgms[2].Play();
                break;
        }
    }


}
