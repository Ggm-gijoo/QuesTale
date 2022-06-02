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
    private GameObject soundEffect;
    [SerializeField]
    private Text turnText;
    [SerializeField]
    public Text actText;

    [SerializeField]
    BgmManager bgmManager;

    private GameObject[] enemies;
    private GameObject[] players;

    public AudioSource[] soundEffects;

    public List<CharacterManager> characters = new List<CharacterManager>();

    public CharacterManager actChar = null;
    public CharacterManager oppChar = null;

    public int Index { private set; get; }
    private int characterCount;

    private readonly int hashAttack = Animator.StringToHash("Attack");
    private readonly int hashDie = Animator.StringToHash("Die");

    void Start()
    {
        Index = 0;

        soundEffects = soundEffect.GetComponents<AudioSource>();
        bgmManager.BgmEvent(1);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");

        characterCount = characters.Count;
        characters = characters.OrderByDescending(n => n.StatusSpd).ToList();

        actChar = characters[0];
        oppChar = characters[1];

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
        if(actChar.apNow <= 0 || actChar.IsDefence) //���� �ѱ�� ����, Ap�� 0�� �ǰų� �� ���� ��
        {
            StopAllCoroutines();
            if (Index + 1 >= characterCount)
                Index = 0;
            else
                Index++;
            

            actChar = characters[Index];

            if (Index + 1 >= characterCount)
                oppChar = characters[0];
            else
                oppChar = characters[Index + 1];


            actChar.IsDefence = false;

            if(actChar.apNow < 9)
            actChar.apNow++;

            TurnText();
            if (actChar.CompareTag("Player"))
            {
                StopCoroutine(EnemyAct());
                actPanel.SetActive(true);
                skillPanel.SetActive(false);
            }
            else if (actChar.CompareTag("Enemy"))
            {
                actPanel.SetActive(false);
                skillPanel.SetActive(false);
                StartCoroutine(EnemyAct());
            }
        }
    }

    public void TurnText()
    {
        if (actChar.CompareTag("Player"))
            turnText.text = $"<b><color=#0000ff>{actChar.name}</color></b>�� ��\n <size=70>AP {actChar.apNow}</size>";
        else if (actChar.CompareTag("Enemy"))
            turnText.text = $"<b><color=#ff0000>{actChar.name}</color></b>�� ��\n <size=70>AP {actChar.apNow}</size>";
    }

    public void Act() //��� ���� �ൿ�� �� ��
    {
        --actChar.apNow;
        TurnText();
    }

    public void Attack() //�����ؾ� �� �� : �ε��� + 1�� ���� ĳ������ ���� �Ѿ�°�? ������ �޴� ��밡 �� �ϰ� �ִ°�?
    {
        StartCoroutine(AttackTo());
    }

    private IEnumerator AttackTo()
    {
        actText.text = $"{actChar.name} �� ����!";
        actPanel.SetActive(false);
        Act();
        actChar.anim.SetTrigger(hashAttack);

        float randomCrit = Random.Range(1, 101);
        if (actChar.StatusCri >= randomCrit)
        {
            soundEffects[1].Play();
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef) * 2;
            actText.text += "\n<color=#FFD800>�޼ҿ� �¾Ҵ�!</color>";
        }

        else if (oppChar.IsDefence)
        {
            soundEffects[0].Play();
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef) / 2;
        }

        else
        {
            soundEffects[0].Play();
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef);
        }

        yield return new WaitForSeconds(0.4f);
        if (actChar.CompareTag("Player"))
        actPanel.SetActive(true);
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
        actText.text = $"{actChar.name} ��(��) �� �õ��ߴ�!";
        actChar.IsDefence = true;
        StopCoroutine(EnemyAct());
        TurnChange();
    }

    public void GameEnd()
    {

    }

    public IEnumerator EnemyAct()
    {
        while(actChar.apNow >= 0 || !actChar.IsDefence)
        {
            if (actChar.CompareTag("Enemy"))
            {
                yield return new WaitForSeconds(0.6f);
                if (actChar.apNow >= 9 || oppChar.hpNow <= actChar.StatusAtk)
                {
                    Attack();
                }
                else if (actChar.apNow <= 1)
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
