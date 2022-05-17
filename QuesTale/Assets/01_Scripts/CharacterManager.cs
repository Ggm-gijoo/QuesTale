using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus status;

    public bool IsDefence { set; get; } = false;

    public float StatusSpd
    {
        get => status.Spd;
        set => status.Spd = value;
    }
    public float StatusAtk
    {
        get => status.Atk;
        set => status.Atk = value;
    }
    public float StatusDef
    {
        get => status.Def;
        set => status.Def = value;
    }
    public int StatusAp
    {
        get => status.Ap;
        set => status.Ap = value;
    }
    public float hpNow;
    public float mpNow;

    private void Start()
    {
        hpNow = status.Hp;
        mpNow = status.Mp;
    }
}
