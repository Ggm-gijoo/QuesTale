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
    private Text actText;

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
        if(actChar.apNow <= 0 || actChar.IsDefence) //턴을 넘기는 조건, Ap가 0이 되거나 방어를 했을 때
        {
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
            turnText.text = $"<b><color=#0000ff>{actChar.name}</color></b>의 턴\n <size=70>AP {actChar.apNow}</size>";
        else if (actChar.CompareTag("Enemy"))
            turnText.text = $"<b><color=#ff0000>{actChar.name}</color></b>의 턴\n <size=70>AP {actChar.apNow}</size>";
    }

    public void Act() //방어 외의 행동을 할 때
    {
        --actChar.apNow;
        TurnText();
    }

    public void Attack() //감안해야 할 것 : 인덱스 + 1의 값이 캐릭터의 수를 넘어가는가? 공격을 받는 상대가 방어를 하고 있는가?
    {
        actText.text = $"{actChar.name} 의 공격!";
        Act();
        actChar.anim.SetTrigger(hashAttack);
        soundEffects[0].Play();

        if (oppChar.IsDefence)
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef) / 2;
        else
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef);
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
        actText.text = $"{actChar.name} 은(는) 방어를 시도했다!";
        actChar.IsDefence = true;
        StopCoroutine(EnemyAct());
        TurnChange();
    }

    public IEnumerator EnemyAct()
    {
        while(actChar.apNow > 0 || !actChar.IsDefence)
        {
            if (actChar.CompareTag("Enemy"))
            {
                yield return new WaitForSeconds(1f);
                if (actChar.apNow >= 9)
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
