using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> towers;
    private int selected;
    public bool isClient = false;
    public int playerID;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (var t in towers)
        {
            t.GetComponent<TowerComponent>().PlayerID = playerID;
            t.GetComponent<TowerComponent>().index = i;
            i++;
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.onShootAction += shoot;
    }

    private void OnDisable()
    {
        GameManager.Instance.onShootAction -= shoot;
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
