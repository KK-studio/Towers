using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public List<GameObject> towers;
    [SerializeField] private GameObject[] SpawnLocations;
    private int selected;
    public bool isClient = false;
    public int playerID;
    // Start is called before the first frame update
    void Start()
    {
        GameObject randomSpawn = SpawnLocations[Random.Range(0,SpawnLocations.Length)];
        int i = 0;
        foreach (var t in towers)
        {
            t.GetComponent<TowerComponent>().PlayerID = playerID;
            t.GetComponent<TowerComponent>().index = i;
            t.transform.position = randomSpawn.transform.GetChild(i).position;
            i++;
        }
        GameManager.Instance.onShootAction += shoot;
    }



    private void OnDisable()
    {
      //  GameManager.Instance.onShootAction -= shoot;
    }

    private void shoot(Vector2 direction,float power)
    {
        towers[selected].GetComponent<Rigidbody>().AddForce(direction*power,ForceMode.Impulse);
    }

    public void select(int selected)
    {
        this.selected = selected;
        towers[selected].transform.localScale *= 2;
        //TODO select action
    }
}
