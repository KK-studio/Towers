using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Player player1;
    [SerializeField] private Player player2;


    public void removePawn(Dragable pawn)
    {
        if (player1.losePawn(pawn))
        {
            if (player1.checkLosed())
            {
                Debug.Log("player 1  lost");
                //Todo losing scenario

            }
            
        }
        else if (player2.losePawn(pawn))
        {
            if (player2.checkLosed())
            {
                Debug.Log("player 2  lost");
                //Todo losing scenario
            }
        }
    }

    // this function will call by network
    public void removePawn(String guid)
    {
        Dragable pawn = null;
        if (player1.losePawn(guid))
        {
            if (player1.checkLosed())
            {
                Debug.Log("player 1  lost");
                //Todo losing scenario

            }
            
        }
        else if (player2.losePawn(guid))
        {
            if (player2.checkLosed())
            {
                Debug.Log("player 2  lost");
                //Todo losing scenario
            }
        }
    }
}