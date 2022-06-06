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
        if(GameManager.Instance.actChar.expNow >= (10 + Mathf.Floor(Mathf.Log10(GameManager.Instance.actChar.statusLv))))
        {
            Debug.Log("레벨업 하기 위한 Exp : "+ 10 + Mathf.Floor(Mathf.Log10(GameManager.Instance.actChar.statusLv) * ));
            GameManager.Instance.actChar.statusLv++;
            GameManager.Instance.actChar.expNow = 0;
        }
    }
}
