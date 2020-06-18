using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    
    public int playerNumbers = 2;
    public int towersNumber = 3;
    private int gameState = 0;
    private int[] points;
    public Action<int> onDropAction;
   // public Action<Vector2,float> onShootAction;
    public Action <Transform>onChoosingCharacterAction;
    public Player[] Players;
    

    [SerializeField] private TouchController touchController;
    // Start is called before the first frame update
    protected override void OnAwake()
    {
        onDropAction = null;
    }

    void Start()
    {
        Players = FindObjectsOfType<Player>();
        Color[] colors = {Color.black, Color.red, Color.green, Color.yellow, Color.blue, Color.white, Color.magenta};
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].initializations(colors[Random.Range(0,colors.Length)]);
        }
        points = Enumerable.Repeat(towersNumber,playerNumbers).ToArray(); //make first points for players  size palyerNumbers   amount : numTower 
        onChoosingCharacterAction(Players[gameState].selectedTarget);
    }


    public void OnDrop(int playerID)
    {
        points[playerID]--;
        onDropAction(playerID);
        onTurnSwitch();
    }

    public void OnShoot(Vector2 direction,float power)
    {
        touchController.enabled = false;
        Players[gameState].shoot(direction,power,onTurnSwitch);
    }


    private void onTurnSwitch()
    {
        gameState = (gameState + 1) % playerNumbers;
        onChoosingCharacterAction(Players[gameState].selectedTarget);
        touchController.enabled = true;
    }
    public void OnChoosingCharacter(Transform towerSelected)
    {
        if (Players[gameState].playerID == towerSelected.GetComponent<TowerComponent>().PlayerID)
        {
            Players[gameState].select(towerSelected.GetComponent<TowerComponent>().index);
            onChoosingCharacterAction(towerSelected);
        }
        
    }
}
