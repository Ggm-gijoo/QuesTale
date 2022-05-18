using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private GameObject actPanel;

    public List<CharacterManager> characters = new List<CharacterManager>();

    private int index = 0;
    private int characterCount;

    void Start()
    {
        characterCount = characters.Count;
        characters = characters.OrderByDescending(n => n.StatusSpd).ToList();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SkillCancel();
        }
    }

    public void TurnChange()
    {
        if(characters[index].apNow <= 0 || characters[index].IsDefence) //턴을 넘기는 조건, Ap가 0이 되거나 방어를 했을 때
        {
            Debug.Log("턴 넘어감!");
            if (index + 1 >= characterCount)
                index = 0;
            else
                index++;

            characters[index].IsDefence = false;

            if(characters[index].apNow < 9)
            characters[index].apNow++;
        }
    }

    public void Act() //방어 외의 행동을 할 때
    {
        --characters[index].apNow;
        Debug.Log("현재 플레이어 : " + characters[index].name + ", 플레이어의 직업군 : " + characters[index].Status.name + ", 남은 AP : " + characters[index].apNow);
    }

    public void Attack() //감안해야 할 것 : 인덱스 + 1의 값이 캐릭터의 수를 넘어가는가? 공격을 받는 상대가 방어를 하고 있는가?
    {
        Act();
        if (index + 1 < characterCount)
        {
            if(characters[index + 1].IsDefence)
                characters[index + 1].hpNow -= characters[index].StatusAtk * (100 / characters[index + 1].StatusDef)/2;
            else
                characters[index + 1].hpNow -= characters[index].StatusAtk * (100 / characters[index + 1].StatusDef);
        }
        else
        {
            if (characters[0].IsDefence)
                characters[0].hpNow -= characters[index].StatusAtk * (100 / characters[0].StatusDef) / 2;
            else
                characters[0].hpNow -= characters[index].StatusAtk * (100 / characters[0].StatusDef);
        }
        TurnChange();
    }

    public void Skill()
    {
        actPanel.SetActive(false);
        skillPanel.SetActive(true);
    }
    
    public void SkillCancel()
    {
        if (skillPanel.activeSelf)
        {
            skillPanel.SetActive(false);
            actPanel.SetActive(true);
        }
    }

    public void Defence()
    {
        characters[index].IsDefence = true;
        TurnChange();
    }
}
