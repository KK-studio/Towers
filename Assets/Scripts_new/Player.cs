using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private String name;
    [SerializeField] private bool isWhite;
    [SerializeField] private List<Dragable> pawns;


    private void Awake()
    {
        pawns = new List<Dragable>();
    }


    public void addPawnToPlayer(Dragable pawn)
    {
        pawns.Add(pawn);
    }

    public bool losePawn(Dragable pawn)
    {
        bool isFounded = pawns.Remove(pawn);

        Debug.Log("is Founded : " + isFounded.ToString());
        return true;
    }
    
    public bool losePawn(String guid)
    {
        bool isFounded = false;

        for (int i = 0; i < pawns.Count; i++)
        {
            if (pawns[i].getGUID() == guid)
            {
                pawns.RemoveAt(i);
                isFounded = true;
                break;
            }
        }

        return isFounded;
    }


    // Update is called once per frame
    void Update()
    {
    }
}