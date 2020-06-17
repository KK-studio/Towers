﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    
    public int playerNumbers = 2;
    public int towersNumber = 3;
    private int gameState = 0;
    private int[] points;
    public Action<int> onDropAction;
    public Action<Vector2,float> onShootAction;
    public Action <Transform>onChoosingCharacterAction;
    public Player[] Players;
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        onDropAction = null;
        onShootAction = null;
    }

    void Start()
    {
        Players = FindObjectsOfType<Player>();
        points = Enumerable.Repeat(towersNumber,playerNumbers).ToArray(); //make first points for players  size palyerNumbers   amount : numTower 
    }


    public void OnDrop(int playerID)
    {
        points[playerID]--;
        onDropAction(playerID);
    }

    public void OnShoot(Vector2 direction,float power)
    {
        gameState = (gameState + 1) % playerNumbers;
        onShootAction(direction,power);
    }

    public void OnChoosingCharacter(Transform towerSelected)
    {
        if (Players[gameState].playerID == towerSelected.GetComponent<TowerComponent>().PlayerID)
        {
            Players[gameState].select(towerSelected.GetComponent<TowerComponent>().index);
        }

        onChoosingCharacterAction(towerSelected);
    }
}
