using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;


    public void remove(Dragable pawn)
    {
        if (!player1.losePawn(pawn))
        {
            if (!player2.losePawn(pawn))
            {
                Debug.Log("none of player has that object");
            }
        }
    }

    // this function will call by network
    public void remove(String guid)
    {
        if (!player1.losePawn(guid))
        {
            if (!player2.losePawn(guid))
            {
                Debug.Log("none of player has that object");
            }
        }
    }
}