using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillStatus[] skils;

    public void OnClickSkill(SkillStatus skill)
    {
        if (GameManager.Instance.actChar.mpNow >= skill.UseMp && GameManager.Instance.actChar.apNow >= skill.UseAp)
        {
            GameManager.Instance.actChar.mpNow -= skill.UseMp;
            GameManager.Instance.actChar.apNow -= skill.UseAp;

            float percent = Random.Range(1, 101);

            if (percent >= skill.CritP)
            {
                GameManager.Instance.soundEffects[0].Play();
                GameManager.Instance.oppChar.hpNow -= skill.Power * 0.01f * GameManager.Instance.actChar.StatusAtk * (100 / GameManager.Instance.oppChar.StatusDef);
            }
            else
            {
                GameManager.Instance.soundEffects[1].Play();
                GameManager.Instance.oppChar.hpNow -= skill.Power * 0.01f * GameManager.Instance.actChar.StatusAtk * (100 / GameManager.Instance.oppChar.StatusDef) * 2;
            }
        }

        GameManager.Instance.SkillCancel();
        GameManager.Instance.TurnText();
        GameManager.Instance.TurnChange();
    }

    public void OnClickSlash() => OnClickSkill(skils[0]);
    public void OnClickGuillotine() => OnClickSkill(skils[1]);
    public void OnClickFireBall() => OnClickSkill(skils[2]);
}
