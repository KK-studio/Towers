﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance != null)
                return _instance;
            return null;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = (T) this;
            initialization();
            DontDestroyOnLoad(gameObject);

        }  
        OnAwake();
        
    }

    protected virtual void OnAwake()
    {
        
    }
    protected virtual void initialization()
    {
        
    }
}
