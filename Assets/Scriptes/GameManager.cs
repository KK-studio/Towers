using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public int playerNumbers = 2;
    public int towersNumber = 3;
    public static int gameState = 0;
    private int[] points;
    private Action<int> drop;

    private void shoot()
    {
        gameState = (gameState + 1) % playerNumbers;
        drop += droped;
    }

    // Start is called before the first frame update
    void Start()
    {
        points = Enumerable.Repeat(towersNumber,playerNumbers ).ToArray(); //make first points for players  size palyerNumbers   amount : numTower 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void droped(int playerID)
    {
        //you must find the team first :)
        points[playerID]--;

    }
}
