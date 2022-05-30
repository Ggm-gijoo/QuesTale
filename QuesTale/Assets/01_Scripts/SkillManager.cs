using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillStatus[] skils;

    public void OnClickSkill(SkillStatus skill)
    {
        
    }

    public void OnClickSlash() => OnClickSkill(skils[0]);
    public void OnClickGuillotine() => OnClickSkill(skils[1]);
    public void OnClickFireBall() => OnClickSkill(skils[2]);
}
