using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    [Header("�÷��̾� �������ͽ� ������")]
    [SerializeField]
    private CharacterStatus[] playerStatuses;
    
    [Space(2f)]

    [Header("�� �������ͽ� ������")]
    [SerializeField]
    private CharacterStatus[] enemyStatuses;
}
