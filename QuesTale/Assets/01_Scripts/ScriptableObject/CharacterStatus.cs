using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="����", fileName ="����")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private float exp;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float mp;
    [SerializeField]
    private int ap;
    [SerializeField]
    private float attack;
    [SerializeField]
    private float defence;
    [SerializeField]
    private float crit;
    [SerializeField]
    private float speed;

    public int Lv { get { return level; } set { level = value; } }
    public float Exp { get { return exp; }set { exp = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float Mp { get { return mp; } set { mp = value; } }
    public int Ap { get { return ap; } set { ap = value; } }
    public float Atk { get { return attack; } set { attack = value; } }
    public float Def { get { return defence; } set { defence = value; } }
    public float Cri { get { return crit; } set { crit = value; } }
    public float Spd { get { return speed; } set { speed = value; } }


}
