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

    [SerializeField] private Material Onselection;
    private Material lastMaterial;
    private Color targetColor;
    private Coroutine animationProgress;
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
        this.targetColor = targetColor;
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
        //select(0);
        selected = 0;
        selectedTarget = towers[0].transform;
    }


    private void OnDisable()
    {
      //  GameManager.Instance.onShootAction -= shoot;
    }

    public void shoot(Vector3 direction,float power,Action onFinishingForce)
    {
        Vector3 force = new Vector3(direction.x,0,direction.z);
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
        diSelect();
        this.selected = selected;

        lastMaterial = towers[selected].GetComponentInChildren<MeshRenderer>().material;
        towers[selected].GetComponentInChildren<MeshRenderer>().material = Onselection;
        if(animationProgress == null)
            animationProgress = StartCoroutine(materialAnimation());
        else
        {
            StopCoroutine(animationProgress);
            animationProgress = StartCoroutine(materialAnimation());
        }
        selectedTarget = towers[selected].transform;

        //TODO select action
    }

    private IEnumerator materialAnimation()
    {
        float t = 0f;
        Onselection.SetColor("Color_4669EC58",targetColor);
        while (true)
        {
            while (t < 2f)
            {
                Onselection.SetFloat("Vector1_68BB8CB8", t);
                t+=0.1f;
                yield return new WaitForSeconds(0.1f);
            }

            while (t > 0f)
            {
                Onselection.SetFloat("Vector1_68BB8CB8", t);
                t-=0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }
    
    public void diSelect()
    {
        if (this.selected != -1)
        {
            towers[this.selected].GetComponentInChildren<MeshRenderer>().material = lastMaterial;
        }
    }
}
