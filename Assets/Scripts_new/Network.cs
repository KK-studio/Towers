using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class Network : MonoSingleton<Network>
{
    WebSocket ws;

    bool connected = false;


    void Start()
    {
        Connect();
    }

    void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
        }
    }

    void Connect()
    {
        ws = new WebSocket("ws://localhost:8080/matchmaking"); // replace with your server URL
        ws.OnOpen += OnOpen;
        ws.OnMessage += OnMessage;
        ws.OnError += OnError;
        ws.OnClose += OnClose;
        ws.ConnectAsync();
    }

    void OnOpen(object sender, EventArgs e)
    {
        Debug.Log("WebSocket connected");
        connected = true;
    }

    void OnMessage(object sender, MessageEventArgs e)
    {
        string data = e.Data;
        Debug.Log("Received message: " + data);
        MsgManager.Instance.handelIncomingMsg(data);
    }

    void OnError(object sender, ErrorEventArgs e)
    {
        Debug.LogError("WebSocket error: " + e.Message);
    }

    void OnClose(object sender, CloseEventArgs e)
    {
        Debug.LogWarning("WebSocket closed: " + e.Reason);
        connected = false;
        StartCoroutine(RetryConnection());
    }

    IEnumerator RetryConnection()
    {
        while (!connected)
        {
            yield return new WaitForSeconds(1f);
            Connect();
        }
    }

    void Update()
    {
        // if (ws != null && ws.ReadyState == WebSocketState.Open)
        // {
        //     ws.Send("Hello, world!");
        // }
    }

    public void sendMsg(string str)
    {
        ws.Send(str);
    }
}