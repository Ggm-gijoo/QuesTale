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
        characters = characters.OrderByDescending(n => n.StatusSpd).ToList();
    }

    public void TurnChange()
    {
        if(characters[index].apNow <= 0 || characters[index].IsDefence) //���� �ѱ�� ����, Ap�� 0�� �ǰų� �� ���� ��
        {
            Debug.Log("�� �Ѿ!");
            if (index + 1 >= characterCount)
                index = 0;
            else
                index++;

            characters[index].IsDefence = false;

            if(characters[index].apNow < 9)
            characters[index].apNow++;
        }
    }

    public void Act() //��� ���� �ൿ�� �� ��
    {
        --characters[index].apNow;
        Debug.Log("���� �÷��̾� : " + index + ", ���� AP : " + characters[index].apNow);
    }

    public void Attack() //�����ؾ� �� �� : �ε��� + 1�� ���� ĳ������ ���� �Ѿ�°�? ������ �޴� ��밡 �� �ϰ� �ִ°�?
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
    public void Defence()
    {
        characters[index].IsDefence = true;
        TurnChange();
    }
}
