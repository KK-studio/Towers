using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoSingleton<GameConfig>
{
    [SerializeField] private Color baseWhiteColor;
    [SerializeField] private Color baseBalckColor;



    public Color getBaseBlackColor()
    {
        return baseBalckColor;
    }
    
    public Color getBaseWhiteColor()
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
