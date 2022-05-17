using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{
    public List<CharacterManager> characters = new List<CharacterManager>();

    private int index = 0;
    private int characterCount;

    void Start()
    {
        characterCount = characters.Count;
        characters = characters.OrderBy(n => n.StatusSpd).ToList();
    }

    public void TurnChange()
    {
        if(characters[index].StatusAp <= 0 || characters[index].IsDefence) //턴을 넘기는 조건, Ap가 0이 되거나 방어를 했을 때
        {
            if (index >= characterCount)
                index = 0;
            else
                index++;
            if(characters[index].StatusAp < 9)
            characters[index].StatusAp++;
        }
    }

    public void Act() //방어 외의 행동을 할 때
    {
        --characters[index].StatusAp;
    }

    public void Attack() //감안해야 할 것 : 인덱스+1의 값이 캐릭터의 수를 넘어가는가? 공격을 받는 상대가 방어를 하고 있는가?
    {
        Act();
        if (index < characterCount)
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
}
