using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    private float maxExp = 10;

    private void Start()
    {
        GameManager.Instance.actChar.statusLv = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ExpUp(5);
    }

    public void ExpUp(float exp)
    {
        GameManager.Instance.actChar.expNow += exp;
        ExpCheck();
    }
    public void ExpCheck()
    {
        Debug.Log($"�ʿ��� Exp �� : {maxExp}, ���� �÷��̾� ���� : {GameManager.Instance.actChar.statusLv}");
        if (GameManager.Instance.actChar.expNow >= maxExp)
        {
            maxExp += (Mathf.Ceil(Mathf.Log(GameManager.Instance.actChar.statusLv) * (GameManager.Instance.actChar.statusLv / 2 + 1))) + GameManager.Instance.actChar.statusLv * 2;
            GameManager.Instance.actChar.expNow -= maxExp;
            GameManager.Instance.actChar.statusLv++;
        }
    }
}
