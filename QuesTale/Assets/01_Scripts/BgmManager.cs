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

    [SerializeField]
    private int nowEvent = 0;

    public void BgmEvent()
    {
        //GetComponentInChildren<AudioSource>().Stop();//자식의 오디오소스를 꺼야함
        bgms[nowEvent].Play();
    }


}
