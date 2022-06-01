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
    private Text turnText;
    [SerializeField]
    private Text actText;

    [SerializeField]
    BgmManager bgmManager;

    private GameObject[] enemies;
    private GameObject[] players;

    public List<CharacterManager> characters = new List<CharacterManager>();

    public int Index { private set; get; }
    private int characterCount;

    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDie = Animator.StringToHash("Die");

    void Start()
    {
        Index = 0;
        bgmManager.BgmEvent(1);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");
        characterCount = characters.Count;
        characters = characters.OrderByDescending(n => n.StatusSpd).ToList();
        TurnText();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SkillCancel();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            bgmManager.BgmEvent(Random.Range(0,2));
        }
    }

    public void TurnChange()
    {
        if(characters[Index].apNow <= 0 || characters[Index].IsDefence) //���� �ѱ�� ����, Ap�� 0�� �ǰų� �� ���� ��
        {
            if (Index + 1 >= characterCount)
            {
                Index = 0;
            }
            else
            {
                Index++;
            }

            characters[Index].IsDefence = false;

            if(characters[Index].apNow < 9)
            characters[Index].apNow++;

            TurnText();
            if (characters[Index].CompareTag("Player"))
            {
                actPanel.SetActive(true);
                skillPanel.SetActive(false);
            }
            else if (characters[Index].CompareTag("Enemy"))
            {
                actPanel.SetActive(false);
                skillPanel.SetActive(false);
                StartCoroutine(EnemyAct());
            }
        }
    }

    public void TurnText()
    {
        if (characters[Index].CompareTag("Player"))
            turnText.text = $"<b><color=#0000ff>{characters[Index].name}</color></b>�� ��\n <size=70>AP {characters[Index].apNow}</size>";
        else if (characters[Index].CompareTag("Enemy"))
            turnText.text = $"<b><color=#ff0000>{characters[Index].name}</color></b>�� ��\n <size=70>AP {characters[Index].apNow}</size>";
    }

    public void Act() //��� ���� �ൿ�� �� ��
    {
        --characters[Index].apNow;
        TurnText();
    }

    public void Attack() //�����ؾ� �� �� : �ε��� + 1�� ���� ĳ������ ���� �Ѿ�°�? ������ �޴� ��밡 �� �ϰ� �ִ°�?
    {
        actText.text = $"{characters[Index].name} �� ����!";
        Act();
        characters[Index].anim.SetTrigger(hashAttack);
        if (Index + 1 < characterCount)
        {
            if(characters[Index + 1].IsDefence)
                characters[Index + 1].hpNow -= characters[Index].StatusAtk * (100 / characters[Index + 1].StatusDef)/2;
            else
                characters[Index + 1].hpNow -= characters[Index].StatusAtk * (100 / characters[Index + 1].StatusDef);

        }
        else
        {
            if (characters[0].IsDefence)
                characters[0].hpNow -= characters[Index].StatusAtk * (100 / characters[0].StatusDef) / 2;
            else
                characters[0].hpNow -= characters[Index].StatusAtk * (100 / characters[0].StatusDef);
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
        actText.text = $"{characters[Index].name} ��(��) �� �õ��ߴ�!";
        characters[Index].IsDefence = true;
        StopCoroutine(EnemyAct());
        TurnChange();
    }

    public IEnumerator EnemyAct()
    {
        while(characters[Index].apNow >= 0 || !characters[Index].IsDefence)
        {
            if (characters[Index].CompareTag("Enemy"))
            {
                yield return new WaitForSeconds(1f);
                if (characters[Index].apNow >= 9)
                {
                    Attack();
                }
                else if (characters[Index].apNow <= 1)
                {
                    Defence();
                }
                else
                {
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
            }
            else yield break;
        }
        
    }

}
