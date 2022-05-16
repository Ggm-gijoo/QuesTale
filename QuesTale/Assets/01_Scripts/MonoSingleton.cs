using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<GM> : MonoBehaviour where GM : MonoBehaviour
{
    private static GM instance = null;
    private static object locker = new object();

    public static GM Instance
    {
        get 
        {
            lock(locker)//한 번에 하나의 스레드만 실행되게 해줌
            {
                if(instance == null)//instance가 비어있으면
                {
                    instance = FindObjectOfType<GM>();//GM에서 찾아줌

                    if(instance == null)//그래도 비어있으면 없다는 뜻이므로
                    {
                        instance = new GameObject(typeof(GM).ToString()).AddComponent<GM>();//다시 만들어줌
                    }
                }
                return instance;
            }
        }
    }

}
