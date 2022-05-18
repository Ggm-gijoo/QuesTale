using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    [Header("플레이어 스테이터스 데이터")]
    [SerializeField]
    private CharacterStatus[] playerStatuses;
    
    [Space(2f)]

    [Header("적 스테이터스 데이터")]
    [SerializeField]
    private CharacterStatus[] enemyStatuses;
}
