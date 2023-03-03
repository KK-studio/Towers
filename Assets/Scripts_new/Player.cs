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

    public void losePawn(Dragable pawn)
    {
        bool isFounded = pawns.Remove(pawn);

        Debug.Log("is Founded : "  + isFounded.ToString() );
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
