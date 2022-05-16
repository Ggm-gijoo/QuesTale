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
            lock(locker)//�� ���� �ϳ��� �����常 ����ǰ� ����
            {
                if(instance == null)//instance�� ���������
                {
                    instance = FindObjectOfType<GM>();//GM���� ã����

                    if(instance == null)//�׷��� ��������� ���ٴ� ���̹Ƿ�
                    {
                        instance = new GameObject(typeof(GM).ToString()).AddComponent<GM>();//�ٽ� �������
                    }
                }
                return instance;
            }
        }
    }

}
