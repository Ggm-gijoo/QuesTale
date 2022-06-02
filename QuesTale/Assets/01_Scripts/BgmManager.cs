using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{

    private AudioSource[] bgms;

    public void BgmEvent(int nowEvent)
    {
        bgms = GetComponentsInChildren<AudioSource>(true);
        for (int i = 0; i < bgms.Length; i++)
            bgms[i].gameObject.SetActive(false);
        bgms[nowEvent].gameObject.SetActive(true);
    }


}
