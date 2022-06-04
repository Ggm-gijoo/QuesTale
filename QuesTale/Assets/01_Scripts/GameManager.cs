using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        GameEnd();
    }

    public void TurnChange()
    {
        if(actChar.apNow <= 0 || actChar.IsDefence) //턴을 넘기는 조건, Ap가 0이 되거나 방어를 했을 때
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
        StartCoroutine(AttackTo());
    }

    private IEnumerator AttackTo()
    {
        actText.text = $"{actChar.name} 의 공격!";
        actPanel.SetActive(false);
        Act();
        actChar.anim.SetTrigger(hashAttack);

        float randomCrit = Random.Range(1, 101);
        if (actChar.StatusCri >= randomCrit)
        {
            soundEffects[1].Play();
            oppChar.hpNow -= actChar.StatusAtk * (100 / oppChar.StatusDef) * 2;
            actText.text += "\n<color=#FFD800>크리티컬!</color>";
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
        actText.text = $"{actChar.name} 은(는) 방어를 시도했다!";
        actChar.IsDefence = true;
        StopCoroutine(EnemyAct());
        TurnChange();
    }

    public void GameEnd()
    {
        if(oppChar.hpNow <= 0)
        {
            StopAllCoroutines();
            actPanel.SetActive(false);
            oppChar.gameObject.SetActive(false);
            SceneManager.LoadScene("Field");
        }
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
