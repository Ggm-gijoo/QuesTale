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
        //GetComponentInChildren<AudioSource>().Stop();//�ڽ��� ������ҽ��� ������
        bgms[nowEvent].Play();
    }


}
