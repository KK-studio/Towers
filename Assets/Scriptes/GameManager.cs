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
    //public Action<int> onDropAction;
   // public Action<Vector2,float> onShootAction;
    public Action <Transform>onChoosingCharacterAction;
    public Player[] Players;
    

    [SerializeField] private TouchController touchController;
    // Start is called before the first frame update


    void Start()
    {
        Players = FindObjectsOfType<Player>();
        Color[] colorss = { Color.red,Color.blue, Color.white};
        List<Color> colors = colorss.OfType<Color>().ToList();
        for (int i = 0; i < Players.Length; i++)
        {
            int random = Random.Range(0, colors.Count);
            Players[i].initializations(colors[random]);
            colors.RemoveAt(i);
        }
        points = Enumerable.Repeat(towersNumber,playerNumbers).ToArray(); //make first points for players  size palyerNumbers   amount : numTower 
        OnChoosingCharacter(Players[gameState].selectedTarget);
    }


    public void OnDrop(TowerComponent towerComponent)
    {
        points[towerComponent.PlayerID]--;
        Players[towerComponent.PlayerID].destroyTower();
        if (points[towerComponent.PlayerID] == 0)
        {
            //TODO on win/lose action
        }
        else
        {
            //onDropAction(playerID);
            onTurnSwitch();
        }

    }

    public void OnShoot(Vector3 direction,float power)
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
