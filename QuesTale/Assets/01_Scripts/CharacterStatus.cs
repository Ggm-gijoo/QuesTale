using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Ω∫≈»", fileName ="Ω∫≈»")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField]
    private int level;
    [SerializeField]
    private float hp;
    [SerializeField]
    private float mp;
    [SerializeField]
    private float ap;
    [SerializeField]
    private float attack;
    [SerializeField]
    private float deffence;
    [SerializeField]
    private float speed;

    public int Lv { get { return level; } set { level = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float Mp { get { return mp; } set { mp = value; } }
    public float Ap { get { return ap; } set { ap = value; } }
    public float Atk { get { return attack; } set { attack = value; } }
    public float Def { get { return deffence; } set { deffence = value; } }
    public float Spd { get { return speed; } set { speed = value; } }


}
