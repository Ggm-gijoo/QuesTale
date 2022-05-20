using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private GameObject skillPanel;
    [SerializeField]
    private GameObject actPanel;
    [SerializeField]
    private Text TurnText;

    public List<CharacterManager> characters = new List<CharacterManager>();

    private int index = 0;
    private int characterCount;

    void Start()
    {
        TurnText.text = $"{characters[index].name}�� ��";
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
        if(characters[index].apNow <= 0 || characters[index].IsDefence) //���� �ѱ�� ����, Ap�� 0�� �ǰų� �� ���� ��
        {
            print("�� �Ѿ!");
            if (index + 1 >= characterCount)
                index = 0;
            else
                index++;

            characters[index].IsDefence = false;

            if(characters[index].apNow < 9)
            characters[index].apNow++;


            TurnText.text = $"{characters[index].name}�� ��";
            if (characters[index].CompareTag("Player"))
            {
                actPanel.SetActive(true);
                skillPanel.SetActive(false);
            }
            else if (characters[index].CompareTag("Enemy"))
            {
                actPanel.SetActive(false);
                skillPanel.SetActive(false);
                StartCoroutine(EnemyAct());
            }
        }
    }

    public void Act() //��� ���� �ൿ�� �� ��
    {
        --characters[index].apNow;
        print("���� �÷��̾� : " + characters[index].name + ", �÷��̾��� ������ : " + characters[index].Status.name + ", ���� AP : " + characters[index].apNow);
    }

    public void Attack() //�����ؾ� �� �� : �ε��� + 1�� ���� ĳ������ ���� �Ѿ�°�? ������ �޴� ��밡 �� �ϰ� �ִ°�?
    {
        print(characters[index].name + "����!");
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
        print(characters[index].name + "�� �� �õ��ߴ�!");
        characters[index].IsDefence = true;
        StopCoroutine(EnemyAct());
        TurnChange();
    }

    public IEnumerator EnemyAct()
    {
        while(characters[index].apNow >= 0 || !characters[index].IsDefence)
        {
            if (characters[index].CompareTag("Enemy"))
            {
                yield return new WaitForSeconds(1f);
                switch (Random.Range(0, 2))
                {
                    case 0:
                        Attack();
                        break;
                    case 1:
                        Defence();
                        break;
                    default:
                        break;
                }
            }
            else yield break;
        }
        
    }

}
