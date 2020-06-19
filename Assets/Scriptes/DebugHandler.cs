using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DebugHandler : MonoSingleton<DebugHandler>
{
    //call this class any where there is error or you want see any string as output in game UI

    [SerializeField] private Text Text;
    [SerializeField] private float timeToShowTextMassage = 3;
    private Coroutine MassageViewrCoroutine = null;
    private String TextMassage;
    private IEnumerator MassageViewr()
    {
        
        Text.enabled = true;
        float timer = timeToShowTextMassage;
        while (timer > 0)
        {
            Text.text = TextMassage;
            yield return new WaitForSeconds(0.3f);
            timer -= 0.3f;
        }
        Text.enabled = false;
        MassageViewrCoroutine = null;
    }

    public void showTextMassage(String massage)
    {
        TextMassage = massage;
        if (MassageViewrCoroutine == null)
        {
            MassageViewrCoroutine = StartCoroutine(MassageViewr());
        }
    }
}
