using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
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
        Debug.Log(Mathf.Ceil(Mathf.Log(GameManager.Instance.actChar.statusLv)));
        Debug.Log($"필요한 Exp 양 : {10 + Mathf.Ceil(Mathf.Log(GameManager.Instance.actChar.statusLv) * (GameManager.Instance.actChar.statusLv / 2 + 1))}, 현재 플레이어 레벨 : {GameManager.Instance.actChar.statusLv}");
        if (GameManager.Instance.actChar.expNow >= (10 + Mathf.Ceil(Mathf.Log(GameManager.Instance.actChar.statusLv) * (GameManager.Instance.actChar.statusLv/2 + 1))))
        {
            GameManager.Instance.actChar.expNow -= (10 + Mathf.Ceil(Mathf.Log(GameManager.Instance.actChar.statusLv) * (GameManager.Instance.actChar.statusLv / 2 + 1)));
            GameManager.Instance.actChar.statusLv++;
        }
    }
}
