using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillStatus[] skils;

    public void OnClickSkill(SkillStatus skill)
    {
        if (GameManager.Instance.characters[GameManager.Instance.Index].mpNow >= skill.UseMp && GameManager.Instance.characters[GameManager.Instance.Index].apNow >= skill.UseAp)
        {
            GameManager.Instance.characters[GameManager.Instance.Index].mpNow -= skill.UseMp;
            GameManager.Instance.characters[GameManager.Instance.Index].apNow -= skill.UseAp;
        }
        else GameManager.Instance.SkillCancel();
    }

    public void OnClickSlash() => OnClickSkill(skils[0]);
    public void OnClickGuillotine() => OnClickSkill(skils[1]);
    public void OnClickFireBall() => OnClickSkill(skils[2]);
}
