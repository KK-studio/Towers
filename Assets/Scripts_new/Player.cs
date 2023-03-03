using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private String name;
    [SerializeField] private bool isWhite;
    [SerializeField] private List<Dragable> pawns;


    private void Start()
    {
        foreach (Dragable pawn in pawns)
        {
            pawn.setBaseMaterial(isWhite);
        }
    }


    public void addPawnToPlayer(Dragable pawn)
    {
        pawns.Add(pawn);
    }

    public bool losePawn(Dragable pawn)
    {
        int index = pawns.IndexOf(pawn);

        if (index == -1)
        {
            return false;
        }

        pawns.RemoveAt(index);
        Debug.Log(index);

        Destroy(pawn.gameObject, GameConfig.Instance.delayDestroyPawnAfterFall);
        return true;
    }

    public bool losePawn(String guid)
    {
        bool isFounded = false;

        for (int i = 0; i < pawns.Count; i++)
        {
            if (pawns[i].getGUID() == guid)
            {
                Destroy(pawns[i].gameObject, GameConfig.Instance.delayDestroyPawnAfterFall);
                pawns.RemoveAt(i);
                isFounded = true;
                break;
            }
        }

        return isFounded;
    }

    //reutrn true if losed
    public bool checkLosed()
    {
        if (pawns.Count <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}