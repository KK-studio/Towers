using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoSingleton<GameConfig>
{
    [SerializeField] private Material baseWhiteColor;
    [SerializeField] private Material baseBalckColor;

    [SerializeField] public float delayDestroyPawnAfterFall = 2f;


    public Material getBaseBlackColor()
    {
        return baseBalckColor;
    }
    
    public Material getBaseWhiteColor()
    {
        return baseWhiteColor;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
