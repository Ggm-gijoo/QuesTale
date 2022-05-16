using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private CharacterStatus[] charactersStatus;

    private int index = 0;
    private bool isDeffence = false;


    public void DecideTurnOrder()
    {

    }
    public void TurnChange()
    {
        if (charactersStatus[index].Ap <= 0 || isDeffence)
        { 
            index++;
        }
    }
}
