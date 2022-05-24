using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "스킬", fileName = "스킬")]

public class SkillStatus : ScriptableObject
{
    [SerializeField]
    private float s_power;

    [SerializeField]
    private float s_useMp;

    [SerializeField]
    private float s_critPercent;

    [SerializeField]
    private int s_useAp = 1;

    public float Power { get { return s_power; } set { s_power = value; } }

    public float UseMp { get { return s_useMp; } set { s_useMp = value; } }

    public float CritP { get { return s_critPercent; } set { s_critPercent = value; } }

    public int UseAp { get { return s_useAp; } set { s_useAp = value; } }

}
