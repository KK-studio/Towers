using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public List<GameObject> towers;
    [SerializeField] private GameObject[] SpawnLocations;
    private int selected = -1;
    public bool isClient = false;
    public int playerID;
    public Transform selectedTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject randomSpawn = SpawnLocations[Random.Range(0,SpawnLocations.Length)];
        int i = 0;
        foreach (var t in towers)
        {
            t.GetComponent<TowerComponent>().PlayerID = playerID;
            t.GetComponent<TowerComponent>().index = i;
            t.transform.position = randomSpawn.transform.GetChild(i).position;
            i++;
        }
        select(0);
        */
    }

    public void initializations(Color targetColor)// will call by game manager for syncing points
    {
        GameObject randomSpawn = SpawnLocations[Random.Range(0,SpawnLocations.Length)];
        int i = 0;
        foreach (var t in towers)
        {
            t.GetComponentInChildren<MeshRenderer>().material.color = targetColor;
            t.GetComponent<TowerComponent>().PlayerID = playerID;
            t.GetComponent<TowerComponent>().index = i;
            t.transform.position = randomSpawn.transform.GetChild(i).position;
            i++;
        }
        select(0);
    }


    private void OnDisable()
    {
      //  GameManager.Instance.onShootAction -= shoot;
    }

    public void shoot(Vector2 direction,float power,Action onFinishingForce)
    {
        Vector3 force = new Vector3(direction.x,0,direction.y);
        towers[selected].GetComponent<Rigidbody>().AddForce(force*power,ForceMode.Impulse);
        StartCoroutine(checkingForce(onFinishingForce));
    }

    private IEnumerator checkingForce(Action onComplete)
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            try
            {
                if (towers[selected].GetComponent<Rigidbody>().velocity.magnitude <= 0.1f)
                {
                    break;
                }
            }
            catch (Exception e)
            {
                break;
            }


            yield return null;
        }
        onComplete();
    }

    public void destroyTower()
    {
        Destroy(towers[selected].gameObject);
        towers[selected] = null;
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i] != null)
            {
                selectedTarget = towers[i].transform;
                selected = i;
                break;
            }
        }
    }
    
    public void select(int selected)
    {
        if(this.selected != -1)
          towers[this.selected].transform.localScale /= 1.2f;
        this.selected = selected;
        towers[selected].transform.localScale *= 1.2f;
        selectedTarget = towers[selected].transform;

        //TODO select action
    }
}
